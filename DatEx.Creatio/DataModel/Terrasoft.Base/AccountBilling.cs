namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;
    
    /// <summary> Платежный реквизит контрагента </summary>
    [CreatioType("Платежный реквизит контрагента")]
    public class AccountBilling : BaseLookup
    {
        /// <summary> Контрагент </summary>
        public Account Account { get; set; }

        /// <summary> Страна </summary>
        public Country Country { get; set; }

        /// <summary> Платежные реквизиты </summary>
        public String BillingInfo { get; set; }

        /// <summary> Руководитель </summary>
        public Contact AccountManager { get; set; }

        /// <summary> Главный бухгалтер </summary>
        public Contact ChiefAccountant { get; set; }

        /// <summary> Юридическое лицо </summary>
        public String LegalUnit { get; set; }
    }
}
