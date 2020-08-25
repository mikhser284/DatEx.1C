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
        [CreatioProp("Guid", "Подразделение (Id)")]
        public Guid? ITISSubdivisionId { get; set; }

        /// <summary> Подразделение </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Guid", "Подразделение")]
        public OrgStructureUnit ITISSubdivision { get; set; }

        /// <summary> Вид занятости (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Guid", "Вид занятости (Id)")]
        public Guid? ITISEmploymentTypeId { get; set; }

        /// <summary> Вид занятости (Id) </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Guid", "Вид занятости (Id)")]
        public ITIS.ITISEmploymentType ITISEmploymentType { get; set; }

        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Идентификатор объекта в 1С", Remarks = "Поле отсутствует в оригинальном пакете 'ItisWorkFlowBase'", Color = ConsoleColor.Red)]
        public Guid? ITISOneSId { get; set; }
    }
}
