using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneS = DatEx.OneS.DataModel;
using ITIS = DatEx.Creatio.DataModel.ITIS;
using Terrasoft = DatEx.Creatio.DataModel.Terrasoft.Base;
using DatEx.OneS.DataModel;
using DatEx.Creatio.DataModel.ITIS;
using DatEx.Creatio;
using App.Auxilary;
using System.Diagnostics;
using OfficeOpenXml.FormulaParsing.Excel.Functions;

namespace App
{
    public partial class Program
    {
        public static void SyncNomenclature(SyncSettings settings)
        {
            Task<SyncObjs_SyncNomenclature> oneS_NomenclatureInfo_GetSyncObjs = new Task<SyncObjs_SyncNomenclature>(() => OneS_GetSyncObjs_NomenclatureInfo(settings));
            Task[] tasks = new Task[]
            {
                new Task(() => Creatio_Nomenclature_ClearSyncObjs()),
                oneS_NomenclatureInfo_GetSyncObjs
            }.StartAndWaitForAll();

            SyncObjs_SyncNomenclature syncObjs = oneS_NomenclatureInfo_GetSyncObjs.Result;
            //OneS_Nomenclature_ShowSyncObjs(syncObjs);

            Creatio_Nomenclature_FirstSyncWithOneS(syncObjs, settings);
        }
    }


    /// <summary> Работа с Creatio </summary>
    public partial class Program
    {
        /// <summary> Удалить ранее синхнонизированные объекты в Creatio </summary>
        public static void Creatio_Nomenclature_ClearSyncObjs()
        {
            String removableObjsQuery = $"filter=ITISOneSId ne null and ITISOneSId ne {default(Guid)}";

            // Удалить ранее синхронизированные объекты номенклатуры
            List<ITIS.ITISCompaniesNomenclature> nomenclature = HttpClientOfCreatio.GetObjs<ITIS.ITISCompaniesNomenclature>(removableObjsQuery);
            nomenclature.ForEach(x => HttpClientOfCreatio.DeleteObj(x));

            // Удалить ранее синхронизированные объекты ед. измерения
            List<ITIS.Unit> measureUnits = HttpClientOfCreatio.GetObjs<ITIS.Unit>(removableObjsQuery);
            measureUnits.ForEach(x => HttpClientOfCreatio.DeleteObj(x));

            // Удалить ранее синхронизированные объекты статей закупок
            List<ITIS.ITISPurchasingArticle> purchasingArticles = HttpClientOfCreatio.GetObjs<ITIS.ITISPurchasingArticle>(removableObjsQuery);
            purchasingArticles.ForEach(x => HttpClientOfCreatio.DeleteObj(x));

            // Удалить ранее синхронизированные объекты номенклатурных групп
            Dictionary<Guid, ITIS.ITISNomenclatureGroups> nomenclatureGroups = HttpClientOfCreatio.GetObjs<ITIS.ITISNomenclatureGroups>(removableObjsQuery).ToDictionary(k => (Guid)k.Id);
            List<ITIS.ITISNomenclatureGroups> deletableObjs = new List<ITISNomenclatureGroups>();
            do
            {
                deletableObjs.Clear();
                foreach (var e in nomenclatureGroups.Values)
                {
                    Boolean isLeaf = true;
                    foreach(var v in nomenclatureGroups.Values)
                    {
                        if(v.ITISParentGroupId == e.Id)
                        {
                            isLeaf = false;
                            break;
                        }
                    }
                    if (isLeaf) deletableObjs.Add(e);
                }
                foreach(var e in deletableObjs)
                {
                    HttpClientOfCreatio.DeleteObj(e);
                    nomenclatureGroups.Remove((Guid)e.Id);
                }                
            } while(deletableObjs.Count != 0);            
        }

        public static void Creatio_Nomenclature_FirstSyncWithOneS(SyncObjs_SyncNomenclature syncObjs, SyncSettings settings)
        {
            Stopwatch watch = Stopwatch.StartNew();
            Console.WriteLine("Начало синхронизации");
            
            Console.WriteLine("Синхронизация номенклатурных групп");
            Cratio_MapObj_NomenclatureGroups(syncObjs, settings);
            Console.WriteLine(watch.Elapsed); watch.Restart();

            Console.WriteLine("Синхронизация ед. измерения");
            Cratio_SyncMeasureUnits(syncObjs, settings);
            Console.WriteLine(watch.Elapsed); watch.Restart();

            Console.WriteLine("Синхронизация статей закупки");
            Cratio_SyncPurchasingArticles(syncObjs, settings);
            Console.WriteLine(watch.Elapsed); watch.Restart();

            Console.WriteLine("Синхронизация номенклатуры");
            Cratio_SyncNomenclatureItems(syncObjs, settings);
            Console.WriteLine(watch.Elapsed); watch.Restart();

            Console.WriteLine("Синхронизация завершена");
        }

        public static void Cratio_MapObj_NomenclatureGroups(SyncObjs_SyncNomenclature syncObjs, SyncSettings settings)
        {
            // Синхронизация номенклатурных групп
            foreach(Nomenclature e in syncObjs.OneS_NomenclatureGroupsOrderedById.Values)
            {
                ITISNomenclatureGroups x = new ITISNomenclatureGroups();
                //
                //
                x.ITISOneSId = e.Id;
                x.ITISName = e.Description;
                x.ITISNotes = e.Comments;
                //
                //
                x = HttpClientOfCreatio.CreateObj(x);
                syncObjs.Creatio_NomenclatureGroupsOrderedByOneSId.Add((Guid)x.ITISOneSId, x);
            }

            // Связывание номенклатурных групп в иерархию
            foreach(ITISNomenclatureGroups e in syncObjs.Creatio_NomenclatureGroupsOrderedByOneSId.Values)
            {
                Nomenclature n = syncObjs.OneS_NomenclatureGroupsOrderedById[e.ITISOneSId];
                if (!n.ParentId.IsNotNullOrDefault()) continue;

                ITISNomenclatureGroups parentNmc = syncObjs.Creatio_NomenclatureGroupsOrderedByOneSId[(Guid)n.ParentId];
                e.ITISParentGroupId = parentNmc.Id;
                e.ITISParentGroup = parentNmc;

                HttpClientOfCreatio.UpdateObj(e);
            }
        }

        public static void Cratio_SyncNomenclatureItems(SyncObjs_SyncNomenclature syncObjs, SyncSettings settings)
        {
            Int32 parallelQueries = 12;

            foreach (List<Nomenclature> page in syncObjs.OneS_NomenclatureItemsOrderedById.Values.Paginate(parallelQueries))
            {
                List<Task> queriesButch = new List<Task>();
                
                foreach(Nomenclature nmc in page)
                    queriesButch.Add(new Task(() => MapSingleElement(nmc, syncObjs, settings)));

                queriesButch.StartAndWaitForAll();
            }
            
            static void MapSingleElement(Nomenclature e, SyncObjs_SyncNomenclature syncObjs, SyncSettings settings)
            {
                ITISCompaniesNomenclature x = new ITISCompaniesNomenclature();
                //
                //
                x.ITISOneSId = e.Id;
                x.ITISName = e.Description;
                x.ITISNotes = e.Comments;
                x.ITISOneCCode = e.Code;
                //
                // Связывание с номенклатурной группой
                var parentGroup = syncObjs.Creatio_NomenclatureGroupsOrderedByOneSId[(Guid)e.ParentId];
                x.ITISNomenclaturesGroupId = parentGroup.Id;
                //
                x.ITISNomenclatureUnitOfMeasurementId = syncObjs.Creatio_MeasureUnitsOrderedByOneSId[(Guid)e.MeasureUnit_VirtualProp.Id].Id;
                x.ITISOwnerId = settings.CreatioGuidOfNomenclatureItemOwnerByDefault;
                if (e.CostArticleId.IsNotNullOrDefault())
                    x.ITISPurchasingArticleId = syncObjs.Creatio_CostArticlesOrderedByOneSId[(Guid)e.CostArticleId].Id;

                //
                //
                x = HttpClientOfCreatio.CreateObj(x);
                syncObjs.Creatio_NomenclatureItemsOrderedByOneSId.Add((Guid)x.ITISOneSId, x);
            }
        }

        public static void Cratio_SyncMeasureUnits(SyncObjs_SyncNomenclature syncObjs, SyncSettings settings)
        {
            foreach(OneS.MeasureUnitsClassifier e in syncObjs.OneS_MeassureUnitsClassifiersOrderedById.Values)
            {
                ITIS.Unit x = new Unit();
                //
                //
                x.Name = e.Description;
                x.ShortName = e.Description;
                x.Description = e.FullName;
                x.ITISOneSId = e.Id;
                x.ITISFullName = e.FullName.IfIsNullOrEmpty(x.Name);
                x.ITISInternationalShortName = e.InternationalShortName.IfIsNullOrEmpty(x.Name);
                //
                //
                x = HttpClientOfCreatio.CreateObj(x);
                syncObjs.Creatio_MeasureUnitsOrderedByOneSId.Add((Guid)x.ITISOneSId, x);
            }
        }

        public static void Cratio_SyncPurchasingArticles(SyncObjs_SyncNomenclature syncObjs, SyncSettings settings)
        {
            foreach (OneS.CostArticle e in syncObjs.OneS_CostArticlesOrderedById.Values)
            {
                ITIS.ITISPurchasingArticle x = new ITISPurchasingArticle();
                //
                //
                x.Name = e.Description;
                x.ITISOneSId = e.Id;
                //
                //
                x = HttpClientOfCreatio.CreateObj(x);
                syncObjs.Creatio_CostArticlesOrderedByOneSId.Add((Guid)x.ITISOneSId, x);
            }
        }
    }

    /// <summary> Работа с 1С </summary>
    public partial class Program
    {
        /// <summary> Отобразить объекты номенклатуры полученные из 1C </summary>
        public static void OneS_Nomenclature_ShowSyncObjs(SyncObjs_SyncNomenclature syncObjs)
        {
            syncObjs.OneS_NomenclatureGroupsOrderedById.FirstOrDefault().Value.Show();
            syncObjs.OneS_NomenclatureItemsOrderedById.Values.Where(e => e.CostArticleId.IsNotNullOrDefault()).Skip(0).Take(10).ToList().ShowOneCObjects();

            syncObjs.OneS_MeassureUnitsClassifiersOrderedById.Values.ForEach(e => Console.WriteLine(e));
        }

        /// <summary> Получить объекты для синхронизации с 1С </summary>
        public static SyncObjs_SyncNomenclature OneS_GetSyncObjs_NomenclatureInfo(SyncSettings settings)
        {
            SyncObjs_SyncNomenclature syncObjs = new SyncObjs_SyncNomenclature();

            Console.WriteLine("Получение классификаторов единиц изменения, папок с номенклатуройб");
            Stopwatch stopwatch = Stopwatch.StartNew();
            Task[] tasks = new Task[]
            {
                new Task(() => OneS_GetSyncObjs_MeasureUnitsClassifier(settings, syncObjs)),
                new Task(() => OneS_GetSyncObjs_NomenclatureGroups(settings, syncObjs)),
            }.StartAndWaitForAll();
            
            Console.WriteLine(stopwatch.Elapsed);
            stopwatch.Restart();

            Console.WriteLine("Получение номенклатуры");
            OneS_GetSyncObjs_NomenclatureItems(settings, syncObjs);

            Console.WriteLine(stopwatch.Elapsed);
            stopwatch.Restart();

            Console.WriteLine("Получение статей затрат и единиц измерения");
            Task[] tasks2 = new Task[]
            {
                new Task(() => OneS_GetSyncObjs_MeasureUnits(settings, syncObjs)),
                new Task(() => OneS_GetSyncObjs_CostItems(settings, syncObjs))
                
            }.StartAndWaitForAll();

            Console.WriteLine(stopwatch.Elapsed);
            stopwatch.Stop();

            OneS_BindNomenclatureRelatedObjs(settings, syncObjs);

            return syncObjs;
        }

        private static void OneS_GetSyncObjs_NomenclatureGroups(SyncSettings settings, SyncObjs_SyncNomenclature syncObjs)
        {
            const Int32 MaxIdsPerPage = 25;
            Stack<List<OneS.Nomenclature>> stack = new Stack<List<OneS.Nomenclature>>();
            var nmcRootFolder = HttpClientOfOneS.GetObjsByIds<OneS.Nomenclature>(settings.OneSGuidOfNomenclatureRootFolder);
            stack.Push(nmcRootFolder);

            while (stack.Count > 0)
            {
                List<OneS.Nomenclature> nmcDirs = stack.Pop();
                if (nmcDirs.Count < 1) continue;

                nmcDirs.ForEach(e => syncObjs.OneS_NomenclatureGroupsOrderedById.Add(e.Id, e));

                List<List<OneS.Nomenclature>> queries = nmcDirs.Paginate(MaxIdsPerPage);//.ToList();
                Task<List<OneS.Nomenclature>>[] queriesTasks = new Task<List<OneS.Nomenclature>>[queries.Count];
                for (int i = 0; i < queries.Count; i++)
                {
                    String query = $"$filter=IsFolder eq true and ({String.Join("\n or ", queries[i].Select(n => $"Parent_Key eq guid'{n.Id}'"))})";
                    queriesTasks[i] = new Task<List<OneS.Nomenclature>>(() => HttpClientOfOneS.GetObjs<OneS.Nomenclature>(query));
                }
                queriesTasks.StartAndWaitForAll();
                List<OneS.Nomenclature> childDirs = new List<Nomenclature>();
                queriesTasks.ForEach(e => childDirs.AddRange(e.Result));
                stack.Push(childDirs);
            }
        }


        private static void OneS_GetSyncObjs_NomenclatureItems(SyncSettings settings, SyncObjs_SyncNomenclature syncObjs)
        {
            const Int32 MaxIdsPerPage = 25;

            List<List<Guid>> idsBatches = syncObjs.OneS_NomenclatureGroupsOrderedById.Keys.Paginate(MaxIdsPerPage);
            List<Task<List<OneS.Nomenclature>>> parallelQueries = new List<Task<List<Nomenclature>>>();

            foreach (var queryableIds in idsBatches)
            {
                String query = $"$filter=IsFolder eq false and ({String.Join("\n or ", queryableIds.Select(e => $"Parent_Key eq guid'{e}'"))})";
                parallelQueries.Add(new Task<List<OneS.Nomenclature>>(() => HttpClientOfOneS.GetObjs<OneS.Nomenclature>(query)));                
            }

            parallelQueries.StartAndWaitForAll();

            foreach (var query in parallelQueries)
                foreach (var nomenclature in query.Result)
                {
                    syncObjs.OneS_NomenclatureItemsOrderedById.Add(nomenclature.Id, nomenclature);
                    //
                    syncObjs.OneS_MeasureUnitsOrderedById.AddGuidKey(nomenclature.MeasureUnitForRemainsStoragingId);
                    syncObjs.OneS_MeasureUnitsOrderedById.AddGuidKey(nomenclature.BaseMeasureUnitId);
                    syncObjs.OneS_MeasureUnitsOrderedById.AddGuidKey(nomenclature.MeasureUnitForReportsId);
                    syncObjs.OneS_MeasureUnitsOrderedById.AddGuidKey(nomenclature.PlacesMeasureUnitId);
                    syncObjs.OneS_CostArticlesOrderedById.AddGuidKey(nomenclature.CostArticleId);
                }
        }




        private static void OneS_GetSyncObjs_MeasureUnits(SyncSettings settings, SyncObjs_SyncNomenclature syncObjs)
        {
            const Int32 MaxIdsPerPage = 20;

            var idsBatches = syncObjs.OneS_MeasureUnitsOrderedById.Keys.Paginate(MaxIdsPerPage);
            List<Task<List<OneS.MeasureUnit>>> parallelQueries = new List<Task<List<MeasureUnit>>>();

            foreach(var queryableIds in idsBatches)
                parallelQueries.Add(new Task<List<OneS.MeasureUnit>>(() => HttpClientOfOneS.GetObjsByIds<OneS.MeasureUnit>(queryableIds)));                

            parallelQueries.StartAndWaitForAll();

            foreach (var query in parallelQueries)
                foreach (var e in query.Result)
                {
                    syncObjs.OneS_MeasureUnitsOrderedById[e.Id] = e;
                    syncObjs.OneS_MeassureUnitsClassifiersOrderedById.AddGuidKey(e.MeasureUnitByClassifierId);
                }
        }



        private static void OneS_GetSyncObjs_MeasureUnitsClassifier(SyncSettings settings, SyncObjs_SyncNomenclature syncObjs)
        {
            syncObjs.OneS_MeassureUnitsClassifiersOrderedById = HttpClientOfOneS.GetObjs<OneS.MeasureUnitsClassifier>().ToDictionary(k => k.Id);
        }


        private static void OneS_GetSyncObjs_CostItems(SyncSettings settings, SyncObjs_SyncNomenclature syncObjs)
        {
            const Int32 MaxIdsPerPage = 20;

            var idsBatches = syncObjs.OneS_CostArticlesOrderedById.Keys.Paginate(MaxIdsPerPage);
            List<Task<List<CostArticle>>> parallelQueries = new List<Task<List<CostArticle>>>();

            foreach (var queryableIds in idsBatches)
                parallelQueries.Add(new Task<List<CostArticle>>(() => HttpClientOfOneS.GetObjsByIds<CostArticle>(queryableIds)));

            parallelQueries.StartAndWaitForAll();

            foreach (var query in parallelQueries)
                foreach (var e in query.Result)
                    syncObjs.OneS_CostArticlesOrderedById[e.Id] = e;
        }



        private static void OneS_BindNomenclatureRelatedObjs(SyncSettings settings, SyncObjs_SyncNomenclature syncObjs)
        {
            // Связать номенклатурные группы
            foreach(Nomenclature e in syncObjs.OneS_NomenclatureGroupsOrderedById.Values)
            {
                if (!e.ParentId.IsNotNullOrDefault()) continue;
                e.Parent_NavProp = syncObjs.OneS_NomenclatureGroupsOrderedById[(Guid)e.ParentId];
            }

            // Связать номенклатуру и номенклатурные группы
            foreach(Nomenclature e in syncObjs.OneS_NomenclatureItemsOrderedById.Values)
            {
                e.Parent_NavProp = syncObjs.OneS_NomenclatureGroupsOrderedById[(Guid)e.ParentId];
            }

            List<Nomenclature> deletableNomenclatureWithoutMeasureUnits = new List<Nomenclature>();
            // Связать номенклатуру и единицы измерения
            foreach (Nomenclature e in syncObjs.OneS_NomenclatureItemsOrderedById.Values)
            {
                MeasureUnit measureUnit = null;

                if (e.MeasureUnitForRemainsStoragingId.IsNotNullOrDefault())
                    measureUnit = syncObjs.OneS_MeasureUnitsOrderedById[(Guid)e.MeasureUnitForRemainsStoragingId];
                else if (e.BaseMeasureUnitId.IsNotNullOrDefault())
                    measureUnit = syncObjs.OneS_MeasureUnitsOrderedById[(Guid)e.BaseMeasureUnitId];

                if(measureUnit == null || !measureUnit.MeasureUnitByClassifierId.IsNotNullOrDefault())
                {
                    deletableNomenclatureWithoutMeasureUnits.Add(e);
                    continue;
                }

                e.MeasureUnit_VirtualProp = syncObjs.OneS_MeassureUnitsClassifiersOrderedById[(Guid)measureUnit.MeasureUnitByClassifierId];
            }

            // Удалить объекты номенклатуры к которым не привязаны ед. измерения
            foreach(var e in deletableNomenclatureWithoutMeasureUnits)
                syncObjs.OneS_NomenclatureItemsOrderedById.Remove(e.Id);

            // Связать номенклатуру и статьи затрат
            foreach (Nomenclature e in syncObjs.OneS_NomenclatureItemsOrderedById.Values)
                if(e.CostArticleId.IsNotNullOrDefault())
                    e.CostArticle_NavProp = syncObjs.OneS_CostArticlesOrderedById[(Guid)e.CostArticleId];
        }
    }
}
