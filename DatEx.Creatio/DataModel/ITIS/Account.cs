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

        [CreatioProp("Юридический статус контрагента (Id)", Color = ConsoleColor.Blue)]
        public Guid ITISCOunterpartyLegalStatusId { get; set; }

        [Map]
        [CreatioProp("Юридический статус контрагента", Color = ConsoleColor.Yellow)]
        public ITISCounterpartyLegalStatus ITISCOunterpartyLegalStatus { get; set; }

        [CreatioProp("Деактивирована")]
        public Boolean RecordInactive { get; set; }

        [Map]
        [CreatioProp("Внутренний код", Color = ConsoleColor.Yellow)]
        public String ITISInternalCode { get; set; }

        [Map]
        [CreatioProp("Id Контрагента в системе 1С", Color = ConsoleColor.Red)]
        public Guid ITISOneSId { get; set; }
    }
}
