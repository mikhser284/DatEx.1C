namespace DatEx.Creatio.DataModel.ITIS
{
    using DatEx.Creatio.DataModel.Auxilary;
    using DatEx.Creatio.DataModel.Terrasoft.Base;
    using Newtonsoft.Json;
    using System;
    using Terrasoft = DatEx.Creatio.DataModel.Terrasoft.Base;

    /// <summary> Карьера контакта </summary>
    [CreatioType("Контакт")]
    public class ContactCareer : Terrasoft.ContactCareer
    {
        /// <summary> Подразделение (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Guid", "Подразделение (Id)", Color = ConsoleColor.Blue)]
        public Guid? ITISSubdivisionId { get; set; }

        /// <summary> Подразделение </summary>
        [JsonIgnoreSerialization]
        [Map]
        [CreatioProp("Guid", "Подразделение", Color = ConsoleColor.Yellow)]
        public OrgStructureUnit ITISSubdivision { get; set; }

        /// <summary> Вид занятости (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Guid", "Вид занятости (Id)", Color = ConsoleColor.Yellow)]
        public Guid? ITISEmploymentTypeId { get; set; }

        /// <summary> Вид занятости (Id) </summary>
        [JsonIgnoreSerialization]
        [Map]
        [CreatioProp("Guid", "Вид занятости", Color = ConsoleColor.Blue)]
        public ITIS.ITISEmploymentType ITISEmploymentType { get; set; }

        [JsonConverter(typeof(JsonConverter_Guid))]
        [Map]
        [CreatioPropNotExistInDataModelOfITIS]
        [CreatioProp("Идентификатор объекта в 1С", Color = ConsoleColor.Red)]
        public Guid? ITISOneSId { get; set; }
    }
}
