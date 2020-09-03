namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;
    using Newtonsoft.Json;

    /// <summary> Сотрудник </summary>
    [CreatioType("Сотрудник")]
    public class Employee : BaseEntity
    {
        /// <summary> Контакт (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Контакт (Id)", Color = ConsoleColor.Blue)]
        public Guid? ContactId { get; set; }

        /// <summary> Контакт </summary>
        [CreatioProp("Контакт", Color = ConsoleColor.Yellow)]
        [JsonIgnoreSerialization]
        public Contact Contact { get; set; }

        /// <summary> ФИО </summary>
        [CreatioProp("ФИО", Color = ConsoleColor.Yellow)]
        public String Name { get; set; }

        /// <summary> Элемент организационной структуры (Id) </summary>
        [CreatioProp("Элемент организационной структуры (Id)", Color = ConsoleColor.Blue)]
        [JsonConverter(typeof(JsonConverter_Guid))]
        public Guid? OrgStructureUnitId { get; set; }

        /// <summary> Элемент организационной структуры </summary>
        [CreatioProp("Элемент организационной структуры", Color = ConsoleColor.Yellow)]
        [JsonIgnoreSerialization]
        public OrgStructureUnit OrgStructureUnit { get; set; }

        /// <summary> Заметки </summary>
        [CreatioProp("Заметки")]
        public String Notes { get; set; }

        /// <summary> Должность сотрудника (Id) </summary>
        [CreatioProp("Должность сотрудника (Id)", Color = ConsoleColor.Blue)]
        [JsonConverter(typeof(JsonConverter_Guid))]
        public Guid? JobId { get; set; }

        /// <summary> Должность сотрудника </summary>
        [CreatioProp("Должность сотрудника", Color = ConsoleColor.Yellow)]
        [JsonIgnoreSerialization]
        public Job Job { get; set; }

        /// <summary> Полное название должности </summary>
        [CreatioProp("Полное название должности", Color = ConsoleColor.Yellow)]
        public String FullJobTitle { get; set; }

        /// <summary> Ответственный (Id) </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Ответственный (Id)")]
        [JsonConverter(typeof(JsonConverter_Guid))]
        public Guid? OwnerId { get; set; }

        /// <summary> Ответственный </summary>
        [CreatioProp("Ответственный")]
        [JsonIgnoreSerialization]
        public Contact Owner { get; set; }

        /// <summary> Начало карьеры </summary>
        [JsonConverter(typeof(JsonConverter_Date))]
        [CreatioProp("Начало карьеры", Color = ConsoleColor.Yellow)]
        public DateTime? CareerStartDate { get; set; }

        /// <summary> Завершение карьеры </summary>
        [JsonConverter(typeof(JsonConverter_Date))]
        [CreatioProp("Завершение карьеры", Color = ConsoleColor.Yellow)]
        public DateTime? CareerDueDate { get; set; }

        /// <summary> Испытательный срок до </summary>
        [JsonConverter(typeof(JsonConverter_Date))]
        [CreatioProp("Испытательный срок до")]
        public DateTime? ProbatiaonDueDate { get; set; }

        /// <summary> Причина увольнения (Id) </summary>
        [CreatioProp("Причина увольнения (Id)")]
        [JsonConverter(typeof(JsonConverter_Guid))]
        public Guid? ReasonForDismissalId { get; set; }

        /// <summary> Причина увольнения </summary>
        [CreatioProp("Причина увольнения")]
        [JsonIgnoreSerialization]
        public ReasonForLeaving ReasonForDismissal { get; set; }

        /// <summary> Контрагент (Id) </summary>
        [CreatioProp("Контрагент (Id)", Color = ConsoleColor.Yellow)]
        [JsonConverter(typeof(JsonConverter_Guid))]
        public Guid? AccountId { get; set; }

        /// <summary> Контрагент </summary>
        [CreatioProp("Контрагент", Color = ConsoleColor.Yellow)]
        [JsonIgnoreSerialization]
        public Account Account { get; set; }

        /// <summary> Руководитель (Id) </summary>
        [CreatioProp("Руководитель (Id)")]
        [JsonConverter(typeof(JsonConverter_Guid))]
        public Guid? ManagerId { get; set; }

        /// <summary> Руководитель </summary>
        [CreatioProp("Руководитель")]
        [JsonIgnoreSerialization]
        public Employee Manager { get; set; }

        
    }
}
