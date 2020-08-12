namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;

    /// <summary> Структура организации контрагента </summary>
    [CreatioType("Структура организации контрагента")]
    public class AccountOrganizationChart : BaseEntity
    {
        /// <summary> Контрагент (Id) </summary>
        public Guid AccountId { get; set; }

        /// <summary> Контрагент </summary>
        public Account Account { get; set; }

        /// <summary> Подразделение </summary>
        public String CustomDepartmentName { get; set; }

        /// <summary> Департамент (Id) </summary>
        public Guid DepartmentId { get; set; }

        /// <summary> Департамент </summary>
        public Department Department { get; set; }

        /// <summary> Руководитель (Id) </summary>
        public Guid ManagerId { get; set; }

        /// <summary> Руководитель </summary>
        public Contact Manager { get; set; }

        /// <summary> Описание </summary>
        public String Description { get; set; }

        /// <summary> Родительское подразделение (Id) </summary>
        public Guid ParentId { get; set; }

        /// <summary> Родительское подразделение </summary>
        public AccountOrganizationChart Parent { get; set; }

        public override string ToString()
        {
            return CustomDepartmentName;
        }
    }
}
