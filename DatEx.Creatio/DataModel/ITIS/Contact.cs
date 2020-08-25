namespace DatEx.Creatio.DataModel.ITIS
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;
    using Terrasoft = DatEx.Creatio.DataModel.Terrasoft.Base;
    using ITIS = DatEx.Creatio.DataModel.ITIS;
    using Newtonsoft.Json;

    [CreatioType("Контакт")]
    /// <summary> Контакт ITIS </summary>
    public class Contact : Terrasoft.Contact
    {
        /// <summary> 1C Id </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioPropNotExistInDataModelOfITIS]
        [MapFromOneSProp(OneSDataTypeKind.Lookup, "Catalog_ФизическиеЛица", "Guid", "Id")]
        [CreatioProp("Guid", "Id объекта в 1С", Color = ConsoleColor.Red)]
        public Guid? ITISOneSId { get; set; }

        /// <summary> Деактивирована </summary>
        [CreatioProp("Деактивирована")]
        public Boolean? RecordInactive { get; set; }

        /// <summary> ИНН (ДРФО) </summary>
        [CreatioProp("ИНН (ДРФО)")]
        public String ITISIndividualTaxCode { get; set; }

        /// <summary> Должность сотрудника (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Guid", "Должность сотрудника (Id)", Color = ConsoleColor.Blue)]
        public Guid? ITISEmployeePositionId { get; set; }

        /// <summary> Должность сотрудника </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Должность сотрудника", Color = ConsoleColor.Yellow)]
        public ITIS.EmployeeJob ITISEmployeePosition { get; set; }

        /// <summary> Подразделение (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Guid", "Подразделение (Id)", Color = ConsoleColor.Blue)]
        public Guid? ITISSubdivisionId { get; set; }

        /// <summary> Подразделение </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Подразделение", Color = ConsoleColor.Yellow)]
        public Terrasoft.AccountOrganizationChart ITISSubdivision { get; set; }

        /// <summary> Дата начала карьеры </summary>
        [JsonConverter(typeof(JsonConverter_Date))]
        [CreatioProp("Дата начала карьеры", Color = ConsoleColor.Yellow)]
        public DateTime? ITISCareeStartDate { get; set; }

        /// <summary> Дата начала карьеры </summary>
        [CreatioPropNotExistInDataModelOfITIS]
        [JsonConverter(typeof(JsonConverter_Date))]
        [CreatioProp("Дата завершения карьеры", Color = ConsoleColor.Yellow)]
        public DateTime? ITISCareerEndDate { get; set; }

        /// <summary> Вид занятости (Id) </summary>
        [CreatioProp("Guid", "Вид занятости (Id)", Color = ConsoleColor.Blue)]
        [JsonConverter(typeof(JsonConverter_Guid))]
        public Guid? ITISEmploymentTypeId { get; set; }

        /// <summary> Вид занятости </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Вид занятости", Color = ConsoleColor.Yellow)]
        public ITISEmploymentType ITISEmploymentType { get; set; }

        /// <summary> Подразделение организации (Id) </summary>
        [CreatioProp("Guid", "Подразделение организации (Id)", Color = ConsoleColor.Blue)]
        [JsonConverter(typeof(JsonConverter_Guid))]
        public Guid? ITISOrganizationSubdivisionId { get; set; }

        /// <summary> Подразделение организации </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Подразделение организации", Color = ConsoleColor.Yellow)]
        public Terrasoft.OrgStructureUnit ITISOrganizationSubdivision { get; set; }
    }
}
