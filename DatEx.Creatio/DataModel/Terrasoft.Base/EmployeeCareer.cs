
namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;
    using Newtonsoft.Json;

    /// <summary> Карьера сотрудника в нашей компании </summary>
    [CreatioType("Карьера сотрудника в нашей компании")]
    public class EmployeeCareer : BaseEntity
    {
        /// <summary> Сотрудник (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [MapRemarks("Объект Сотрудник должен быть уже синхронизирован с 1С, здесь же необходимо найти ранее созданный объект по его ITISOneSId и присвоить его Id в Creatio")]
        [Map(true)]
        [CreatioProp("Сотрудник (Id)", Color = ConsoleColor.Blue)]
        public Guid? EmployeeId { get; set; }

        /// <summary> Сотрудник </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Сотрудник", Color = ConsoleColor.Yellow)]
        public Employee Employee { get; set; }

        /// <summary> Контрагент (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [MapRemarks("Объект Контрагентдолжен быть уже синхронизирован с 1С, здесь же необходимо найти ранее созданный объект по его ITISOneSId и присвоить его Id в Creatio")]
        [Map(true)]
        [CreatioProp("Контрагент (Id)", Color = ConsoleColor.Blue)]
        public Guid? AccountId { get; set; }

        /// <summary> Контрагент </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Контрагент", Color = ConsoleColor.Yellow)]
        public Account Account { get; set; }

        /// <summary> Подразделение (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [MapRemarks("Объект Подразделение должен быть уже синхронизирован с 1С, здесь же необходимо найти ранее созданный объект по его ITISOneSId и присвоить его Id в Creatio")]
        [Map(true)]
        [CreatioProp("Подразделение (Id)", Color = ConsoleColor.Blue)]
        public Guid? OrgStructureUnitId { get; set; }

        /// <summary> Подразделение </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Подразделение", Color = ConsoleColor.Yellow)]
        public OrgStructureUnit OrgStructureUnit { get; set; }

        /// <summary> Должность (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [MapRemarks("Объект Должность сотрудника должен быть уже синхронизирован с 1С, здесь же необходимо найти ранее созданный объект по его ITISOneSId и присвоить его Id в Creatio")]
        [Map(true)]
        [CreatioProp("Должность (Id)", Color = ConsoleColor.Blue)]
        public Guid? EmployeeJobId { get; set; }

        /// <summary> Должность </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Должность", Color = ConsoleColor.Yellow)]
        public ITIS.EmployeeJob EmployeeJob { get; set; }

        /// <summary> Полное название должности </summary>
        [Map(true, DataType.Lookup, "Catalog_ДолжностиОрганизаций", DataType.String, "Description")]
        [CreatioProp("Полное название должности", Color = ConsoleColor.Yellow)]
        public String FullJobTitle { get; set; }

        /// <summary> Начало </summary>
        [JsonConverter(typeof(JsonConverter_Date))]
        [Map(true, DataType.Lookup, "Catalog_СотрудникиОрганизаций", DataType.Date, "ДатаПриемаНаРаботу")]
        [CreatioProp("Начало", Color = ConsoleColor.Yellow)]
        public DateTime? StartDate { get; set; }

        /// <summary> Завершение </summary>
        [Map(true, DataType.Lookup, "Catalog_СотрудникиОрганизаций", DataType.Date, "ДатаУвольнения")]
        [JsonConverter(typeof(JsonConverter_Date))]
        [CreatioProp("Завершение", Color = ConsoleColor.Yellow)]
        public DateTime? DueDate { get; set; }

        /// <summary> Текущее </summary>
        [MapRemarks("Если дата окончания равна значению по умолчанию или null то true, иначе false; ? При попытке установить значение true - Internal server error, либо же система не реагирует никак")]
        [Map(false, DataType.Lookup, "Catalog_СотрудникиОрганизаций", DataType.Date, "ДатаОкончания")]
        [CreatioProp("Текущее", Color = ConsoleColor.Yellow)]
        public Boolean? IsCurrent { get; set; }

        /// <summary> Испытательный срок до </summary>
        [JsonConverter(typeof(JsonConverter_Date))]
        [MapRemarks("? Используеться ли испытательный срок в 1С, если да то в каких ед. измерения дни или месяцы")]
        [Map(false, DataType.Lookup, "Catalog_СотрудникиОрганизаций", DataType.Date, "ИспытательныйСрок")]
        [CreatioProp("Испытательный срок до", Color = ConsoleColor.Yellow)]
        public DateTime? ProbationDueDate { get; set; }

        /// <summary> Причина увольнения (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Причина увольнения (Id)", Color = ConsoleColor.Blue)]
        public Guid? ReasonForDismissalId { get; set; }

        /// <summary> Причина увольнения </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Причина увольнения", Color = ConsoleColor.Yellow)]
        public ReasonForLeaving ReasonForDismissal { get; set; }
    }
}
