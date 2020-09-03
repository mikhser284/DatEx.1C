
namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;
    using Newtonsoft.Json;

    /// <summary> Карьера сотрудника в нашей компании </summary>
    [CreatioType("Карьера сотрудника в нашей компании")]
    public class ContactCareer : BaseEntity
    {
        /// <summary> Сотрудник (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [MapRemarks("Объект Сотрудник должен быть уже синхронизирован с 1С, здесь же необходимо найти ранее созданный объект по его ITISOneSId и присвоить его Id в Creatio")]
        [Map(true)]
        [CreatioProp("Контакт (Id)", Color = ConsoleColor.Blue)]
        public Guid? ContactId { get; set; }

        /// <summary> Сотрудник </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Контакт", Color = ConsoleColor.Yellow)]
        public Contact Contact { get; set; }

        /// <summary> Контрагент (Id) </summary>
        [CreatioProp("Контрагент (Id)", Color = ConsoleColor.Blue)]
        [MapRemarks("Объект Контрагентдолжен быть уже синхронизирован с 1С, здесь же необходимо найти ранее созданный объект по его ITISOneSId и присвоить его Id в Creatio")]
        [Map(true)]
        public Guid? AccountId { get; set; }

        /// <summary> Контрагент </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Контрагент", Color = ConsoleColor.Yellow)]
        public Account Account { get; set; }

        /// <summary> Подразделение (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [MapRemarks("Объект Подразделение должен быть уже синхронизирован с 1С, здесь же необходимо найти ранее созданный объект по его ITISOneSId и присвоить его Id в Creatio")]
        [Map(true)]
        [CreatioProp("Департамент (Id)", Color = ConsoleColor.Blue)]
        public Guid? OrgStructureUnitId { get; set; }

        /// <summary> Подразделение </summary>
        [JsonIgnoreSerialization]
        [Map]
        [CreatioProp("Департамент", Color = ConsoleColor.Yellow)]
        public OrgStructureUnit OrgStructureUnit { get; set; }

        /// <summary> Должность (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [MapRemarks("Объект Должность сотрудника должен быть уже синхронизирован с 1С, здесь же необходимо найти ранее созданный объект по его ITISOneSId и присвоить его Id в Creatio")]
        [Map(true)]
        [CreatioProp("Должность (Id)", Color = ConsoleColor.Blue)]
        public Guid? JobId { get; set; }

        /// <summary> Должность </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Должность", Color = ConsoleColor.Yellow)]
        public ITIS.Job Job { get; set; }

        /// <summary> Полное название должности </summary>
        [Map(true, DataType.Lookup, "Catalog_ДолжностиОрганизаций", DataType.String, "Description")]
        [CreatioProp("Полное название должности", Color = ConsoleColor.Yellow)]
        public String JobTitle { get; set; }

        /// <summary> Основное </summary>
        [MapRemarks("Если значение перечисления ВидыЗанятостиВОрганизации равно 'ОсновноеМестоРаботы' - true, иначе false")]
        [Map(true, DataType.Lookup, "Catalog_СотрудникиОрганизаций", DataType.Enum, "Перечисление.ВидыЗанятостиВОрганизации/ВидЗанятости")]
        [CreatioProp("Основное", Color = ConsoleColor.Yellow)]
        public Boolean Primary { get; set; }

        /// <summary> Текущее </summary>
        [MapRemarks("Если дата окончания равна значению по умолчанию или null то true, иначе false; ? При попытке установить значение true - Internal server error, либо же система не реагирует никак")]
        [Map(false, DataType.Lookup, "Catalog_СотрудникиОрганизаций", DataType.Date, "ДатаОкончания")]
        [CreatioProp("Текущее", Color = ConsoleColor.Yellow)]
        public Boolean Current { get; set; }

        /// <summary> Начало </summary>
        [JsonConverter(typeof(JsonConverter_Date))]
        [Map(true, DataType.Lookup, "Catalog_СотрудникиОрганизаций", DataType.Date, "ДатаПриемаНаРаботу")]
        [CreatioProp("Начало", Color = ConsoleColor.Yellow)]
        public DateTime? StartDate { get; set; }

        /// <summary> Завершение </summary>
        [JsonConverter(typeof(JsonConverter_Date))]
        [Map(true, DataType.Lookup, "Catalog_СотрудникиОрганизаций", DataType.Date, "ДатаУвольнения")]
        [CreatioProp("Завершение", Color = ConsoleColor.Yellow)]
        public DateTime? DueDate { get; set; }

        /// <summary> Причина смены места работы (Id) </summary>
        [CreatioProp("Причина смены места работы (Id)")]
        public Guid? JobChangeReasonId { get; set; }

        /// <summary> Причина смены места работы </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Причина смены места работы")]
        public JobChangeReason JobChangeReason { get; set; }

        /// <summary> Описание </summary>
        [CreatioProp("Описание")]
        public String Description { get; set; }

        /// <summary> Роль (Id) </summary>
        [CreatioProp("Роль (Id)")]
        public Guid? DecisionRoleId { get; set; }

        /// <summary> Роль </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Роль")]
        public ContactDecisionRole DecisionRole { get; set; }
    }
}
