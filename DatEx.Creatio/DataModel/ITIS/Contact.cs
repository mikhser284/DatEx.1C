namespace DatEx.Creatio.DataModel.ITIS
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;
    using Terrasoft = DatEx.Creatio.DataModel.Terrasoft.Base;
    using ITIS = DatEx.Creatio.DataModel.ITIS;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    [CreatioType("Контакт")]
    /// <summary> Контакт ITIS </summary>
    public class Contact : Terrasoft.Contact
    {
        [JsonIgnoreSerialization]
        public List<ITIS.ContactCareer> Career { get; set; } = new List<ContactCareer>();

        /// <summary> 1C Id </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioPropNotExistInDataModelOfITIS]
        [Map(true, DataType.Lookup, "Catalog_ФизическиеЛица", DataType.Guid, "Id")]
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
        [MapRemarks("Объект Должность сотрудника должен быть уже синхронизирован с 1С, здесь же необходимо найти ранее созданный объект по его ITISOneSId и присвоить его Id в Creatio")]
        [Map(true)]
        [CreatioProp("Guid", "Должность сотрудника (Id)", Color = ConsoleColor.Blue)]
        public Guid? ITISEmployeePositionId { get; set; }

        /// <summary> Должность сотрудника </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Должность сотрудника", Color = ConsoleColor.Yellow)]
        public ITIS.EmployeeJob ITISEmployeePosition { get; set; }

        /// <summary> Подразделение (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [MapRemarks("Объект Структура организации контрагента должен быть уже синхронизирован с 1С, здесь же необходимо найти ранее созданный объект по его ITISOneSId и присвоить его Id в Creatio")]
        [Map(true)]
        [CreatioProp("Guid", "Подразделение (Id)", Color = ConsoleColor.Blue)]
        public Guid? ITISSubdivisionId { get; set; }

        /// <summary> Подразделение </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Подразделение", Color = ConsoleColor.Yellow)]
        public Terrasoft.AccountOrganizationChart ITISSubdivision { get; set; }

        /// <summary> Дата начала карьеры </summary>
        [JsonConverter(typeof(JsonConverter_Date))]
        [Map(true, DataType.Lookup, "Catalog_СотрудникиОрганизаций", DataType.Date, "ДатаПриемаНаРаботу")]
        [CreatioProp("Дата начала карьеры", Color = ConsoleColor.Yellow)]
        public DateTime? ITISCareeStartDate { get; set; }

        /// <summary> Дата завершения карьеры </summary>
        [JsonConverter(typeof(JsonConverter_Date))]
        [CreatioPropNotExistInDataModelOfITIS]
        [MapRemarks("Если у сотрудника есть хоть одна актуальная должность это поле пусто")]
        [Map(true, DataType.Lookup, "Catalog_СотрудникиОрганизаций", DataType.Date, "ДатаУвольнения")]
        [CreatioProp("Дата завершения карьеры", Color = ConsoleColor.Yellow)]
        public DateTime? ITISCareerEndDate { get; set; }

        /// <summary> Вид занятости (Id) </summary>
        [MapRemarks("Значение берется из объекта настроек синхронизации")]
        [Map(true, DataType.Lookup, "Catalog_СотрудникиОрганизаций", DataType.Enum, "ВидЗанятости(ВидыЗанятостиВОрганизации)")]
        [CreatioProp("Guid", "Вид занятости (Id)", Color = ConsoleColor.Blue)]
        [JsonConverter(typeof(JsonConverter_Guid))]
        public Guid? ITISEmploymentTypeId { get; set; }

        /// <summary> Вид занятости </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Вид занятости", Color = ConsoleColor.Yellow)]
        public ITISEmploymentType ITISEmploymentType { get; set; }

        /// <summary> Подразделение организации (Id) </summary>
        [MapRemarks("Объект Элемент организационной структуры должен быть уже синхронизирован с 1С, здесь же необходимо найти ранее созданный объект по его ITISOneSId и присвоить его Id в Creatio")]
        [Map(true)]
        [CreatioProp("Guid", "Подразделение организации (Id)", Color = ConsoleColor.Blue)]
        [JsonConverter(typeof(JsonConverter_Guid))]
        public Guid? ITISOrganizationSubdivisionId { get; set; }

        /// <summary> Подразделение организации </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Подразделение организации", Color = ConsoleColor.Yellow)]
        public Terrasoft.OrgStructureUnit ITISOrganizationSubdivision { get; set; }
    }
}
