namespace DatEx.Creatio.DataModel.ITIS
{
    using System;
    using System.Collections.Generic;
    using DatEx.Creatio.DataModel.Auxilary;

    /// <summary> Сотрудник </summary>
    [CreatioType("Сотрудник")]
    public class Employee : DatEx.Creatio.DataModel.Terrasoft.Base.Employee
    {
        public List<ITIS.EmployeeCareer> Career { get; set; } = new List<EmployeeCareer>();

        /// <summary> Фамилия </summary>
        [CreatioProp("Фамилия", Color = ConsoleColor.Magenta)]
        public String ITISSurName { get; set; }

        /// <summary> Имя </summary>
        [CreatioProp("Имя", Color = ConsoleColor.Magenta)]
        public String ITISGivenName { get; set; }

        /// <summary> Отчество </summary>
        [CreatioProp("Отчество", Color = ConsoleColor.Magenta)]
        public String ITISMiddleName { get; set; }

        /// <summary> Деактивирована </summary>
        [CreatioProp("Деактивирована")]
        public Boolean RecordInactive { get; set; }

        /// <summary> ИНН (ДРФО) </summary>
        [CreatioProp("ИНН (ДРФО)")]
        public String ITISIndividualTaxCode { get; set; }

        /// <summary> Вид занятости (Id) </summary>
        [CreatioProp("Вид занятости (Id)", Color = ConsoleColor.Blue)]
        public Guid ITISEmploymentsTypeId { get; set; }

        /// <summary> Вид занятости </summary>
        [CreatioProp("Вид занятости", Color = ConsoleColor.Magenta)]
        public ITISEmploymentType ITISEmploymentsType { get; set; }
    }
}
