
namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;

    /// <summary> Карьера сотрудника в нашей компании </summary>
    [CreatioType("Карьера сотрудника в нашей компании")]
    public class EmployeeCareer : BaseEntity
    {
        /// <summary> Сотрудник (Id) </summary>
        [CreatioProp("Сотрудник (Id)", Color = ConsoleColor.Blue)]
        public Guid EmployeeId { get; set; }

        /// <summary> Сотрудник </summary>
        [CreatioProp("Сотрудник", Color = ConsoleColor.Yellow)]
        public Employee Employee { get; set; }

        /// <summary> Контрагент (Id) </summary>
        [CreatioProp("Контрагент (Id)", Color = ConsoleColor.Blue)]
        public Guid AccountId { get; set; }

        /// <summary> Контрагент </summary>
        [CreatioProp("Контрагент", Color = ConsoleColor.Yellow)]
        public Account Account { get; set; }

        /// <summary> Подразделение (Id) </summary>
        [CreatioProp("Подразделение (Id)", Color = ConsoleColor.Blue)]
        public Guid OrgStructureUnitId { get; set; }

        /// <summary> Подразделение </summary>
        [CreatioProp("Подразделение", Color = ConsoleColor.Yellow)]
        public OrgStructureUnit OrgStructureUnit { get; set; }

        /// <summary> Должность (Id) </summary>
        [CreatioProp("Должность (Id)", Color = ConsoleColor.Blue)]
        public Guid EmployeeJobId { get; set; }

        /// <summary> Должность </summary>
        [CreatioProp("Должность", Color = ConsoleColor.Yellow)]
        public ITIS.EmployeeJob EmployeeJob { get; set; }

        /// <summary> Полное название должности </summary>
        [CreatioProp("Полное название должности", Color = ConsoleColor.Yellow)]
        public String FullJobTitle { get; set; }

        /// <summary> Начало </summary>
        [CreatioProp("Начало", Color = ConsoleColor.Yellow)]
        public DateTime StartDate { get; set; }

        /// <summary> Завершение </summary>
        [CreatioProp("Завершение", Color = ConsoleColor.Yellow)]
        public DateTime DueDate { get; set; }

        /// <summary> Текущее </summary>
        [CreatioProp("Текущее", Color = ConsoleColor.Yellow)]
        public Boolean IsCurrent { get; set; }

        /// <summary> Испытательный срок до </summary>
        [CreatioProp("Испытательный срок до", Color = ConsoleColor.Yellow)]
        public DateTime ProbationDueDate { get; set; }

        /// <summary> Причина увольнения (Id) </summary>
        [CreatioProp("Причина увольнения (Id)", Color = ConsoleColor.Blue)]
        public Guid ReasonForDismissalId { get; set; }

        /// <summary> Причина увольнения </summary>
        [CreatioProp("Причина увольнения", Color = ConsoleColor.Yellow)]
        public ReasonForLeaving ReasonForDismissal { get; set; }
    }
}
