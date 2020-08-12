namespace DatEx.OneC.DataModel
{
    using System;
    using DatEx.OneC.DataModel.Auxilary;
    using Newtonsoft.Json;

    [OneC("InformationRegister_ФИОФизЛиц", "InformationRegister_ФИОФизЛиц", "ФИОФизЛиц", "ФИОФизЛиц")]
    [JsonObject("InformationRegister_ФИОФизЛиц")]
    public class IRNamesOfPersons : OneCObject
    {
        [OneC("DateTime", "Period", "Дата", "?", Color = ConsoleColor.DarkBlue)]
        [JsonProperty("Period")]
        public DateTime KeyPeriod { get; set; }

        [OneC("Guid", "ФизЛицо", "Справочник.ФизическиеЛица, Справочник.Организации", "ФизЛицо", Color = ConsoleColor.DarkBlue)]
        [JsonProperty("ФизЛицо")]
        public Guid KeyPerson { get; set; }

        [OneC("String", "ФизЛицо_Type", "-", "-", Color = ConsoleColor.DarkBlue)]
        [JsonProperty("ФизЛицо_Type")]
        public String KeyPersonType { get; set; }

        [OneC("String", "Фамилия", "Строка", "Фамилия", Color = ConsoleColor.Green)]
        [JsonProperty("Фамилия")]
        public String Surname { get; set; }

        [OneC("String", "Имя", "Строка", "Имя", Color = ConsoleColor.Green)]
        [JsonProperty("Имя")]
        public String GivenName { get; set; }

        [OneC("String", "Имя", "Строка", "Имя", Color = ConsoleColor.Green)]
        [JsonProperty("Отчество")]
        public String MiddleName { get; set; }

        public override String ToString() => $"{Surname} {GivenName} {MiddleName}";
    }
}
