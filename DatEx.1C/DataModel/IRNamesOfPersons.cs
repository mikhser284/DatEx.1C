namespace DatEx.OneC.DataModel
{
    using System;
    using DatEx.OneC.DataModel.Auxilary;
    using Newtonsoft.Json;

    [JsonObject("InformationRegister_ФИОФизЛиц")]
    public class IRNamesOfPersons : OneCObject
    {
        [CreatioAux]
        [JsonProperty("Period")]
        public DateTime KeyPeriod { get; set; }

        [CreatioAux]
        [JsonProperty("ФизЛицо")]
        public Guid KeyPerson { get; set; }

        [CreatioAux]
        [JsonProperty("ФизЛицо_Type")]
        public String KeyPersonType { get; set; }

        [CreatioAux]
        [JsonProperty("Фамилия")]
        public String Surname { get; set; }

        [CreatioAux]
        [JsonProperty("Имя")]
        public String GivenName { get; set; }

        [CreatioAux]
        [JsonProperty("Отчество")]
        public String MiddleName { get; set; }


    }
}
