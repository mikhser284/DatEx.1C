namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;
    
    /// <summary> Годовой оборот контрагента </summary>
    [CreatioType("Годовой оборот контрагента")]
    public class AccountAnnualRevenue : BaseLookup
    {
        /// <summary> От, базовая валюта </summary>
        public Int32 FromBaseCurrency { get; set; }

        /// <summary> До, базовая валюта </summary>
        public Int32 ToBaseCurrency { get; set; }
    }
}
