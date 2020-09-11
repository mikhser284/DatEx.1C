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
            Task<SyncObjs_SyncNomenclature> oneS_Nomenclature_GetSyncObjs = new Task<SyncObjs_SyncNomenclature>(() => OneC_GetSyncObjs_Nomenclature(settings));
            Task[] tasks = new Task[]
            {
                new Task(() => Creatio_Nomenclature_ClearSyncObjs()),
                oneS_Nomenclature_GetSyncObjs
            }.StartAndWaitForAll();

            SyncObjs_SyncNomenclature syncObjs = oneS_Nomenclature_GetSyncObjs.Result;
            OneS_Nomenclature_ShowSyncObjs(syncObjs.OneS_NomenclatureById.Values.Take(2));

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
        public static void OneS_Nomenclature_ShowSyncObjs(IEnumerable<OneS.Nomenclature> nomenclatures)
        {
            //TODO Показать структуру объектов полученных из 1С
        }

        /// <summary> Получить объекты для синхронизации с 1С </summary>
        public static SyncObjs_SyncNomenclature OneC_GetSyncObjs_Nomenclature(SyncSettings settings)
        {
            SyncObjs_SyncNomenclature syncObjs = new SyncObjs_SyncNomenclature();

            //var res = HttpClientOfOneS.GetObjs<OneS.Nomenclature>("$filter=Description eq 'Нова Номенклатура'");
            OneS.Nomenclature rootNomenclatureFolder = HttpClientOfOneS.GetObjsByIds<OneS.Nomenclature>(settings.OneSGuidOfNomenclatureRootFolder).FirstOrDefault();
            if (rootNomenclatureFolder == null) throw new EntityNotFoundException("Не удалось получить корневую папку для синхронизации номенклатуры с ключом ");
            //TODO Получить объекты из 1С

            return syncObjs;
        }
    }
}
