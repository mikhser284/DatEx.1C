namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using System;

    /// <summary> Сотрудник </summary>
    public class Employee : BaseEntity
    {
        /// <summary> Контакт </summary>
        public Contact Contact { get; set; }
        
        /// <summary> ФИО </summary>
        public String Name { get; set; }

        /// <summary> Элемент организационной структуры </summary>
        public OrgStructureUnit OrgStructureUnit { get; set; }

        /// <summary> Заметки </summary>
        public String Notes { get; set; }

        /// <summary> Должность сотрудника </summary>
        public Job Job { get; set; }

        /// <summary> Полное название должности </summary>
        public String FullJobTitle { get; set; }

        /// <summary> Ответственный </summary>
        public Contact Owner { get; set; }

        /// <summary> Начало карьеры </summary>
        public DateTime CareerStartDate { get; set; }

        /// <summary> Завершение карьеры </summary>
        public DateTime CareerDueDate { get; set; }

        /// <summary> Испытательный срок до </summary>
        public DateTime ProbatiaonDueDate { get; set; }

        /// <summary> Причина увольнения </summary>
        public ReasonForLeaving ReasonForDismissal { get; set; }

        /// <summary> Контрагент </summary>
        public Account Account { get; set; }

        /// <summary> Руководитель </summary>
        public Employee Manager { get; set; }
    }
}
