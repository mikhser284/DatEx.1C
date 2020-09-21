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
        [MapRemarks("Значение береться из объекта настроек, это проекция булевого значения на справочник Юридический статус контрагента. \nЕсли true - Id записи Резидент, иначе записи Не резидент. \nКогда выполняеться синхронизация Catalog_Организации (во время синхронизации сотрудников и их карьеры) - проекция для значения true")]
        [MapAttribute(true, DataType.Lookup, "Catalog_Организации", DataType.Bool, "#true")]
        [MapAttribute(false, DataType.Lookup, "Catalog_Контрагенты", DataType.Bool, "НеЯвляетсяРезидентом")]
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
