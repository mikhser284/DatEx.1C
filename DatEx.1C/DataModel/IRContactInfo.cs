using System;
using System.Collections.Generic;
using System.Text;
using DatEx.OneS.DataModel.Auxilary;
using Newtonsoft.Json;

namespace DatEx.OneS.DataModel
{
    [OneS("InformationRegister_КонтактнаяИнформация", "InformationRegister_КонтактнаяИнформация", "КонтактнаяИнформация", "КонтактнаяИнформация")]
    [JsonObject("InformationRegister_КонтактнаяИнформация")]
    public class IRContactInfo : OneCObject
    {
        [OneS("Catalog_ВидыКонтактнойИнформации", "-", "Виды контактной информации", "-", Remarks = "Прямая связь между объектами отсутствует")]
        [JsonIgnore]
        public ContactInfoType RelatedObj_TypeOfContactInfo { get; set; }

        [OneS("String", "Объект", "Справочники: ЛичныеКонтакты, КонтактныеЛица, ФизическиеЛица, Пользователи, ОбособленныеПодразделенияОрганизаций, Контрагенты, Организации, КонтактныеЛицаКонтрагентов", "Объект", Color=ConsoleColor.Blue)]
        [JsonProperty("Объект")]
        public String KeyObject { get; set; }

        [OneS("String", "Тип", "ПеречислениеСсылка.ТипыКонтактнойИнформации", "Тип", Color = ConsoleColor.Blue)]
        [JsonProperty("Тип")]
        public String KeyType { get; set; }

        [OneS("String", "Вид", "Справочник.ВидыКонтактнойИнформации, Строка", "Вид", Color = ConsoleColor.Blue)]
        [JsonProperty("Вид")]
        public String KeyKind { get; set; }

        [OneS("String", "Объект_Type", "-", "-", Color = ConsoleColor.Blue)]
        [JsonProperty("Объект_Type")]
        public String KeyObjectType { get; set; }

        [OneS("String", "Вид_Type", "-", "-", Color = ConsoleColor.Blue)]
        [JsonProperty("Вид_Type")]
        public String KeyKindType { get; set; }

        [OneS("String", "Представление", "Строка", "Представление", Color = ConsoleColor.Yellow)]
        [JsonProperty("Представление")]
        public String View { get; set; }

        [OneS("String", "Поле1", "Строка", "Поле1")]
        [JsonProperty("Поле1")]
        public String Field01 { get; set; }

        [OneS("String", "Поле2", "Строка", "Поле2")]
        [JsonProperty("Поле2")]
        public String Field02 { get; set; }

        [OneS("String", "Поле3", "Строка", "Поле3")]
        [JsonProperty("Поле3")]
        public String Field03 { get; set; }

        [OneS("String", "Поле4", "Строка", "Поле4")]
        [JsonProperty("Поле4")]
        public String Field04 { get; set; }

        [OneS("String", "Поле5", "Строка", "Поле5")]
        [JsonProperty("Поле5")]
        public String Field05 { get; set; }

        [OneS("String", "Поле6", "Строка", "Поле6")]
        [JsonProperty("Поле6")]
        public String Field06 { get; set; }

        [OneS("String", "Поле7", "Строка", "Поле7")]
        [JsonProperty("Поле7")]
        public String Field07 { get; set; }

        [OneS("String", "Поле8", "Строка", "Поле8")]
        [JsonProperty("Поле8")]
        public String Field08 { get; set; }

        [OneS("String", "Поле9", "Строка", "Поле9")]
        [JsonProperty("Поле9")]
        public String Field09 { get; set; }

        [OneS("String", "Поле10", "Строка", "Поле10")]
        [JsonProperty("Поле10")]
        public String Field10 { get; set; }

        [OneS("String", "Комментарий", "Строка", "Комментарий")]
        [JsonProperty("Комментарий")]
        public String Remarks { get; set; }

        [OneS("Boolean?", "ЗначениеПоУмолчанию", "Булево", "ЗначениеПоУмолчанию")]
        [JsonProperty("ЗначениеПоУмолчанию")]
        public Boolean? IsDefaultValue { get; set; }

        [OneS("String", "ТипДома", "ПеречислениеСсылка.ТипыДомов", "ТипДома")]
        [JsonProperty("ТипДома")]
        public String TypeOfHouse { get; set; }

        [OneS("String", "ТипКорпуса", "ПеречислениеСсылка.ТипыКорпусов", "ТипКорпуса")]
        [JsonProperty("ТипКорпуса")]
        public String TypeOfHousing { get; set; }

        [OneS("String", "ТипКвартиры", "ПеречислениеСсылка.ТипыКвартир", "ТипКвартиры")]
        [JsonProperty("ТипКвартиры")]
        public String TypeOfFlat { get; set; }


        public override String ToString() => $"{(RelatedObj_TypeOfContactInfo is null ? KeyType : $"{RelatedObj_TypeOfContactInfo.Type} ({RelatedObj_TypeOfContactInfo.Description})")} = {View}";
    }
}
