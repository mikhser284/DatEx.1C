namespace DatEx.Creatio.DataModel.ITIS
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;
    using Terrasoft = DatEx.Creatio.DataModel.Terrasoft.Base;
    using ITIS = DatEx.Creatio.DataModel.ITIS;
    using Newtonsoft.Json;

    [CreatioType("Контрагент")]
    public class Account : Terrasoft.Account
    {
        [CreatioProp("Email")]
        public String ITISEmail { get; set; }

        [JsonConverter(typeof(JsonConverter_Guid))]
        [MapAttribute(true, DataType.Lookup, "Catalog_Организации", DataType.Lookup, "#True")]
        [CreatioProp("Юридический статус контрагента (Id)", Color = ConsoleColor.Blue)]
        public Guid? ITISCounterpartyLegalStatusId { get; set; }

        [CreatioProp("Юридический статус контрагента", Color = ConsoleColor.Yellow)]
        public ITISCounterpartyLegalStatus ITISCOunterpartyLegalStatus { get; set; }

        [CreatioProp("Деактивирована")]
        public Boolean? RecordInactive { get; set; }

        [Map(true, DataType.Lookup, "Catalog_Организации", DataType.String, "Префикс")]
        [CreatioProp("Внутренний код", Color = ConsoleColor.Yellow)]
        public String ITISInternalCode { get; set; }

        [JsonConverter(typeof(JsonConverter_Guid))]
        [Map(true, DataType.Lookup, "Catalog_Организации", DataType.Guid, "Ref_Key")]
        [CreatioProp("Id Контрагента в системе 1С", Color = ConsoleColor.Red)]
        public Guid? ITISOneSId { get; set; }
    }
}
