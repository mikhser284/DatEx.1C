namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;

    /// <summary> Тип средства связи </summary>
    [CreatioType("Тип средства связи")]
    public class CommunicationType : BaseImageLookup
    {
        /// <summary> Шаблон для формирования гиперссылки в реестре </summary>
        public String HyperlinkTemplate { get; set; }

        /// <summary> Использовать для контрагентов </summary>
        public Boolean UseforAccounts { get; set; }

        /// <summary> Использовать для контактов </summary>
        public Boolean UseforContacts { get; set; }
    }
}
