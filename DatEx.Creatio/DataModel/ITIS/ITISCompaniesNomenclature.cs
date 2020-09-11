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
        [Map()]
        [CreatioProp("String", "Название")]
        public String ITISName { get; set; }



        /// <summary> Заметки </summary>
        [Map()]
        [CreatioProp("Строка неограниченной длины", "Заметки")]
        public String ITISNotes { get; set; }



        /// <summary> Тип номенклатуры_Не используется (Id) </summary>
        [JsonIgnoreSerialization]
        [Obsolete("Мусорное свойство", true)]
        [ObsoleteCreatioProp("Тип номенклатуры_Не используется (Id)")]
        public Guid? ITITISNomenclatureTypeId { get; set; }



        /// <summary> Тип номенклатуры_Не используется </summary>
        [JsonIgnoreSerialization]
        [Obsolete("Мусорное свойство", true)]
        [ObsoleteCreatioProp("Тип номенклатуры_Не используется")]
        public Object ITITISNomenclatureType { get; set; }



        /// <summary> Вид номенклатуры_Не используется (Id) </summary>
        [JsonIgnoreSerialization]
        [Obsolete("Мусорное свойство", true)]
        [ObsoleteCreatioProp("Вид номенклатуры_Не используется (Id)")]
        public Guid? ITISNomenclatureKindId { get; set; }



        /// <summary> Вид номенклатуры_Не используется </summary>
        [JsonIgnoreSerialization]
        [Obsolete("Мусорное свойство", true)]
        [ObsoleteCreatioProp("Вид номенклатуры_Не используется")]
        public Object ITISNomenclatureKind { get; set; }



        /// <summary> Номенклатурная группа_Не используется (Id) </summary>
        [JsonIgnoreSerialization]
        [Obsolete("Мусорное свойство", true)]
        [ObsoleteCreatioProp("Номенклатурная группа_Не используется (Id)")]
        public Guid? ITISNomenclatureGroupId { get; set; }



        /// <summary> Номенклатурная группа_Не используется </summary>
        [JsonIgnoreSerialization]
        [Obsolete("Мусорное свойство", true)]
        [ObsoleteCreatioProp("Номенклатурная группа_Не используется")]
        public Object ITISNomenclatureGroup { get; set; }



        /// <summary> Единица измерения номенклатуры </summary>
        [Map()]
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Guid?", "Единица измерения номенклатуры (Id)")]
        public Guid? ITISNomenclatureUnitOfMeasurementId { get; set; }



        /// <summary> Единица измерения номенклатуры </summary>
        [Map()]
        [JsonIgnoreSerialization]
        [CreatioProp("Единица измерения номенклатуры")]
        public Terrasoft.Unit ITISNomenclatureUnitOfMeasurement { get; set; }



        /// <summary> Ответственный (Id) </summary>
        [Map()]
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Guid?", "Ответственный (Id)")]
        public Guid? ITISOwnerId { get; set; }



        /// <summary> Ответственный </summary>
        [Map()]
        [JsonIgnoreSerialization]
        [CreatioProp("Ответственный")]
        public ITIS.Contact ITISOwner { get; set; }



        /// <summary> Код 1С_Не используется (Id) </summary>
        [JsonIgnoreSerialization]
        [Obsolete("Мусорное свойство - неверно задан тип данных: String, а не Guid", true)]
        [ObsoleteCreatioProp("Код 1С_Не используется (Id)")]
        public String ITISOneCCode { get; set; }



        /// <summary> Деактивирована </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Деактивирована")]
        public Boolean? RecordInactive { get; set; }



        /// <summary> Статья закупок (Id) </summary>
        [Map()]
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Guid?", "Статья закупок (Id)", Color = ConsoleColor.Blue)]
        public Guid? ITISPurchasingArticleId { get; set; }



        /// <summary> Статья закупок </summary>
        [Map()]
        [JsonIgnoreSerialization]
        [CreatioProp("Статья закупок", Color = ConsoleColor.Yellow)]
        public ITISPurchasingArticle ITISPurchasingArticle { get; set; }




        /// <summary> Группы номенклатуры (Id) </summary>
        [Map()]
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Guid?", "Группы номенклатуры (Id)", Color = ConsoleColor.Blue)]
        public Guid? ITISNomenclaturesGroupId { get; set; }



        /// <summary> Группы номенклатуры </summary>
        [Map()]
        [JsonIgnoreSerialization]
        [CreatioProp("Группы номенклатуры", Color = ConsoleColor.Yellow)]
        public ITISNomenclatureGroups ITISNomenclaturesGroup { get; set; }



        /// <summary> Id объекта в 1C </summary>
        [Map()]
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioPropNotExistInDataModelOfITIS]
        [CreatioProp("Guid?", "Id объекта в 1C", Color = ConsoleColor.Red)]
        public Guid? ITISOneSId { get; set; }


    }
}
