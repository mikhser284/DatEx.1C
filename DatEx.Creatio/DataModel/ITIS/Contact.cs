namespace DatEx.Creatio.DataModel.ITIS
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;
    using DatEx.Creatio.DataModel.Terrasoft.Base;
    using ITIS = DatEx.Creatio.DataModel.ITIS;

    /// <summary> Контакт ITIS </summary>
    public class Contact : DatEx.Creatio.DataModel.Terrasoft.Base.Contact
    {
        /// <summary> 1C Id </summary>
        /// <remarks> ■ Поле отсутствует? </remarks>
        [CreatioProp("1C Id")]
        public Guid IdOneC { get; set; }

        /// <summary> Деактивирована </summary>
        [CreatioProp("Деактивирована")]
        public Boolean RecordInactive { get; set; }

        /// <summary> ИНН (ДРФО) </summary>
        [CreatioProp("ИНН (ДРФО)")]
        public String ITISIndividualTaxCode { get; set; }

        /// <summary> Должность сотрудника (Id) </summary>
        [CreatioProp("Должность сотрудника (Id)")]
        public Guid ITISEmployeePositionId { get; set; }

        /// <summary> Должность сотрудника </summary>
        [CreatioProp("Должность сотрудника")]
        public ITIS.EmployeeJob ITISEmployeePosition { get; set; }

        /// <summary> Подразделение (Id) </summary>
        [CreatioProp("Подразделение (Id)")]
        public Guid ITISSubdivisionId { get; set; }

        /// <summary> Подразделение </summary>
        [CreatioProp("Подразделение")]
        public AccountOrganizationChart ITISSubdivision { get; set; }

        /// <summary> Дата начала карьеры </summary>
        [CreatioProp("Дата начала карьеры")]
        public DateTime ITISCareeStartDate { get; set; }

        /// <summary> Вид занятости (Id) </summary>
        [CreatioProp("Вид занятости (Id)")]
        public Guid ITISEmploymentTypeId { get; set; }

        /// <summary> Вид занятости </summary>
        [CreatioProp("Вид занятости")]
        public ITISEmploymentType ITISEmploymentType { get; set; }

        /// <summary> Подразделение организации (Id) </summary>
        [CreatioProp("Подразделение организации (Id)")]
        public Guid ITISOrganizationSubdivisionId { get; set; }

        /// <summary> Подразделение организации </summary>
        [CreatioProp("Подразделение организации")]
        public OrgStructureUnit ITISOrganizationSubdivision { get; set; }
    }
}
