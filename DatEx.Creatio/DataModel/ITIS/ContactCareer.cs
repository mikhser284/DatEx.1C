namespace DatEx.Creatio.DataModel.ITIS
{
    using DatEx.Creatio.DataModel.Auxilary;
    using DatEx.Creatio.DataModel.Terrasoft.Base;
    using Newtonsoft.Json;
    using System;
    using Terrasoft = DatEx.Creatio.DataModel.Terrasoft.Base;

    /// <summary> Карьера контакта </summary>
    [CreatioType("Карьера контакта")]
    public class ContactCareer : Terrasoft.ContactCareer
    {
        /// <summary> Подразделение (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [Map]
        [CreatioProp("Guid", "Подразделение (Id)", Color = ConsoleColor.Blue)]
        public Guid? ITISSubdivisionId { get; set; }

        /// <summary> Подразделение </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Guid", "Подразделение", Color = ConsoleColor.Yellow)]
        public OrgStructureUnit ITISSubdivision { get; set; }

        /// <summary> Вид занятости (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [MapRemarks("Значение преобразуется в Id объекта Creatio используя словарь Map_OneSEnum_EmploymentType_CreatioGuidOf_EmploymentType из объекта настроек синхронизации")]
        [Map(true, DataType.Lookup, "Catalog_СотрудникиОрганизаций", DataType.String, "ВидЗанятости")]
        [CreatioProp("Guid", "Вид занятости (Id)", Color = ConsoleColor.Yellow)]
        public Guid? ITISEmploymentTypeId { get; set; }

        /// <summary> Вид занятости (Id) </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Guid", "Вид занятости", Color = ConsoleColor.Blue)]
        public ITIS.ITISEmploymentType ITISEmploymentType { get; set; }

        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioPropNotExistInDataModelOfITIS]
        [Map(true, DataType.Lookup, "Catalog_СотрудникиОрганизаций", DataType.Guid, "Ref_Key")]
        [CreatioProp("Идентификатор объекта в 1С", Color = ConsoleColor.Red)]
        public Guid? ITISOneSId { get; set; }
    }
}
