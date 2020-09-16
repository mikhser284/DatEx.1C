namespace App
{
    using System;
    using System.Collections.Generic;
    using OneS = DatEx.OneS.DataModel;
    using ITIS = DatEx.Creatio.DataModel.ITIS;
    using Terrasoft = DatEx.Creatio.DataModel.Terrasoft.Base;
    using DatEx.Creatio.DataModel.Terrasoft.Base;

    public class SyncObjs_SyncNomenclature
    {
        public Dictionary<Guid, OneS.Nomenclature> OneS_NomenclatureGroupsOrderedById { get; set; } = new Dictionary<Guid, OneS.Nomenclature>();

        public Dictionary<Guid, OneS.Nomenclature> OneS_NomenclatureItemsOrderedById { get; set; } = new Dictionary<Guid, OneS.Nomenclature>();

        public Dictionary<Guid, OneS.MeasureUnit> OneS_MeasureUnitsOrderedById { get; set; } = new Dictionary<Guid, OneS.MeasureUnit>();

        public Dictionary<Guid, OneS.CostItem> OneS_CostItemsOrderedById { get; set; } = new Dictionary<Guid, OneS.CostItem>();
    }
}
