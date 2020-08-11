namespace DatEx.OneC.DataModel
{
    using System;
    using DatEx.OneC.DataModel.Auxilary;
    using Newtonsoft.Json;

    
    [JsonObject("InformationRegister_ФИОФизЛиц")]
    public class IRNamesOfPersons : OneCObject
    {
        
        [JsonProperty("Period")]
        public DateTime KeyPeriod { get; set; }

        
        [JsonProperty("ФизЛицо")]
        public Guid KeyPerson { get; set; }

        
        [JsonProperty("ФизЛицо_Type")]
        public String KeyPersonType { get; set; }

        
        [JsonProperty("Фамилия")]
        public String Surname { get; set; }

        
        [JsonProperty("Имя")]
        public String GivenName { get; set; }

        
        [JsonProperty("Отчество")]
        public String MiddleName { get; set; }

        public override String ToString() => $"{Surname} {GivenName} {MiddleName}";
    }
}
