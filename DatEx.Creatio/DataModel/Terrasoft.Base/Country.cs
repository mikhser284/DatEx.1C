namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;

    /// <summary> Страна </summary>
    [CreatioType("Страна")]
    public class Country : BaseImageLookup
    {
        /// <summary> Платежные реквизиты </summary>
        public String BillingInfo { get; set; }

        /// <summary> Часовой пояс </summary>
        public TimeZone TimeZone { get; set; }

        /// <summary> Код стараны </summary>
        public String Code { get; set; }
    }
}
