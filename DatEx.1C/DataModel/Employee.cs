using System;
using System.Collections.Generic;
using System.Text;
using DatEx.OneC.DataModel.Auxilary;
using Newtonsoft.Json;

namespace DatEx.OneC.DataModel
{
    [CreatioTypeMap("DatEx.Creatio.DataModel.ITIS.Contact", "Catalog_СотрудникиОрганизаций")]
    [JsonObject("Catalog_СотрудникиОрганизаций")]
    public class Employee : OneCBaseLookup
    {
        [CreatioPropertyMap("Guid", "Id", "IdCreatio")]
        [JsonProperty("IdCreatio")]
        public String IdCreatio { get; set; }

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

        // ───── Не используемые поля ───────────────────────────────────────────────────────────────────────────────────────────────────────────────

        [CreatioIgnore]
        [JsonProperty("ОбособленноеПодразделение_Key")]
        public Guid? DetachedUnitId { get; set; }

        [CreatioIgnore]
        [JsonProperty("НомерДоговора")]
        public String ContractNumber { get; set; }


        [CreatioIgnore]
        [JsonProperty("ГрафикРаботы_Key")]
        public Guid? WorkSheduleId { get; set; }

        [CreatioIgnore]
        [JsonProperty("ЗанимаемыхСтавок")]
        public Double? OccupiedBets { get; set; }


        [CreatioIgnore]
        [JsonProperty("ДатаДоговора")]
        public DateTime? ContractDate { get; set; }

        [CreatioIgnore]
        [JsonProperty("ИспытательныйСрок")]
        public Double? TrialPeriod { get; set; }

        [CreatioIgnore]
        [JsonProperty("ВидРасчета_Key")]
        public Guid? CalculationTypeId { get; set; }

        [CreatioIgnore]
        [JsonProperty("ТарифнаяСтавка")]
        public Double? TariffRate { get; set; }

        [CreatioIgnore]
        [JsonProperty("ВалютаТарифнойСтавки_Key")]
        public Guid? TariffRateCurrencyId { get; set; }

        [CreatioIgnore]
        [JsonProperty("ПостфиксНаименования")]
        public String NamePostfix { get; set; }

        [CreatioIgnore]
        [JsonProperty("ТарифныйРазряд_Key")]
        public Guid? TariffCategoryId { get; set; }

        [CreatioIgnore]
        [JsonProperty("ОсновноеНазначение_Key")]
        public Guid? PrimaryPurposeId { get; set; }

        [CreatioIgnore]
        [JsonProperty("ГруппаВзносов_Key")]
        public Guid? ContributionGroupId { get; set; }

        [CreatioIgnore]
        [JsonProperty("ТекущееПодразделениеКомпании_Key")]
        public Guid? CurrentCompanyDivisionId { get; set; }

        [CreatioIgnore]
        [JsonProperty("ТекущаяДолжностьКомпании_Key")]
        public Guid? CurrentCompanyPositionId { get; set; }

        [CreatioIgnore]
        [JsonProperty("ДатаПриемаНаРаботуВКомпанию")]
        public DateTime? DateOfEmploymentInCompany { get; set; }

        [CreatioIgnore]
        [JsonProperty("ДатаУвольненияИзКомпании")]
        public DateTime? DateOfDismisalFromCompany { get; set; }

        [CreatioIgnore]
        [JsonProperty("ПоНаправлениюОргановЗанятости")]
        public Boolean? ByDirectionOfEmploymentOrganizations { get; set; }

        [CreatioIgnore]
        [JsonProperty("ПерсональныеНадбавки")]
        public object[] PersonalAllowances { get; set; }
    }
}
