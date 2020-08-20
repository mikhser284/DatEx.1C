namespace DatEx.Creatio.DataModel.ITIS
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;
    using Terrasoft = DatEx.Creatio.DataModel.Terrasoft.Base;
    using ITIS = DatEx.Creatio.DataModel.ITIS;

    [CreatioType("Контрагент")]
    public class Account : Terrasoft.Account
    {
        [CreatioProp("Email")]
        public String ITISEmail { get; set; }

        [CreatioProp("Юридический статус контрагента (Id)")]
        public Guid ITISCOunterpartyLegalStatusId { get; set; }

        [CreatioProp("Юридический статус контрагента")]
        public ITISCounterpartyLegalStatus ITISCOunterpartyLegalStatus { get; set; }

        [CreatioProp("Деактивирована")]
        public Boolean RecordInactive { get; set; }

        [CreatioProp("Внутренний код")]
        public String ITISInternalCode { get; set; }

        [CreatioProp("Id Контрагента в системе 1С")]
        public Guid ITISOneSId { get; set; }
    }
}
