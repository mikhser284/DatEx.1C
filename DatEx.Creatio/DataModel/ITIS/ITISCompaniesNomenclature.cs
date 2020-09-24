namespace DatEx.Creatio.DataModel.ITIS
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;
    using Newtonsoft.Json;
    using Terrasoft = DatEx.Creatio.DataModel.Terrasoft.Base;

    /// <summary> Номенклатура </summary>
    [CreatioType("Номенклатура")]
    public class ITISCompaniesNomenclature : Terrasoft.BaseEntity
    {
        /// <summary> Название </summary>
        [Map(true, DataType.Lookup, "Catalog_Номенклатура", DataType.String, "Description")]
        [CreatioProp("String", "Название")]
        public String ITISName { get; set; }



        /// <summary> Заметки </summary>
        [Map(true, DataType.Lookup, "Catalog_Номенклатура", DataType.String, "Комментарий")]
        [CreatioProp("Строка неограниченной длины", "Заметки")]
        public String ITISNotes { get; set; }



        /// <summary> Тип номенклатуры_Не используется (Id) </summary>
        [JsonIgnoreSerialization]
        [Obsolete("Мусорное свойство", true)]
        [ObsoleteCreatioProp("Тип номенклатуры_Не используется (Id)", Remarks = "Мусорное свойство")]
        [CreatioProp("Тип номенклатуры_Не используется (Id)")]
        public Guid? ITITISNomenclatureTypeId { get; set; }



        /// <summary> Тип номенклатуры_Не используется </summary>
        [JsonIgnoreSerialization]
        [Obsolete("Мусорное свойство", true)]
        [ObsoleteCreatioProp("Тип номенклатуры_Не используется", Remarks = "Мусорное свойство")]
        [CreatioProp("Тип номенклатуры_Не используется")]
        public Object ITITISNomenclatureType { get; set; }



        /// <summary> Вид номенклатуры_Не используется (Id) </summary>
        [JsonIgnoreSerialization]
        [Obsolete("Мусорное свойство", true)]
        [ObsoleteCreatioProp("Вид номенклатуры_Не используется (Id)", Remarks = "Мусорное свойство")]
        [CreatioProp("Вид номенклатуры_Не используется (Id)")]
        public Guid? ITISNomenclatureKindId { get; set; }



        /// <summary> Вид номенклатуры_Не используется </summary>
        [JsonIgnoreSerialization]
        [Obsolete("Мусорное свойство", true)]
        [ObsoleteCreatioProp("Вид номенклатуры_Не используется", Remarks = "Мусорное свойство")]
        [CreatioProp("Вид номенклатуры_Не используется")]
        public Object ITISNomenclatureKind { get; set; }



        /// <summary> Номенклатурная группа_Не используется (Id) </summary>
        [JsonIgnoreSerialization]
        [Obsolete("Мусорное свойство", true)]
        [ObsoleteCreatioProp("Номенклатурная группа_Не используется (Id)", Remarks = "Мусорное свойство")]
        [CreatioProp("Номенклатурная группа_Не используется (Id)")]
        public Guid? ITISNomenclatureGroupId { get; set; }



        /// <summary> Номенклатурная группа_Не используется </summary>
        [JsonIgnoreSerialization]
        [Obsolete("Мусорное свойство", true)]
        [ObsoleteCreatioProp("Номенклатурная группа_Не используется", Remarks = "Мусорное свойство")]
        [CreatioProp("Номенклатурная группа_Не используется")]
        public Object ITISNomenclatureGroup { get; set; }



        /// <summary> Единица измерения номенклатуры </summary>
        [MapRemarks("Полный маппинг для этого свойства выглядит следующим образом"
            + "\n[Справочник] Catalog_Номенклатура → [Ссылка на Справочник.ЕдиницыИзмерения] ЕдиницаХраненияОстатков"
            + "\n → [Ссылка на Справочник.КлассификаторЕдиницИзмерения] ЕдиницаПоКлассификатору"
            + "\n → [Guid] Ref_Key")]
        [Map(true)]
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Guid?", "Единица измерения номенклатуры (Id)")]
        public Guid? ITISNomenclatureUnitOfMeasurementId { get; set; }



        /// <summary> Единица измерения номенклатуры </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Единица измерения номенклатуры")]
        public Terrasoft.Unit ITISNomenclatureUnitOfMeasurement { get; set; }



        /// <summary> Ответственный (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Guid?", "Ответственный (Id)")]
        public Guid? ITISOwnerId { get; set; }



        /// <summary> Ответственный </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Ответственный")]
        public ITIS.Contact ITISOwner { get; set; }



        /// <summary> Код 1С </summary>
        [CreatioProp("Код 1С")]
        public String ITISOneCCode { get; set; }



        /// <summary> Деактивирована </summary>
        [CreatioProp("Деактивирована")]
        public Boolean? RecordInactive { get; set; }



        /// <summary> Статья закупок (Id) </summary>
        [MapRemarks("Статьи закупки в Creatio уже должны быть предварительно синхронизированы"
            + "\n1. По ITISOneSId получаем соответствующий объект Catalog_Номенклатура (из 1С)"
            + "\n2. По значению свойства СтатьяЗатрат_Key получаем связанный объект Catalog_СтатьиЗатрат (из 1С)"
            + "\n3. Получаем объект Статья закупок (Creatio) у которого ITISOneSId == Ref_Key объекта полученного на стадии 2"
            + "\n4. Присваиваем полю ITISPurchasingArticleId значение поля Id объекта полученного на стадии 3")]
        [Map(true)]
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Guid?", "Статья закупок (Id)", Color = ConsoleColor.Blue)]
        public Guid? ITISPurchasingArticleId { get; set; }



        /// <summary> Статья закупок </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Статья закупок", Color = ConsoleColor.Yellow)]
        public ITISPurchasingArticle ITISPurchasingArticle { get; set; }




        /// <summary> Группы номенклатуры (Id) </summary>
        [MapRemarks("Номенклатурные группы в Creatio уже должны быть синхронизированны"
            + "\n1. 1. По ITISOneSId получаем соответствующий объект Catalog_Номенклатура (из 1С)" 
            + "\n2.По значению свойства Parent_Key получаем его родительский объект Catalog_Номенклатура(из 1С)"
            + "\n3.Получаем запись справочника ITISNomenclatureGroups(Creatio) у которого ITISOneSId == Ref_Key объекта Catalog_Номенклатура(из 1С) найденного на стадии 2"
            + "\n4.Свойству ITISNomenclaturesGroupId присваем значение Id объекта ITISNomenclatureGroups найденного на стадии 3 ")]
        [Map(true)]
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Guid?", "Группы номенклатуры (Id)", Color = ConsoleColor.Blue)]
        public Guid? ITISNomenclaturesGroupId { get; set; }



        /// <summary> Группы номенклатуры </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Группы номенклатуры", Color = ConsoleColor.Yellow)]
        public ITISNomenclatureGroups ITISNomenclaturesGroup { get; set; }



        /// <summary> Id объекта в 1C </summary>
        [Map(true, DataType.Lookup, "Catalog_Номенклатура", DataType.Guid, "Ref_Key")]
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioPropNotExistInDataModelOfITIS]
        [CreatioProp("Guid?", "Id объекта в 1C", Color = ConsoleColor.Red)]
        public Guid? ITISOneSId { get; set; }
    }
}
