using System;
using System.Collections.Generic;
using System.Text;
using DatEx.OneC.DataModel.Auxilary;
using Newtonsoft.Json;

namespace DatEx.OneC.DataModel
{
    [OneC("Catalog_СотрудникиОрганизаций", "Catalog_СотрудникиОрганизаций", "Справочник.СотрудникиОрганизаций", "Справочник.СотрудникиОрганизаций")]
    [JsonObject("Catalog_СотрудникиОрганизаций")]
    public class Employee : OneCBaseHierarchicalLookup
    {
        [OneC("Guid?", "Физлицо_Key", "-", "-", Color = ConsoleColor.DarkBlue)]
        [JsonProperty("Физлицо_Key")]
        public Guid? PersonId { get; set; }

        [OneC("Catalog_ФизическиеЛица", "NavProp(Catalog_ФизическиеЛица)", "Справочник.ФизическиеЛица", "Физлицо", Color = ConsoleColor.Green)]
        [JsonIgnore]
        public Person NavProp_Person { get; set; }

        [OneC("Boolean?", "Актуальность", "Булево", "Актуальность", Color = ConsoleColor.DarkYellow)]
        [JsonProperty("Актуальность")]
        public Boolean? IsActual { get; set; }

        [OneC("Guid?", "Организация_Key", "-", "-", Color = ConsoleColor.DarkBlue)]
        [JsonProperty("Организация_Key")]
        public Guid? OrganizationId { get; set; }

        [OneC("Catalog_Организации", "NavProp(Catalog_Организации)", "Справочник.Организации", "Организация", Color = ConsoleColor.Green)]
        [JsonIgnore]
        public Organization NavProp_Organization { get; set; }

        [OneC("Guid?", "ОбособленноеПодразделение_Key", "-", "-")]
        [JsonProperty("ОбособленноеПодразделение_Key")]
        public Guid? DetachedUnitId { get; set; }

        [OneC("Catalog_Организации", "NavProp(ОбособленноеПодразделение)", "Справочник.Организации", "ОбособленноеПодразделение")]
        [JsonIgnore]
        public Organization NavProp_DetachedUnit { get; set; }

        [OneC("String", "ВидДоговора", "Перечисление.ВидыДоговоровСФизЛицами", "ВидДоговора")]
        [JsonProperty("ВидДоговора")]
        public String ContractType { get; set; }

        [OneC("String", "ВидЗанятости", "Перечисление.ВидыЗанятостиВОрганизации", "ВидЗанятости", Color = ConsoleColor.Green)]
        [JsonProperty("ВидЗанятости")]
        public String TypeOfEmployment { get; set; }

        [OneC("String", "НомерДоговора", "Строка", "НомерДоговора")]
        [JsonProperty("НомерДоговора")]
        public String ContractNumber { get; set; }

        [OneC("DateTime?", "ДатаДоговора", "Дата", "ДатаДоговора")]
        [JsonProperty("ДатаДоговора")]
        public DateTime? ContractDate { get; set; }

        [OneC("Guid?", "ГрафикРаботы_Key", "-", "-")]
        [JsonProperty("ГрафикРаботы_Key")]
        public Guid? WorkSheduleId { get; set; }

        [OneC("?", "Catalog_ГрафикРаботы", "СправочникСсылка.ГрафикиРаботы", "ГрафикРаботы")]
        [JsonIgnore]
        public Object WorkShedule_ { get; set; }

        [OneC("Guid?", "ПодразделениеОрганизации_Key", "-", "-", Color = ConsoleColor.DarkBlue)]
        [JsonProperty("ПодразделениеОрганизации_Key")]
        public Guid? OrganizationSubdivisionId { get; set; }

        [OneC("Catalog_ПодразделенияОрганизаций", "NavProp(Catalog_ПодразделенияОрганизаций)", "Справочник.ПодразделенияОрганизаций", "ПодразделениеОрганизации", Color = ConsoleColor.Green)]
        [JsonIgnore]
        public OrganizationSubdivision NavProp_OrganizationSubdivision { get; set; }

        [OneC("Guid?", "Должность_Key", "-", "-", Color = ConsoleColor.DarkBlue)]
        [JsonProperty("Должность_Key")]
        public Guid? PositionId { get; set; }

        [OneC("Catalog_ДолжностиОрганизаций", "NavProp(Должность_Key)", "Справочник.ДолжностиОрганизаций", "Должность", Color = ConsoleColor.Green)] 
        [JsonIgnore]
        public PositionInOrganization NavProp_Position { get; set; }

        [OneC("Double?", "ЗанимаемыхСтавок", "Число", "ЗанимаемыхСтавок")]
        [JsonProperty("ЗанимаемыхСтавок")]
        public Double? OccupiedBets { get; set; }

        [OneC("DateTime?", "ДатаНачала", "Дата", "ДатаНачала", Color = ConsoleColor.Green)]
        [JsonProperty("ДатаНачала")]
        public DateTime? StartDate { get; set; }

        [OneC("DateTime?", "ДатаОкончания", "Дата", "ДатаОкончания", Color = ConsoleColor.Green)]
        [JsonProperty("ДатаОкончания")]
        public DateTime? DueDate { get; set; }

        [OneC("Double?", "ИспытательныйСрок", "Число", "ИспытательныйСрок")]
        [JsonProperty("ИспытательныйСрок")]
        public Double? TrialPeriod { get; set; }

        [OneC("Guid?", "ВидРасчета_Key", "-", "-")]
        [JsonProperty("ВидРасчета_Key")]
        public Guid? CalculationTypeId { get; set; }

        [OneC("?", "NavProp(ВидРасчета)", "ПланВидовРасчета.ОсновныеНачисленияОрганизаций", "ВидРасчета")]
        [JsonIgnore]
        public Object NavProp_CalculationType { get; set; }

        [OneC("Double?", "ТарифнаяСтавка", "Число", "ТарифнаяСтавка")]
        [JsonProperty("ТарифнаяСтавка")]
        public Double? TariffRate { get; set; }

        [OneC("Guid?", "ВалютаТарифнойСтавки_Key", "", "")]
        [JsonProperty("ВалютаТарифнойСтавки_Key")]
        public Guid? TariffRateCurrencyId { get; set; }

        [OneC("?", "NavProp(ВалютаТарифнойСтавки)", "Справочник.Валюты", "ВалютаТарифнойСтавки")]
        [JsonIgnore]
        public Object NavProp_TariffRateCurrency { get; set; }

        [OneC("String", "ПостфиксНаименования", "Строка", "ПостфиксНаименования")]
        [JsonProperty("ПостфиксНаименования")]
        public String NamePostfix { get; set; }

        [OneC("Guid?", "ТарифныйРазряд_Key", "-", "-")]
        [JsonProperty("ТарифныйРазряд_Key")]
        public Guid? TariffCategoryId { get; set; }

        [OneC("?", "NavProp(ТарифныйРазряд)", "Справочник.ТарифныеРазряды", "ТарифныйРазряд")]
        [JsonIgnore]
        public Object NavProp_TariffCategory { get; set; }

        [OneC("Guid?", "ОсновноеНазначение_Key", "-", "-")]
        [JsonProperty("ОсновноеНазначение_Key")]
        public Guid? PrimaryPurposeId { get; set; }

        [OneC("Catalog_СотрудникиОрганизаций", "NavProp(ОсновноеНазначение)", "Справочник.СотрудникиОрганизаций", "ОсновноеНазначение")]
        [JsonIgnore]
        public Employee NavProp_PrimaryPurpose { get; set; }

        [OneC("Guid?", "ГруппаВзносов_Key", "-", "-")]
        [JsonProperty("ГруппаВзносов_Key")]
        public Guid? ContributionGroupId { get; set; }

        [OneC("?", "NavProp(ГруппаВзносов)", "Справочник.ГруппыВзносовВФонды", "ГруппаВзносов")]
        [JsonIgnore]
        public Object NavProp_ContributionGroup { get; set; }
        
        [OneC("Guid?", "ТекущееПодразделениеОрганизации_Key", "-", "-", Color = ConsoleColor.DarkBlue)]
        [JsonProperty("ТекущееПодразделениеОрганизации_Key")]
        public Guid? CurrentOrganizationSubdivisionId { get; set; }

        [OneC("Catalog_ПодразделенияОрганизаций", "NavProp(ТекущееПодразделениеОрганизации)", "Справочник.ПодразделенияОрганизаций", "ТекущееПодразделениеОрганизации", Color = ConsoleColor.Green)]
        [JsonIgnore]
        public OrganizationSubdivision NavProp_CurrentOrganizationSubdivision { get; set; }

        [OneC("Guid?", "ТекущаяДолжностьОрганизации_Key", "-", "-", Color = ConsoleColor.DarkBlue)]
        [JsonProperty("ТекущаяДолжностьОрганизации_Key")]
        public Guid? CurrentPositionInOrganizationId { get; set; }

        [OneC("Catalog_ДолжностиОрганизаций", "NavProp(ТекущаяДолжностьОрганизации)", "Справочник.ДолжностиОрганизаций", "ТекущаяДолжностьОрганизации", Color = ConsoleColor.Green)]
        [JsonIgnore]
        public PositionInOrganization NavProp_CurrentPositionInOrganization { get; set; }

        [OneC("DateTime?", "ДатаПриемаНаРаботу", "Дата", "ДатаПриемаНаРаботу", Color = ConsoleColor.Green)]
        [JsonProperty("ДатаПриемаНаРаботу")]
        public DateTime? DateOfEmployment { get; set; }

        [OneC("DateTime?", "ДатаУвольнения", "Дата", "ДатаУвольнения", Color = ConsoleColor.Green)]
        [JsonProperty("ДатаУвольнения")]
        public DateTime? DateOfDismisal { get; set; }

        [OneC("Guid?", "ТекущееПодразделениеКомпании_Key", "-", "-")]
        [JsonProperty("ТекущееПодразделениеКомпании_Key")]
        public Guid? CurrentCompanyDivisionId { get; set; }

        [OneC("Catalog_Подразделения", "NavProp(ТекущееПодразделениеКомпании)", "СправочникСсылка.Подразделения", "ТекущееПодразделениеКомпании")]
        [JsonIgnore]
        public Division NavProp_CurrentCompanyDivision { get; set; }

        [OneC("Guid?", "ТекущаяДолжностьКомпании_Key", "-", "-")]
        [JsonProperty("ТекущаяДолжностьКомпании_Key")]
        public Guid? CurrentCompanyPositionId { get; set; }

        [OneC("Catalog_ДолжностиОрганизаций", "NavProp(ТекущаяДолжностьКомпании)", "Справочник.ДолжностиОрганизаций", "ТекущаяДолжностьКомпании")]
        [JsonIgnore]
        public PositionInOrganization NavProp_CurrentCompanyPosition { get; set; }

        [OneC("DateTime?", "ДатаПриемаНаРаботуВКомпанию", "Дата", "ДатаПриемаНаРаботуВКомпанию")]
        [JsonProperty("ДатаПриемаНаРаботуВКомпанию")]
        public DateTime? DateOfEmploymentInCompany { get; set; }

        [OneC("DateTime?", "ДатаУвольненияИзКомпании", "Дата", "ДатаУвольненияИзКомпании")]
        [JsonProperty("ДатаУвольненияИзКомпании")]
        public DateTime? DateOfDismisalFromCompany { get; set; }

        [OneC("Guid?", "Контрагент_Key", "-", "-")]
        [JsonProperty("Контрагент_Key")]
        public Guid? ContractorId { get; set; }

        [OneC("Catalog_Контрагенты", "NavProp(Контрагент)", "Справочник.Контрагенты", "Контрагент")]
        [JsonIgnore]
        public Contractor NavProp_Contractor { get; set; }

        [OneC("Boolean?", "ПоНаправлениюОргановЗанятости", "Булево", "ПоНаправлениюОргановЗанятости")]
        [JsonProperty("ПоНаправлениюОргановЗанятости")]
        public Boolean? ByDirectionOfEmploymentOrganizations { get; set; }

        [OneC("String", "IdCreatio", "Строка", "IdCreatio", Color = ConsoleColor.Green)]
        [JsonProperty("IdCreatio")]
        public String IdCreatio { get; set; }

        public override String ToString() => $"{Description}";
    }
}
