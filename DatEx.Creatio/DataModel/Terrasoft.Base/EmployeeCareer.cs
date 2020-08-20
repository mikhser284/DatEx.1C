
namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;

    /// <summary> Карьера сотрудника в нашей компании </summary>
    [CreatioType("Карьера сотрудника в нашей компании")]
    public class EmployeeCareer : BaseEntity
    {
        /// <summary> Сотрудник (Id) </summary>
        [CreatioProp("Сотрудник (Id)")]
        public Guid EmployeeId { get; set; }

        /// <summary> Сотрудник </summary>
        [CreatioProp("Сотрудник")]
        public Employee Employee { get; set; }

        /// <summary> Контрагент (Id) </summary>
        [CreatioProp("Контрагент (Id)")]
        public Guid AccountId { get; set; }

        /// <summary> Контрагент </summary>
        [CreatioProp("Контрагент")]
        public Account Account { get; set; }

        /// <summary> Подразделение (Id) </summary>
        [CreatioProp("Подразделение (Id)")]
        public Guid OrgStructureUnitId { get; set; }

        /// <summary> Подразделение </summary>
        [CreatioProp("Подразделение")]
        public OrgStructureUnit OrgStructureUnit { get; set; }

        /// <summary> Должность (Id) </summary>
        [CreatioProp("Должность (Id)")]
        public Guid EmploeeJob { get; set; }

        /// <summary> Должность </summary>
        [CreatioProp("Должность")]
        public ITIS.EmployeeJob EmployeeJob { get; set; }

        /// <summary> Полное название должности </summary>
        [CreatioProp("Полное название должности")]
        public String FullJobTitle { get; set; }

        /// <summary> Начало </summary>
        [CreatioProp("Начало")]
        public DateTime StartDate { get; set; }

        /// <summary> Завершение </summary>
        [CreatioProp("Завершение")]
        public DateTime DueDate { get; set; }

        /// <summary> Текущее </summary>
        [CreatioProp("Текущее")]
        public Boolean IsCurrent { get; set; }

        /// <summary> Испытательный срок до </summary>
        [CreatioProp("Испытательный срок до")]
        public DateTime ProbationDueDate { get; set; }

        /// <summary> Причина увольнения (Id) </summary>
        [CreatioProp("Причина увольнения (Id)")]
        public Guid ReasonForDismissalId { get; set; }

        /// <summary> Причина увольнения </summary>
        [CreatioProp("Причина увольнения")]
        public ReasonForLeaving ReasonForDismissal { get; set; }
    }
}
