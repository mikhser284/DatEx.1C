﻿namespace App
{
    using System;
    using System.Collections.Generic;
    using OneS = DatEx.OneS.DataModel;
    using ITIS = DatEx.Creatio.DataModel.ITIS;
    using Terrasoft = DatEx.Creatio.DataModel.Terrasoft.Base;
    using DatEx.Creatio.DataModel.Terrasoft.Base;

    public class SyncObjs_SyncNomenclature
    {
        public Dictionary<Guid, OneS.Nomenclature> OneS_NomenclatureGroupsById { get; set; } = new Dictionary<Guid, OneS.Nomenclature>();

        public Dictionary<Guid, OneS.Nomenclature> OneS_NomenclatureItemsById { get; set; } = new Dictionary<Guid, OneS.Nomenclature>();
    }
}
