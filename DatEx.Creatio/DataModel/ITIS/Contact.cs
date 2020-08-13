namespace DatEx.Creatio.DataModel.ITIS
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;
    using Terrasoft = DatEx.Creatio.DataModel.Terrasoft.Base;
    using ITIS = DatEx.Creatio.DataModel.ITIS;

    [CreatioType("Контакт")]
    /// <summary> Контакт ITIS </summary>
    public class Contact : Terrasoft.Contact
    {
        /// <summary> 1C Id </summary>
        /// <remarks> ■ Поле отсутствует? </remarks>
        [CreatioProp("Guid", "1C Id", Color = ConsoleColor.Green)]
        public Guid IdOneC { get; set; }

        /// <summary> Деактивирована </summary>
        [CreatioProp("Деактивирована", Color = ConsoleColor.Blue)]
        public Boolean RecordInactive { get; set; }

        /// <summary> ИНН (ДРФО) </summary>
        [CreatioProp("ИНН (ДРФО)")]
        public String ITISIndividualTaxCode { get; set; }

        /// <summary> Должность сотрудника (Id) </summary>
        [CreatioProp("Guid", "Должность сотрудника (Id)", Color = ConsoleColor.Blue)]
        public Guid ITISEmployeePositionId { get; set; }

        /// <summary> Должность сотрудника </summary>
        [CreatioProp("Должность сотрудника", Color = ConsoleColor.Magenta)]
        public ITIS.EmployeeJob ITISEmployeePosition { get; set; }

        /// <summary> Подразделение (Id) </summary>
        [CreatioProp("Guid", "Подразделение (Id)", Color = ConsoleColor.Blue)]
        public Guid ITISSubdivisionId { get; set; }

        /// <summary> Подразделение </summary>
        [CreatioProp("Подразделение", Color = ConsoleColor.Magenta)]
        public Terrasoft.AccountOrganizationChart ITISSubdivision { get; set; }

        /// <summary> Дата начала карьеры </summary>
        [CreatioProp("Дата начала карьеры", Color = ConsoleColor.Magenta)]
        public DateTime ITISCareeStartDate { get; set; }

        /// <summary> Вид занятости (Id) </summary>
        [CreatioProp("Guid", "Вид занятости (Id)", Color = ConsoleColor.Blue)]
        public Guid ITISEmploymentTypeId { get; set; }

        /// <summary> Вид занятости </summary>
        [CreatioProp("Вид занятости", Color = ConsoleColor.Magenta)]
        public ITISEmploymentType ITISEmploymentType { get; set; }

        /// <summary> Подразделение организации (Id) </summary>
        [CreatioProp("Guid", "Подразделение организации (Id)", Color = ConsoleColor.Blue)]
        public Guid ITISOrganizationSubdivisionId { get; set; }

        /// <summary> Подразделение организации </summary>
        [CreatioProp("Подразделение организации", Color = ConsoleColor.Magenta)]
        public Terrasoft.OrgStructureUnit ITISOrganizationSubdivision { get; set; }
    }
}
