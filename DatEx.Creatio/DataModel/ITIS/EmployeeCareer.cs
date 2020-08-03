
namespace DatEx.Creatio.DataModel.ITIS
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary> Карьера сотрудника в нашей компании </summary>
    public class EmployeeCareer : DatEx.Creatio.DataModel.Terrasoft.Base.EmployeeCareer
    {
        /// <summary> Основное </summary>
        public Boolean ITISPrimary { get; set; }

        /// <summary> Вид занятости (Id) </summary>
        public Guid ITISTypeOfEmploymentId { get; set; }

        /// <summary> Вид занятости </summary>
        public ITISEmploymentType ITISTypeOfEmployment { get; set; }

        /// <summary> Деактивирована </summary>
        public Boolean RecordInactive { get; set; }
    }
}
