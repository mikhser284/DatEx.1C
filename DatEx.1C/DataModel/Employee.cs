using System;
using System.Collections.Generic;
using System.Text;
using DatEx._1C.DataModel.Auxilary;
using Newtonsoft.Json;

namespace DatEx._1C.DataModel
{
    [CreatioTypeMap("DatEx.Creatio.DataModel.ITIS.Contact", "Catalog_СотрудникиОрганизаций")]
    [JsonObject("Catalog_СотрудникиОрганизаций")]
    public class Employee : OneCBase
    {
        [CreatioPropertyMap("Guid", "Id", "IdCreatio")]
        [JsonProperty("IdCreatio")]
        public String IdCreatio { get; set; }

        [CreatioPropertyMap("Guid", "IdOneC", "Ref_Key")]
        [JsonProperty("Ref_Key")]
        public Guid Ref_Key { get; set; }

        [CreatioPropertyMap("String", "Name", "Description")]
        [JsonProperty("Description")]
        public String Description { get; set; }


        [CreatioPropertyMap("Раздел<Контакты>", "ContactId", "Физлицо_Key")]
        [JsonProperty("Физлицо_Key")]
        public Guid? ContactId { get; set; }


        [CreatioIgnore]
        [JsonProperty("ДатаНачала")]
        public DateTime? StartDate { get; set; }


        [CreatioIgnore]
        [JsonProperty("ДатаПриемаНаРаботу")]
        public DateTime? DateOfEmployment { get; set; }

        [CreatioIgnore]
        [JsonProperty("ДатаОкончания")]
        public DateTime? DueDate { get; set; }

        [CreatioIgnore]
        [JsonProperty("ДатаУвольнения")]
        public DateTime? DateOfDismisal { get; set; }

        [CreatioIgnore]
        [JsonProperty("Организация_Key")]
        public Guid? OrganizationId { get; set; }

        [CreatioIgnore]
        [JsonProperty("ПодразделениеОрганизации_Key")]
        public Guid? OrganizationSubdivisionId { get; set; }

        [CreatioIgnore]
        [JsonProperty("ТекущееПодразделениеОрганизации_Key")]
        public Guid? CurrentOrganizationSubdivisionId { get; set; }

        [CreatioIgnore]
        [JsonProperty("ТекущаяДолжностьОрганизации_Key")]
        public Guid? CurrentPositionInOrganizationId { get; set; }

        [CreatioIgnore]
        [JsonProperty("ВидДоговора")]
        public String ContractType { get; set; }

        [CreatioIgnore]
        [JsonProperty("ВидЗанятости")]
        public String TypeOfEmployment { get; set; }

        [CreatioIgnore]
        [JsonProperty("Должность_Key")]
        public Guid? PositionId { get; set; }

        [CreatioIgnore]
        [JsonProperty("Актуальность")]
        public Boolean? IsActual { get; set; }

        [CreatioIgnore]
        [JsonProperty("Контрагент_Key")]
        public Guid? ContractorId { get; set; }

        // ····· Служебные или информативные поля ···································································································

        [CreatioAux]
        [JsonProperty("IsFolder")]
        public Boolean? IsFolder { get; set; }

        [CreatioAux]
        [JsonProperty("DeletionMark")]
        public Boolean? DeletionMark { get; set; }

        [CreatioAux]
        [JsonProperty("Code")]
        public string Code { get; set; }

        [CreatioAux]
        [JsonProperty("Parent_Key")]
        public Guid? ParentId { get; set; }





        // ───── Не используемые поля ───────────────────────────────────────────────────────────────────────────────────────────────────────────────

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
        [JsonProperty("ОбособленноеПодразделение_Key")]
        public Guid? DetachedUnitId { get; set; }

        [CreatioIgnore]
        [JsonProperty("НомерДоговора")]
        public String ContractNumber { get; set; }


        [CreatioIgnore]
        [JsonProperty("ГрафикРаботы_Key")]
        public Guid? ГрафикРаботы_Key { get; set; }

        [CreatioIgnore]
        [JsonProperty("ЗанимаемыхСтавок")]
        public Double? ЗанимаемыхСтавок { get; set; }


        [CreatioIgnore]
        [JsonProperty("ДатаДоговора")]
        public DateTime? ДатаДоговора { get; set; }

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
        public Guid? ГруппаВзносов_Key { get; set; }

        [CreatioIgnore]
        [JsonProperty("ТекущееПодразделениеКомпании_Key")]
        public Guid? ТекущееПодразделениеКомпании_Key { get; set; }

        [CreatioIgnore]
        [JsonProperty("ТекущаяДолжностьКомпании_Key")]
        public Guid? ТекущаяДолжностьКомпании_Key { get; set; }

        [CreatioIgnore]
        [JsonProperty("ДатаПриемаНаРаботуВКомпанию")]
        public DateTime? ДатаПриемаНаРаботуВКомпанию { get; set; }

        [CreatioIgnore]
        [JsonProperty("ДатаУвольненияИзКомпании")]
        public DateTime? ДатаУвольненияИзКомпании { get; set; }

        [CreatioIgnore]
        [JsonProperty("ПоНаправлениюОргановЗанятости")]
        public Boolean? ПоНаправлениюОргановЗанятости { get; set; }

        [CreatioIgnore]
        [JsonProperty("ПерсональныеНадбавки")]
        public object[] ПерсональныеНадбавки { get; set; }
    }
}
