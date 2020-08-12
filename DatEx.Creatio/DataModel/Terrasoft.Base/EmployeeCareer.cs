
namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;

    /// <summary> Карьера сотрудника в нашей компании </summary>
    [CreatioType("Карьера сотрудника в нашей компании")]
    public class EmployeeCareer : BaseEntity
    {
        /// <summary> Сотрудник (Id) </summary>
        public Guid EmployeeId { get; set; }

        /// <summary> Сотрудник </summary>
        public Employee Employee { get; set; }

        /// <summary> Контрагент (Id) </summary>
        public Guid AccountId { get; set; }

        /// <summary> Контрагент </summary>
        public Account Account { get; set; }

        /// <summary> Подразделение (Id) </summary>
        public Guid OrgStructureUnitId { get; set; }

        /// <summary> Подразделение </summary>
        public OrgStructureUnit OrgStructureUnit { get; set; }

        /// <summary> Должность (Id) </summary>
        public Guid EmploeeJob { get; set; }

        /// <summary> Должность </summary>
        public ITIS.EmployeeJob EmployeeJob { get; set; }

        /// <summary> Полное название должности </summary>
        public String FullJobTitle { get; set; }

        /// <summary> Начало </summary>
        public DateTime StartDate { get; set; }

        /// <summary> Завершение </summary>
        public DateTime DueDate { get; set; }

        /// <summary> Текущее </summary>
        public Boolean IsCurrent { get; set; }

        /// <summary> Испытательный срок до </summary>
        public DateTime ProbationDueDate { get; set; }

        /// <summary> Причина увольнения (Id) </summary>
        public Guid ReasonForDismissalId { get; set; }

        /// <summary> Причина увольнения </summary>
        public ReasonForLeaving ReasonForDismissal { get; set; }
    }
}
