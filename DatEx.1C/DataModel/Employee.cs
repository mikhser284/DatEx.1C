using System;
using System.Collections.Generic;
using System.Text;
using DatEx._1C.DataModel.Auxilary;
using Newtonsoft.Json;

namespace DatEx._1C.DataModel
{
    [CreatioTypeMap("Employee", "Catalog_СотрудникиОрганизаций")]
    [JsonObject("Catalog_СотрудникиОрганизаций")]
    public class Employee : OneCBase
    {
        [CreatioPropertyMap("Уникальный идентификатор (guid)", "Id", "Ref_Key")]
        [JsonProperty("Ref_Key")]
        public Guid Ref_Key { get; set; }

        [CreatioIgnore]
        [JsonProperty("Predefined")]
        public Boolean? Predefined { get; set; }

        [CreatioIgnore]
        [JsonProperty("PredefinedDataName")]
        public String PredefinedDataName { get; set; }

        [CreatioIgnore]
        [JsonProperty("DataVersion")]
        public string DataVersion { get; set; }

        [CreatioIgnore]
        [JsonProperty("Description")]
        public String Description { get; set; }

        [CreatioIgnore]
        [JsonProperty("Code")]
        public string Code { get; set; }

        [CreatioIgnore]
        [JsonProperty("Parent_Key")]
        public Guid? Parent_Key { get; set; }

        [CreatioIgnore]
        [JsonProperty("IsFolder")]
        public Boolean? IsFolder { get; set; }

        [CreatioIgnore]
        [JsonProperty("DeletionMark")]
        public Boolean? DeletionMark { get; set; }

        [CreatioIgnore]
        [JsonProperty("Физлицо_Key")]
        public Guid? Физлицо_Key { get; set; }

        [CreatioIgnore]
        [JsonProperty("Актуальность")]
        public Boolean? Актуальность { get; set; }

        [CreatioIgnore]
        [JsonProperty("Организация_Key")]
        public Guid? Организация_Key { get; set; }

        [CreatioIgnore]
        [JsonProperty("ОбособленноеПодразделение_Key")]
        public Guid? ОбособленноеПодразделение_Key { get; set; }

        [CreatioIgnore]
        [JsonProperty("ВидДоговора")]
        public String ВидДоговора { get; set; }

        [CreatioIgnore]
        [JsonProperty("ВидЗанятости")]
        public String ВидЗанятости { get; set; }

        [CreatioIgnore]
        [JsonProperty("НомерДоговора")]
        public String НомерДоговора { get; set; }

        [CreatioIgnore]
        [JsonProperty("ДатаДоговора")]
        public DateTime? ДатаДоговора { get; set; }

        [CreatioIgnore]
        [JsonProperty("ГрафикРаботы_Key")]
        public Guid? ГрафикРаботы_Key { get; set; }

        [CreatioIgnore]
        [JsonProperty("ПодразделениеОрганизации_Key")]
        public Guid? ПодразделениеОрганизации_Key { get; set; }

        [CreatioIgnore]
        [JsonProperty("Должность_Key")]
        public Guid? Должность_Key { get; set; }

        [CreatioIgnore]
        [JsonProperty("ЗанимаемыхСтавок")]
        public Double? ЗанимаемыхСтавок { get; set; }

        [CreatioIgnore]
        [JsonProperty("ДатаНачала")]
        public DateTime? ДатаНачала { get; set; }

        [CreatioIgnore]
        [JsonProperty("ДатаОкончания")]
        public DateTime? ДатаОкончания { get; set; }

        [CreatioIgnore]
        [JsonProperty("ИспытательныйСрок")]
        public Double? ИспытательныйСрок { get; set; }

        [CreatioIgnore]
        [JsonProperty("ВидРасчета_Key")]
        public Guid? ВидРасчета_Key { get; set; }

        [CreatioIgnore]
        [JsonProperty("ТарифнаяСтавка")]
        public Double? ТарифнаяСтавка { get; set; }

        [CreatioIgnore]
        [JsonProperty("ВалютаТарифнойСтавки_Key")]
        public Guid? ВалютаТарифнойСтавки_Key { get; set; }

        [CreatioIgnore]
        [JsonProperty("ПостфиксНаименования")]
        public String ПостфиксНаименования { get; set; }

        [CreatioIgnore]
        [JsonProperty("ТарифныйРазряд_Key")]
        public Guid? ТарифныйРазряд_Key { get; set; }

        [CreatioIgnore]
        [JsonProperty("ОсновноеНазначение_Key")]
        public Guid? ОсновноеНазначение_Key { get; set; }







        [CreatioIgnore]
        [JsonProperty("ГруппаВзносов_Key")]
        public object ГруппаВзносов_Key { get; set; }

        [CreatioIgnore]
        [JsonProperty("ТекущееПодразделениеОрганизации_Key")]
        public object ТекущееПодразделениеОрганизации_Key { get; set; }

        [CreatioIgnore]
        [JsonProperty("ТекущаяДолжностьОрганизации_Key")]
        public object ТекущаяДолжностьОрганизации_Key { get; set; }

        [CreatioIgnore]
        [JsonProperty("ДатаПриемаНаРаботу")]
        public object ДатаПриемаНаРаботу { get; set; }

        [CreatioIgnore]
        [JsonProperty("ДатаУвольнения")]
        public object ДатаУвольнения { get; set; }

        [CreatioIgnore]
        [JsonProperty("ТекущееПодразделениеКомпании_Key")]
        public object ТекущееПодразделениеКомпании_Key { get; set; }

        [CreatioIgnore]
        [JsonProperty("ТекущаяДолжностьКомпании_Key")]
        public object ТекущаяДолжностьКомпании_Key { get; set; }

        [CreatioIgnore]
        [JsonProperty("ДатаПриемаНаРаботуВКомпанию")]
        public object ДатаПриемаНаРаботуВКомпанию { get; set; }

        [CreatioIgnore]
        [JsonProperty("ДатаУвольненияИзКомпании")]
        public object ДатаУвольненияИзКомпании { get; set; }

        [CreatioIgnore]
        [JsonProperty("Контрагент_Key")]
        public object Контрагент_Key { get; set; }

        [CreatioIgnore]
        [JsonProperty("ПоНаправлениюОргановЗанятости")]
        public object ПоНаправлениюОргановЗанятости { get; set; }

        [CreatioIgnore]
        [JsonProperty("IdCreatio")]
        public string IdCreatio { get; set; }

        [CreatioIgnore]
        [JsonProperty("ПерсональныеНадбавки")]
        public object[] ПерсональныеНадбавки { get; set; }
    }
}
