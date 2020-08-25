using System;
using System.Collections.Generic;
using System.Text;
using DatEx.OneC.DataModel.Auxilary;
using Newtonsoft.Json;

namespace DatEx.OneC.DataModel
{
    [OneC("InformationRegister_КонтактнаяИнформация", "InformationRegister_КонтактнаяИнформация", "КонтактнаяИнформация", "КонтактнаяИнформация")]
    [JsonObject("InformationRegister_КонтактнаяИнформация")]
    public class IRContactInfo : OneCObject
    {
        [OneC("Catalog_ВидыКонтактнойИнформации", "-", "Виды контактной информации", "-", Remarks = "Прямая связь между объектами отсутствует")]
        [JsonIgnore]
        public ContactInfoType RelatedObj_TypeOfContactInfo { get; set; }

        [OneC("String", "Объект", "Справочники: ЛичныеКонтакты, КонтактныеЛица, ФизическиеЛица, Пользователи, ОбособленныеПодразделенияОрганизаций, Контрагенты, Организации, КонтактныеЛицаКонтрагентов", "Объект", Color=ConsoleColor.Blue)]
        [JsonProperty("Объект")]
        public String KeyObject { get; set; }

        [OneC("String", "Тип", "ПеречислениеСсылка.ТипыКонтактнойИнформации", "Тип", Color = ConsoleColor.Blue)]
        [JsonProperty("Тип")]
        public String KeyType { get; set; }

        [OneC("String", "Вид", "Справочник.ВидыКонтактнойИнформации, Строка", "Вид", Color = ConsoleColor.Blue)]
        [JsonProperty("Вид")]
        public String KeyKind { get; set; }

        [OneC("String", "Объект_Type", "-", "-", Color = ConsoleColor.Blue)]
        [JsonProperty("Объект_Type")]
        public String KeyObjectType { get; set; }

        [OneC("String", "Вид_Type", "-", "-", Color = ConsoleColor.Blue)]
        [JsonProperty("Вид_Type")]
        public String KeyKindType { get; set; }

        [OneC("String", "Представление", "Строка", "Представление", Color = ConsoleColor.Yellow)]
        [JsonProperty("Представление")]
        public String View { get; set; }

        [OneC("String", "Поле1", "Строка", "Поле1")]
        [JsonProperty("Поле1")]
        public String Field01 { get; set; }

        [OneC("String", "Поле2", "Строка", "Поле2")]
        [JsonProperty("Поле2")]
        public String Field02 { get; set; }

        [OneC("String", "Поле3", "Строка", "Поле3")]
        [JsonProperty("Поле3")]
        public String Field03 { get; set; }

        [OneC("String", "Поле4", "Строка", "Поле4")]
        [JsonProperty("Поле4")]
        public String Field04 { get; set; }

        [OneC("String", "Поле5", "Строка", "Поле5")]
        [JsonProperty("Поле5")]
        public String Field05 { get; set; }

        [OneC("String", "Поле6", "Строка", "Поле6")]
        [JsonProperty("Поле6")]
        public String Field06 { get; set; }

        [OneC("String", "Поле7", "Строка", "Поле7")]
        [JsonProperty("Поле7")]
        public String Field07 { get; set; }

        [OneC("String", "Поле8", "Строка", "Поле8")]
        [JsonProperty("Поле8")]
        public String Field08 { get; set; }

        [OneC("String", "Поле9", "Строка", "Поле9")]
        [JsonProperty("Поле9")]
        public String Field09 { get; set; }

        [OneC("String", "Поле10", "Строка", "Поле10")]
        [JsonProperty("Поле10")]
        public String Field10 { get; set; }

        [OneC("String", "Комментарий", "Строка", "Комментарий")]
        [JsonProperty("Комментарий")]
        public String Remarks { get; set; }

        [OneC("Boolean?", "ЗначениеПоУмолчанию", "Булево", "ЗначениеПоУмолчанию")]
        [JsonProperty("ЗначениеПоУмолчанию")]
        public Boolean? IsDefaultValue { get; set; }

        [OneC("String", "ТипДома", "ПеречислениеСсылка.ТипыДомов", "ТипДома")]
        [JsonProperty("ТипДома")]
        public String TypeOfHouse { get; set; }

        [OneC("String", "ТипКорпуса", "ПеречислениеСсылка.ТипыКорпусов", "ТипКорпуса")]
        [JsonProperty("ТипКорпуса")]
        public String TypeOfHousing { get; set; }

        [OneC("String", "ТипКвартиры", "ПеречислениеСсылка.ТипыКвартир", "ТипКвартиры")]
        [JsonProperty("ТипКвартиры")]
        public String TypeOfFlat { get; set; }


        public override String ToString() => $"{(RelatedObj_TypeOfContactInfo is null ? KeyType : $"{RelatedObj_TypeOfContactInfo.Type} ({RelatedObj_TypeOfContactInfo.Description})")} = {View}";
    }
}
