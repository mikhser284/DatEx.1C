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
            OneS_Nomenclature_ShowSyncObjs(syncObjs);

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
            List<ITIS.ITISNomenclatureGroups> nomenclatureGroups = HttpClientOfCreatio.GetObjs<ITIS.ITISNomenclatureGroups>(removableObjsQuery);
            nomenclatureGroups.ForEach(x => HttpClientOfCreatio.DeleteObj(x));
        }

        public static void Creatio_Nomenclature_FirstSyncWithOneS(SyncObjs_SyncNomenclature syncObjs, SyncSettings settings)
        {
            //TODO Синхронизация номенклатуры с 1С
        }
    }

    /// <summary> Работа с 1С </summary>
    public partial class Program
    {
        /// <summary> Отобразить объекты номенклатуры полученные из 1C </summary>
        public static void OneS_Nomenclature_ShowSyncObjs(SyncObjs_SyncNomenclature syncObjs)
        {
            //TODO Показать структуру объектов полученных из 1С
            syncObjs.OneS_NomenclatureGroupsOrderedById.FirstOrDefault().Value.Show();
            syncObjs.OneS_NomenclatureItemsOrderedById.Values.Where(e => e.CostItemId.IsNotNullOrDefault()).Skip(0).Take(10).ToList().ShowOneCObjects();
        }

        /// <summary> Получить объекты для синхронизации с 1С </summary>
        public static SyncObjs_SyncNomenclature OneS_GetSyncObjs_NomenclatureInfo(SyncSettings settings)
        {
            //TODO Получить объекты из 1С
            SyncObjs_SyncNomenclature syncObjs = new SyncObjs_SyncNomenclature();

            OneS_GetSyncObjs_NomenclatureGroups(settings, syncObjs);
            OneS_GetSyncObjs_NomenclatureItems(settings, syncObjs);


            return syncObjs;
        }

        private static void OneS_GetSyncObjs_NomenclatureGroups(SyncSettings settings, SyncObjs_SyncNomenclature syncObjs)
        {
            const Int32 MaxIdsPerPage = 25;
            Stack<List<OneS.Nomenclature>> stack = new Stack<List<OneS.Nomenclature>>();
            stack.Push(HttpClientOfOneS.GetObjsByIds<OneS.Nomenclature>(settings.OneSGuidOfNomenclatureRootFolder));

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
            const Int32 MaxParallelQueries = 6;
            const Int32 MaxIdsPerPage = 25;

            List<List<Guid>> paginatedIds = syncObjs.OneS_NomenclatureGroupsOrderedById.Keys.Paginate(MaxParallelQueries * MaxIdsPerPage);

            foreach(var idsPage in paginatedIds)
            {
                List<Task<List<OneS.Nomenclature>>> parallelQueries = new List<Task<List<Nomenclature>>>();

                Int32 pageIndex = 0;
                foreach(var page in idsPage.Paginate(MaxIdsPerPage))
                {
                    String query = $"$filter=IsFolder eq false and ({String.Join("\n or ", page.Select(e => $"Parent_Key eq guid'{e}'"))})";
                    parallelQueries.Add(new Task<List<OneS.Nomenclature>>(() => HttpClientOfOneS.GetObjs<OneS.Nomenclature>(query)));
                }
                
                parallelQueries.StartAndWaitForAll();

                foreach(var query in parallelQueries)
                    foreach(var nomenclature in query.Result)
                        syncObjs.OneS_NomenclatureItemsOrderedById.Add(nomenclature.Id, nomenclature);   
            }
        }
    }
}
