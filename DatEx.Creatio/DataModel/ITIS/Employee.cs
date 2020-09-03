namespace DatEx.Creatio.DataModel.ITIS
{
    using System;
    using System.Collections.Generic;
    using DatEx.Creatio.DataModel.Auxilary;
    using Newtonsoft.Json;

    /// <summary> Сотрудник </summary>
    [CreatioType("Сотрудник")]
    public class Employee : DatEx.Creatio.DataModel.Terrasoft.Base.Employee
    {
        [JsonIgnoreSerialization]
        public List<ITIS.EmployeeCareer> Career { get; set; } = new List<EmployeeCareer>();

        /// <summary> Фамилия </summary>
        [CreatioProp("Фамилия", Color = ConsoleColor.Yellow)]
        public String ITISSurName { get; set; }

        /// <summary> Имя </summary>
        [CreatioProp("Имя", Color = ConsoleColor.Yellow)]
        public String ITISGivenName { get; set; }

        /// <summary> Отчество </summary>
        [CreatioProp("Отчество", Color = ConsoleColor.Yellow)]
        public String ITISMiddleName { get; set; }

        /// <summary> Деактивирована </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("Деактивирована")]
        public Boolean? RecordInactive { get; set; }

        /// <summary> ИНН (ДРФО) </summary>
        [JsonIgnoreSerialization]
        [CreatioProp("ИНН (ДРФО)")]
        public String ITISIndividualTaxCode { get; set; }

        /// <summary> Вид занятости (Id) </summary>
        [JsonConverter(typeof(JsonConverter_Guid))]
        [CreatioProp("Вид занятости (Id)", Color = ConsoleColor.Blue)]
        public Guid? ITISEmploymentsTypeId { get; set; }

        /// <summary> Вид занятости </summary>
        [CreatioProp("Вид занятости", Color = ConsoleColor.Yellow)]
        [JsonIgnoreSerialization]
        public ITISEmploymentType ITISEmploymentsType { get; set; }

        public override string ToString() => $"{ITISSurName} {ITISGivenName} {ITISMiddleName} ({FullJobTitle})";
    }
}
