using System;
using System.Collections.Generic;
using System.Text;
using DatEx.OneC.DataModel.Auxilary;
using Newtonsoft.Json;

namespace DatEx.OneC.DataModel
{
    
    [JsonObject("InformationRegister_КонтактнаяИнформация")]
    public class IRContactInfo : OneCObject
    {
        
        [JsonIgnore]
        public ContactInfoType _TypeOfContactInfo { get; set; }

        
        [JsonProperty("Объект")]
        public String KeyObject { get; set; }

        
        [JsonProperty("Объект_Type")]
        public String KeyObjectType { get; set; }

        
        [JsonProperty("Тип")]
        public String KeyType { get; set; }

        
        [JsonProperty("Вид")]
        public String KeyKind { get; set; }

        
        [JsonProperty("Вид_Type")]
        public String KeyKindType { get; set; }

        
        [JsonProperty("Представление")]
        public String View { get; set; }

        
        [JsonProperty("")]
        public String PropName { get; set; }

        
        [JsonProperty("Поле1")]
        public String Field01 { get; set; }

        
        [JsonProperty("Поле2")]
        public String Field02 { get; set; }

        
        [JsonProperty("Поле3")]
        public String Field03 { get; set; }

        
        [JsonProperty("Поле4")]
        public String Field04 { get; set; }

        
        [JsonProperty("Поле5")]
        public String Field05 { get; set; }

        
        [JsonProperty("Поле6")]
        public String Field06 { get; set; }

        
        [JsonProperty("Поле7")]
        public String Field07 { get; set; }

        
        [JsonProperty("Поле8")]
        public String Field08 { get; set; }

        
        [JsonProperty("Поле9")]
        public String Field09 { get; set; }

        
        [JsonProperty("Поле10")]
        public String Field10 { get; set; }

        
        [JsonProperty("Комментарий")]
        public String Remarks { get; set; }

        
        [JsonProperty("ЗначениеПоУмолчанию")]
        public Boolean? IsDefaultValue { get; set; }

        
        [JsonProperty("ТипДома")]
        public String TypeOfHouse { get; set; }

        
        [JsonProperty("ТипКорпуса")]
        public String TypeOfHousing { get; set; }

        
        [JsonProperty("ТипКвартиры")]
        public String TypeOfFlat { get; set; }


        public override String ToString()
        {

            return $"{(_TypeOfContactInfo is null ? KeyType : $"{_TypeOfContactInfo.Type} ({_TypeOfContactInfo.Description})")} = {View}";
        }
    }
}
