namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;

    /// <summary> Сотрудник </summary>
    [CreatioType("Сотрудник")]
    public class Employee : BaseEntity
    {
        /// <summary> Контакт (Id) </summary>
        [CreatioProp("Контакт (Id)")]
        public Guid ContactId { get; set; }

        /// <summary> Контакт </summary>
        [CreatioProp("Контакт")]
        public Contact Contact { get; set; }

        /// <summary> ФИО </summary>
        [CreatioProp("ФИО")]
        public String Name { get; set; }

        /// <summary> Элемент организационной структуры (Id) </summary>
        [CreatioProp("Элемент организационной структуры (Id)")]
        public Guid OrgStructureUnitId { get; set; }

        /// <summary> Элемент организационной структуры </summary>
        [CreatioProp("Элемент организационной структуры")]
        public OrgStructureUnit OrgStructureUnit { get; set; }

        /// <summary> Заметки </summary>
        [CreatioProp("Заметки")]
        public String Notes { get; set; }

        /// <summary> Должность сотрудника (Id) </summary>
        [CreatioProp("Должность сотрудника (Id)")]
        public Guid JobId { get; set; }

        /// <summary> Должность сотрудника </summary>
        [CreatioProp("Должность сотрудника")]
        public Job Job { get; set; }

        /// <summary> Полное название должности </summary>
        [CreatioProp("Полное название должности")]
        public String FullJobTitle { get; set; }

        /// <summary> Ответственный (Id) </summary>
        [CreatioProp("Ответственный (Id)")]
        public Guid OwnerId { get; set; }

        /// <summary> Ответственный </summary>
        [CreatioProp("Ответственный")]
        public Contact Owner { get; set; }

        /// <summary> Начало карьеры </summary>
        [CreatioProp("Начало карьеры")]
        public DateTime CareerStartDate { get; set; }

        /// <summary> Завершение карьеры </summary>
        [CreatioProp("Завершение карьеры")]
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
        [CreatioProp("Контрагент (Id)")]
        public Guid AccountId { get; set; }

        /// <summary> Контрагент </summary>
        [CreatioProp("Контрагент")]
        public Account Account { get; set; }

        /// <summary> Руководитель (Id) </summary>
        [CreatioProp("Руководитель (Id)")]
        public Guid ManagerId { get; set; }

        /// <summary> Руководитель </summary>
        [CreatioProp("Руководитель")]
        public Employee Manager { get; set; }
    }
}
