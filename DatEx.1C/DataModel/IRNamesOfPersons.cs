namespace DatEx.OneC.DataModel
{
    using System;
    using DatEx.OneC.DataModel.Auxilary;
    using Newtonsoft.Json;

    [OneS("InformationRegister_ФИОФизЛиц", "InformationRegister_ФИОФизЛиц", "ФИОФизЛиц", "ФИОФизЛиц")]
    [JsonObject("InformationRegister_ФИОФизЛиц")]
    public class IRNamesOfPersons : OneCObject
    {
        [OneS("DateTime", "Period", "Дата", "?", Color = ConsoleColor.Blue)]
        [JsonProperty("Period")]
        public DateTime KeyPeriod { get; set; }

        [OneS("Guid", "ФизЛицо", "Справочник.ФизическиеЛица, Справочник.Организации", "ФизЛицо", Color = ConsoleColor.Blue)]
        [JsonProperty("ФизЛицо")]
        public Guid KeyPerson { get; set; }

        [OneS("String", "ФизЛицо_Type", "-", "-", Color = ConsoleColor.Blue)]
        [JsonProperty("ФизЛицо_Type")]
        public String KeyPersonType { get; set; }

        [OneS("String", "Фамилия", "Строка", "Фамилия", Color = ConsoleColor.Yellow)]
        [JsonProperty("Фамилия")]
        public String Surname { get; set; }

        [OneS("String", "Имя", "Строка", "Имя", Color = ConsoleColor.Yellow)]
        [JsonProperty("Имя")]
        public String GivenName { get; set; }

        [OneS("String", "Имя", "Строка", "Имя", Color = ConsoleColor.Yellow)]
        [JsonProperty("Отчество")]
        public String MiddleName { get; set; }

        public override String ToString() => $"{Surname} {GivenName} {MiddleName}";
    }
}
