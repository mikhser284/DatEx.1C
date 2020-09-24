namespace DatEx.Creatio.DataModel.ITIS
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;
    using Newtonsoft.Json;
    using Terrasoft = DatEx.Creatio.DataModel.Terrasoft.Base;


    /// <summary> Группы номенклатуры </summary>
    [CreatioType("Группы номенклатуры")]
    public class ITISNomenclatureGroups : Terrasoft.BaseEntity
    {
        /// <summary> Название </summary>
        [Map(true, DataType.Lookup, "Catalog_Номенклатура", DataType.String, "Description")]
        [CreatioProp("Название")]
        public String ITISName { get; set; }



        /// <summary> Заметки </summary>
        [Map(true, DataType.Lookup, "Catalog_Номенклатура", DataType.String, "Комментарий")]
        [CreatioProp("Заметки")]
        public String ITISNotes { get; set; }



        /// <summary> Родительская група Id </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [MapRemarks("Номенклатурные группы в Creatio уже созданы. "
            + "\n1. По ITISOneSId получаем соответствующий объект Catalog_Номенклатура (из 1С) "
            + "\n2. По значению свойства Parent_Key получаем его родительский объект Catalog_Номенклатура (из 1С)"
            + "\n3. Получаем запись справочника ITISNomenclatureGroups (Creatio) у которого ITISOneSId == Ref_Key объекта Catalog_Номенклатура (из 1С) найденного на стадии 2"
            + "\n4. Свойству ITISParentGroupId присваем значение Id объекта ITISNomenclatureGroups найденного на стадии 3")]
        [Map(true)]
        [CreatioProp("Родительская група Id")]
        public Guid? ITISParentGroupId { get; set; }




        /// <summary> Родительская група </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Родительская група")]
        public ITISNomenclatureGroups ITISParentGroup { get; set; }



        /// <summary> Ответственный за проверку (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Ответственный за проверку Id")]
        public Guid? ITISResponsibleForVerificationId { get; set; }



        /// <summary> Ответственный за проверку </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Ответственный за проверку")]
        public Contact ITISResponsibleForVerification { get; set; }



        /// <summary> Код 1С </summary>
        [JsonIgnoreSerialization]
        [Obsolete("У свойства Код 1С не верно задан тип данных - String в место Guid", true)]
        [ObsoleteCreatioProp("Код 1С", Remarks = "У свойства Код 1С не верно задан тип данных - String в место Guid")]
        [CreatioProp("Строка",  "Код 1С")]
        public String ITISOneCCode { get; set; }



        /// <summary> Деактивирована </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Деактивирована")]
        public Boolean? RecordInactive { get; set; }



        /// <summary> Id объекта в 1C </summary>
        [Map(true, DataType.Lookup, "Catalog_Номенклатура", DataType.Guid, "Ref_Key")]
        [CreatioPropNotExistInDataModelOfITIS]
        [CreatioProp("Guid", "Id объекта в 1C", Color = ConsoleColor.Red)]
        public Guid ITISOneSId { get; set; }

    }
}
