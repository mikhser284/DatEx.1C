namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;

    /// <summary> Сотрудник </summary>
    [CreatioType("Сотрудник")]
    public class Employee : BaseEntity
    {
        /// <summary> Контакт (Id) </summary>
        [CreatioProp("Контакт (Id)", Color = ConsoleColor.Blue)]
        public Guid ContactId { get; set; }

        /// <summary> Контакт </summary>
        [CreatioProp("Контакт", Color = ConsoleColor.Magenta)]
        public Contact Contact { get; set; }

        /// <summary> ФИО </summary>
        [CreatioProp("ФИО", Color = ConsoleColor.Magenta)]
        public String Name { get; set; }

        /// <summary> Элемент организационной структуры (Id) </summary>
        [CreatioProp("Элемент организационной структуры (Id)", Color = ConsoleColor.Blue)]
        public Guid OrgStructureUnitId { get; set; }

        /// <summary> Элемент организационной структуры </summary>
        [CreatioProp("Элемент организационной структуры", Color = ConsoleColor.Magenta)]
        public OrgStructureUnit OrgStructureUnit { get; set; }

        /// <summary> Заметки </summary>
        [CreatioProp("Заметки")]
        public String Notes { get; set; }

        /// <summary> Должность сотрудника (Id) </summary>
        [CreatioProp("Должность сотрудника (Id)", Color = ConsoleColor.Blue)]
        public Guid JobId { get; set; }

        /// <summary> Должность сотрудника </summary>
        [CreatioProp("Должность сотрудника", Color = ConsoleColor.Magenta)]
        public Job Job { get; set; }

        /// <summary> Полное название должности </summary>
        [CreatioProp("Полное название должности", Color = ConsoleColor.Magenta)]
        public String FullJobTitle { get; set; }

        /// <summary> Ответственный (Id) </summary>
        [CreatioProp("Ответственный (Id)")]
        public Guid OwnerId { get; set; }

        /// <summary> Ответственный </summary>
        [CreatioProp("Ответственный")]
        public Contact Owner { get; set; }

        /// <summary> Начало карьеры </summary>
        [CreatioProp("Начало карьеры", Color = ConsoleColor.Magenta)]
        public DateTime CareerStartDate { get; set; }

        /// <summary> Завершение карьеры </summary>
        [CreatioProp("Завершение карьеры", Color = ConsoleColor.Magenta)]
        public DateTime CareerDueDate { get; set; }

        /// <summary> Испытательный срок до </summary>
        [CreatioProp("Испытательный срок до")]
        public DateTime ProbatiaonDueDate { get; set; }

        /// <summary> Причина увольнения (Id) </summary>
        [CreatioProp("Причина увольнения (Id)")]
        public Guid ReasonForDismissalId { get; set; }

        /// <summary> Причина увольнения </summary>
        [CreatioProp("Причина увольнения")]
        public ReasonForLeaving ReasonForDismissal { get; set; }

        /// <summary> Контрагент (Id) </summary>
        [CreatioProp("Контрагент (Id)", Color = ConsoleColor.Magenta)]
        public Guid AccountId { get; set; }

        /// <summary> Контрагент </summary>
        [CreatioProp("Контрагент", Color = ConsoleColor.Magenta)]
        public Account Account { get; set; }

        /// <summary> Руководитель (Id) </summary>
        [CreatioProp("Руководитель (Id)")]
        public Guid ManagerId { get; set; }

        /// <summary> Руководитель </summary>
        [CreatioProp("Руководитель")]
        public Employee Manager { get; set; }
    }
}
