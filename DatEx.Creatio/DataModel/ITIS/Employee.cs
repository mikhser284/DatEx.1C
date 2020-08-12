namespace DatEx.Creatio.DataModel.ITIS
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;

    /// <summary> Сотрудник </summary>
    [CreatioType("Сотрудник")]
    public class Employee : DatEx.Creatio.DataModel.Terrasoft.Base.Employee
    {
        /// <summary> Фамилия </summary>
        public String ITISSurName { get; set; }

        /// <summary> Имя </summary>
        public String ITISGivenName { get; set; }

        /// <summary> Отчество </summary>
        public String ITISMiddleName { get; set; }

        /// <summary> Деактивирована </summary>
        public Boolean RecordInactive { get; set; }

        /// <summary> ИНН (ДРФО) </summary>
        public String ITISIndividualTaxCode { get; set; }

        /// <summary> Вид занятости (Id) </summary>
        public Guid ITISEmploymentsTypeId { get; set; }

        /// <summary> Вид занятости </summary>
        public ITISEmploymentType ITISEmploymentsType { get; set; }
    }
}
