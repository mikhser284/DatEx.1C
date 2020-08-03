namespace DatEx.Creatio.DataModel.ITIS
{
    using System;
    using DatEx.Creatio.DataModel.Terrasoft.Base;

    /// <summary> Контакт ITIS </summary>
    public class Contact : DatEx.Creatio.DataModel.Terrasoft.Base.Contact
    {
        /// <summary> Деактивирована </summary>
        public Boolean RecordInactive { get; set; }

        /// <summary> ИНН (ДРФО) </summary>
        public String ITISIndividualTaxCode { get; set; }

        /// <summary> Должность сотрудника (Id) </summary>
        public Guid ITISEmployeePositionId { get; set; }

        /// <summary> Должность сотрудника </summary>
        public DatEx.Creatio.DataModel.ITIS.EmployeeJob ITISEmployeePosition { get; set; }

        /// <summary> Подразделение (Id) </summary>
        public Guid ITISSubdivisionId { get; set; }

        /// <summary> Подразделение </summary>
        public AccountOrganizationChart ITISSubdivision { get; set; }

        /// <summary> Дата начала карьеры </summary>
        public DateTime ITISCareeStartDate { get; set; }

        /// <summary> Вид занятости (Id) </summary>
        public Guid ITISEmploymentTypeId { get; set; }

        /// <summary> Вид занятости </summary>
        public ITISEmploymentType ITISEmploymentType { get; set; }

        /// <summary> Подразделение организации (Id) </summary>
        public Guid ITISOrganizationSubdivisionId { get; set; }

        /// <summary> Подразделение организации </summary>
        public OrgStructureUnit ITISOrganizationSubdivision { get; set; }
    }
}
