using System;

namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    /// <summary> Годовой оборот контрагента </summary>
    public class AccountAnnualRevenue : BaseLookup
    {
        /// <summary> От, базовая валюта </summary>
        public Int32 FromBaseCurrency { get; set; }

        /// <summary> До, базовая валюта </summary>
        public Int32 ToBaseCurrency { get; set; }
    }
}
