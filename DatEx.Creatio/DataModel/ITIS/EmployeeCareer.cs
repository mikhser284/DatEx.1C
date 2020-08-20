
namespace DatEx.Creatio.DataModel.ITIS
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;

    /// <summary> Карьера сотрудника в нашей компании </summary>
    [CreatioType("Карьера сотрудника в нашей компании")]
    public class EmployeeCareer : DatEx.Creatio.DataModel.Terrasoft.Base.EmployeeCareer
    {
        /// <summary> Основное </summary>
        [CreatioProp("Основное")]
        public Boolean ITISPrimary { get; set; }

        /// <summary> Вид занятости (Id) </summary>
        [CreatioProp("Вид занятости (Id)")]
        public Guid ITISTypeOfEmploymentId { get; set; }

        /// <summary> Вид занятости </summary>
        [CreatioProp("Вид занятости")]
        public ITISEmploymentType ITISTypeOfEmployment { get; set; }

        /// <summary> Деактивирована </summary>
        [CreatioProp("Деактивирована")]
        public Boolean RecordInactive { get; set; }
    }
}
