
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
        [CreatioProp("Контакт (Id)", Color = ConsoleColor.Blue)]
        public Guid? ContactId { get; set; }

        /// <summary> Сотрудник </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Контакт", Color = ConsoleColor.Yellow)]
        public Contact Contact { get; set; }

        /// <summary> Контрагент (Id) </summary>
        [CreatioProp("Контрагент (Id)", Color = ConsoleColor.Blue)]
        public Guid? AccountId { get; set; }

        /// <summary> Контрагент </summary>
        [JsonIgnoreSerialization]
        [Map]
        [CreatioProp("Контрагент", Color = ConsoleColor.Yellow)]
        public Account Account { get; set; }

        /// <summary> Подразделение (Id) </summary>
        [CreatioProp("Департамент (Id)", Color = ConsoleColor.Blue)]
        public Guid OrgStructureUnitId { get; set; }

        /// <summary> Подразделение </summary>
        [JsonIgnoreSerialization]
        [Map]
        [CreatioProp("Департамент", Color = ConsoleColor.Yellow)]
        public OrgStructureUnit OrgStructureUnit { get; set; }

        /// <summary> Должность (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Должность (Id)", Color = ConsoleColor.Blue)]
        public Guid? JobId { get; set; }

        /// <summary> Должность </summary>
        [JsonIgnoreSerialization]
        [Map]
        [CreatioProp("Должность", Color = ConsoleColor.Yellow)]
        public ITIS.Job Job { get; set; }

        /// <summary> Полное название должности </summary>
        [Map]
        [CreatioProp("Полное название должности", Color = ConsoleColor.Yellow)]
        public String JobTitle { get; set; }

        /// <summary> Основное </summary>
        [Map]
        [CreatioProp("Основное", Color = ConsoleColor.Yellow)]
        public Boolean Primary { get; set; }

        /// <summary> Текущее </summary>
        [Map]
        [CreatioProp("Текущее", Color = ConsoleColor.Yellow)]
        public Boolean Current { get; set; }

        /// <summary> Начало </summary>
        [Map]
        [CreatioProp("Начало", Color = ConsoleColor.Yellow)]
        public DateTime StartDate { get; set; }

        /// <summary> Завершение </summary>
        [Map]
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
