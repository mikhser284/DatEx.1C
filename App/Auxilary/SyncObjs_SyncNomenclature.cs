namespace App
{
    using System;
    using System.Collections.Generic;
    using OneS = DatEx.OneS.DataModel;
    using ITIS = DatEx.Creatio.DataModel.ITIS;
    using Terrasoft = DatEx.Creatio.DataModel.Terrasoft.Base;
    using DatEx.Creatio.DataModel.Terrasoft.Base;
    using DatEx.Creatio.DataModel.ITIS;

    public class SyncObjs_SyncNomenclature
    {
        public Dictionary<Guid, OneS.Nomenclature> OneS_NomenclatureGroupsOrderedById { get; set; } = new Dictionary<Guid, OneS.Nomenclature>();

        public Dictionary<Guid, OneS.Nomenclature> OneS_NomenclatureItemsOrderedById { get; set; } = new Dictionary<Guid, OneS.Nomenclature>();

        public Dictionary<Guid, OneS.MeasureUnit> OneS_MeasureUnitsOrderedById { get; set; } = new Dictionary<Guid, OneS.MeasureUnit>();

        public Dictionary<Guid, OneS.CostArticle> OneS_CostArticlesOrderedById { get; set; } = new Dictionary<Guid, OneS.CostArticle>();

        public Dictionary<Guid, OneS.MeasureUnitsClassifier> OneS_MeassureUnitsClassifiersOrderedById { get; set; } = new Dictionary<Guid, OneS.MeasureUnitsClassifier>();



        public Dictionary<Guid, ITISNomenclatureGroups> Creatio_NomenclatureGroupsOrderedByOneSId { get; set; } = new Dictionary<Guid, ITISNomenclatureGroups>();

        public Dictionary<Guid, ITISCompaniesNomenclature> Creatio_NomenclatureItemsOrderedByOneSId { get; set; } = new Dictionary<Guid, ITISCompaniesNomenclature>();

        public Dictionary<Guid, ITIS.Unit> Creatio_MeasureUnitsOrderedByOneSId { get; set; } = new Dictionary<Guid, ITIS.Unit>();

        public Dictionary<Guid, ITIS.ITISPurchasingArticle> Creatio_CostArticlesOrderedByOneSId { get; set; } = new Dictionary<Guid, ITIS.ITISPurchasingArticle>();
    }
}
