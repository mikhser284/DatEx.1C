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
        [CreatioAux]
        [JsonProperty("Объект")]
        public String KeyObject { get; set; }

        [CreatioAux]
        [JsonProperty("Объект_Type")]
        public String KeyObjectType { get; set; }

        [CreatioAux]
        [JsonProperty("Тип")]
        public String KeyType { get; set; }

        [CreatioAux]
        [JsonProperty("Вид")]
        public String KeyKind { get; set; }

        [CreatioAux]
        [JsonProperty("Вид_Type")]
        public String KeyKindType { get; set; }

        [CreatioAux]
        [JsonProperty("Представление")]
        public String View { get; set; }

        [CreatioAux]
        [JsonProperty("")]
        public String PropName { get; set; }

        [CreatioAux]
        [JsonProperty("Поле1")]
        public String Field01 { get; set; }

        [CreatioAux]
        [JsonProperty("Поле2")]
        public String Field02 { get; set; }

        [CreatioAux]
        [JsonProperty("Поле3")]
        public String Field03 { get; set; }

        [CreatioAux]
        [JsonProperty("Поле4")]
        public String Field04 { get; set; }

        [CreatioAux]
        [JsonProperty("Поле5")]
        public String Field05 { get; set; }

        [CreatioAux]
        [JsonProperty("Поле6")]
        public String Field06 { get; set; }

        [CreatioAux]
        [JsonProperty("Поле7")]
        public String Field07 { get; set; }

        [CreatioAux]
        [JsonProperty("Поле8")]
        public String Field08 { get; set; }

        [CreatioAux]
        [JsonProperty("Поле9")]
        public String Field09 { get; set; }

        [CreatioAux]
        [JsonProperty("Поле10")]
        public String Field10 { get; set; }

        [CreatioAux]
        [JsonProperty("Комментарий")]
        public String Remarks { get; set; }

        [CreatioAux]
        [JsonProperty("ЗначениеПоУмолчанию")]
        public Boolean? IsDefaultValue { get; set; }

        [CreatioAux]
        [JsonProperty("ТипДома")]
        public String TypeOfHouse { get; set; }

        [CreatioAux]
        [JsonProperty("ТипКорпуса")]
        public String TypeOfHousing { get; set; }

        [CreatioAux]
        [JsonProperty("ТипКвартиры")]
        public String TypeOfFlat { get; set; }

        [JsonIgnore]
        public ContactInfoType TypeOfContactInfo { get; set; }
    }
}
