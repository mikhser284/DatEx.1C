using System;
using System.Collections.Generic;
using System.Text;

namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    /// <summary> Структура организации контрагента </summary>
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
    }
}
