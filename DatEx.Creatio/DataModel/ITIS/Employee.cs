namespace DatEx.Creatio.DataModel.ITIS
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;

    /// <summary> Сотрудник </summary>
    [CreatioType("Сотрудник")]
    public class Employee : DatEx.Creatio.DataModel.Terrasoft.Base.Employee
    {
        /// <summary> Фамилия </summary>
        [CreatioProp("Фамилия")]
        public String ITISSurName { get; set; }

        /// <summary> Имя </summary>
        [CreatioProp("Имя")]
        public String ITISGivenName { get; set; }

        /// <summary> Отчество </summary>
        [CreatioProp("Отчество")]
        public String ITISMiddleName { get; set; }

        /// <summary> Деактивирована </summary>
        [CreatioProp("Деактивирована")]
        public Boolean RecordInactive { get; set; }

        /// <summary> ИНН (ДРФО) </summary>
        [CreatioProp("ИНН (ДРФО)")]
        public String ITISIndividualTaxCode { get; set; }

        /// <summary> Вид занятости (Id) </summary>
        [CreatioProp("Вид занятости (Id)")]
        public Guid ITISEmploymentsTypeId { get; set; }

        /// <summary> Вид занятости </summary>
        [CreatioProp("Вид занятости")]
        public ITISEmploymentType ITISEmploymentsType { get; set; }
    }
}
