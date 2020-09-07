using System;
using System.Collections.Generic;
using System.Text;
using DatEx.OneS.DataModel.Auxilary;
using Newtonsoft.Json;

namespace DatEx.OneS.DataModel
{
    [OneS("Catalog_СотрудникиОрганизаций", "Catalog_СотрудникиОрганизаций", "Справочник.СотрудникиОрганизаций", "Справочник.СотрудникиОрганизаций")]
    [JsonObject("Catalog_СотрудникиОрганизаций")]
    public class Employee : OneSBaseHierarchicalLookup
    {
        [OneS("Guid?", "Физлицо_Key", "-", "-", Color = ConsoleColor.Blue)]
        [JsonProperty("Физлицо_Key")]
        public Guid? PersonId { get; set; }

        [OneS("Catalog_ФизическиеЛица", "NavProp(Catalog_ФизическиеЛица)", "Справочник.ФизическиеЛица", "Физлицо", Color = ConsoleColor.Yellow)]
        [JsonIgnore]
        public Person NavProp_Person { get; set; }

        [OneS("Boolean?", "Актуальность", "Булево", "Актуальность")]
        [JsonProperty("Актуальность")]
        public Boolean? IsActual { get; set; }

        [OneS("Guid?", "Организация_Key", "-", "-", Color = ConsoleColor.Blue)]
        [JsonProperty("Организация_Key")]
        public Guid? OrganizationId { get; set; }

        [OneS("Catalog_Организации", "NavProp(Catalog_Организации)", "Справочник.Организации", "Организация", Color = ConsoleColor.Yellow)]
        [JsonIgnore]
        public Organization NavProp_Organization { get; set; }

        [OneS("Guid?", "ОбособленноеПодразделение_Key", "-", "-")]
        [JsonProperty("ОбособленноеПодразделение_Key")]
        public Guid? DetachedUnitId { get; set; }

        [OneS("Catalog_Организации", "NavProp(ОбособленноеПодразделение)", "Справочник.Организации", "ОбособленноеПодразделение")]
        [JsonIgnore]
        public Organization NavProp_DetachedUnit { get; set; }

        [OneS("String", "ВидДоговора", "Перечисление.ВидыДоговоровСФизЛицами", "ВидДоговора")]
        [JsonProperty("ВидДоговора")]
        public String ContractType { get; set; }

        [OneS("String", "ВидЗанятости", "Перечисление.ВидыЗанятостиВОрганизации", "ВидЗанятости", Color = ConsoleColor.Yellow)]
        [JsonProperty("ВидЗанятости")]
        public String TypeOfEmployment { get; set; }

        [OneS("String", "НомерДоговора", "Строка", "НомерДоговора")]
        [JsonProperty("НомерДоговора")]
        public String ContractNumber { get; set; }

        [OneS("DateTime?", "ДатаДоговора", "Дата", "ДатаДоговора")]
        [JsonProperty("ДатаДоговора")]
        public DateTime? ContractDate { get; set; }

        [OneS("Guid?", "ГрафикРаботы_Key", "-", "-")]
        [JsonProperty("ГрафикРаботы_Key")]
        public Guid? WorkSheduleId { get; set; }

        [OneS("?", "Catalog_ГрафикРаботы", "СправочникСсылка.ГрафикиРаботы", "ГрафикРаботы")]
        [JsonIgnore]
        public Object WorkShedule_ { get; set; }

        [OneS("Guid?", "ПодразделениеОрганизации_Key", "-", "-", Color = ConsoleColor.Blue)]
        [JsonProperty("ПодразделениеОрганизации_Key")]
        public Guid? OrganizationSubdivisionId { get; set; }

        [OneS("Catalog_ПодразделенияОрганизаций", "NavProp(Catalog_ПодразделенияОрганизаций)", "Справочник.ПодразделенияОрганизаций", "ПодразделениеОрганизации", Color = ConsoleColor.Yellow)]
        [JsonIgnore]
        public OrganizationSubdivision NavProp_OrganizationSubdivision { get; set; }

        [OneS("Guid?", "Должность_Key", "-", "-", Color = ConsoleColor.Blue)]
        [JsonProperty("Должность_Key")]
        public Guid? PositionId { get; set; }

        [OneS("Catalog_ДолжностиОрганизаций", "NavProp(Должность_Key)", "Справочник.ДолжностиОрганизаций", "Должность", Color = ConsoleColor.Yellow)] 
        [JsonIgnore]
        public PositionInOrganization NavProp_Position { get; set; }

        [OneS("Double?", "ЗанимаемыхСтавок", "Число", "ЗанимаемыхСтавок")]
        [JsonProperty("ЗанимаемыхСтавок")]
        public Double? OccupiedBets { get; set; }

        [OneS("DateTime?", "ДатаНачала", "Дата", "ДатаНачала", Color = ConsoleColor.Yellow)]
        [JsonProperty("ДатаНачала")]
        public DateTime? StartDate { get; set; }

        [OneS("DateTime?", "ДатаОкончания", "Дата", "ДатаОкончания", Color = ConsoleColor.Yellow)]
        [JsonProperty("ДатаОкончания")]
        public DateTime? DueDate { get; set; }

        [OneS("Double?", "ИспытательныйСрок", "Число", "ИспытательныйСрок")]
        [JsonProperty("ИспытательныйСрок")]
        public Double? ProbationDueDate { get; set; }

        [OneS("Guid?", "ВидРасчета_Key", "-", "-")]
        [JsonProperty("ВидРасчета_Key")]
        public Guid? CalculationTypeId { get; set; }

        [OneS("?", "NavProp(ВидРасчета)", "ПланВидовРасчета.ОсновныеНачисленияОрганизаций", "ВидРасчета")]
        [JsonIgnore]
        public Object NavProp_CalculationType { get; set; }

        [OneS("Double?", "ТарифнаяСтавка", "Число", "ТарифнаяСтавка")]
        [JsonProperty("ТарифнаяСтавка")]
        public Double? TariffRate { get; set; }

        [OneS("Guid?", "ВалютаТарифнойСтавки_Key", "", "")]
        [JsonProperty("ВалютаТарифнойСтавки_Key")]
        public Guid? TariffRateCurrencyId { get; set; }

        [OneS("?", "NavProp(ВалютаТарифнойСтавки)", "Справочник.Валюты", "ВалютаТарифнойСтавки")]
        [JsonIgnore]
        public Object NavProp_TariffRateCurrency { get; set; }

        [OneS("String", "ПостфиксНаименования", "Строка", "ПостфиксНаименования")]
        [JsonProperty("ПостфиксНаименования")]
        public String NamePostfix { get; set; }

        [OneS("Guid?", "ТарифныйРазряд_Key", "-", "-")]
        [JsonProperty("ТарифныйРазряд_Key")]
        public Guid? TariffCategoryId { get; set; }

        [OneS("?", "NavProp(ТарифныйРазряд)", "Справочник.ТарифныеРазряды", "ТарифныйРазряд")]
        [JsonIgnore]
        public Object NavProp_TariffCategory { get; set; }

        [OneS("Guid?", "ОсновноеНазначение_Key", "-", "-")]
        [JsonProperty("ОсновноеНазначение_Key")]
        public Guid? PrimaryPurposeId { get; set; }

        [OneS("Catalog_СотрудникиОрганизаций", "NavProp(ОсновноеНазначение)", "Справочник.СотрудникиОрганизаций", "ОсновноеНазначение")]
        [JsonIgnore]
        public Employee NavProp_PrimaryPurpose { get; set; }

        [OneS("Guid?", "ГруппаВзносов_Key", "-", "-")]
        [JsonProperty("ГруппаВзносов_Key")]
        public Guid? ContributionGroupId { get; set; }

        [OneS("?", "NavProp(ГруппаВзносов)", "Справочник.ГруппыВзносовВФонды", "ГруппаВзносов")]
        [JsonIgnore]
        public Object NavProp_ContributionGroup { get; set; }
        
        [OneS("Guid?", "ТекущееПодразделениеОрганизации_Key", "-", "-", Color = ConsoleColor.Blue)]
        [JsonProperty("ТекущееПодразделениеОрганизации_Key")]
        public Guid? CurrentOrganizationSubdivisionId { get; set; }

        [OneS("Catalog_ПодразделенияОрганизаций", "NavProp(ТекущееПодразделениеОрганизации)", "Справочник.ПодразделенияОрганизаций", "ТекущееПодразделениеОрганизации", Color = ConsoleColor.Yellow)]
        [JsonIgnore]
        public OrganizationSubdivision NavProp_CurrentOrganizationSubdivision { get; set; }

        [OneS("Guid?", "ТекущаяДолжностьОрганизации_Key", "-", "-", Color = ConsoleColor.Blue)]
        [JsonProperty("ТекущаяДолжностьОрганизации_Key")]
        public Guid? CurrentPositionInOrganizationId { get; set; }

        [OneS("Catalog_ДолжностиОрганизаций", "NavProp(ТекущаяДолжностьОрганизации)", "Справочник.ДолжностиОрганизаций", "ТекущаяДолжностьОрганизации", Color = ConsoleColor.Yellow)]
        [JsonIgnore]
        public PositionInOrganization NavProp_CurrentPositionInOrganization { get; set; }

        [OneS("DateTime?", "ДатаПриемаНаРаботу", "Дата", "ДатаПриемаНаРаботу", Color = ConsoleColor.Yellow)]
        [JsonProperty("ДатаПриемаНаРаботу")]
        public DateTime? DateOfEmployment { get; set; }

        [OneS("DateTime?", "ДатаУвольнения", "Дата", "ДатаУвольнения", Color = ConsoleColor.Yellow)]
        [JsonProperty("ДатаУвольнения")]
        public DateTime? DateOfDismisal { get; set; }

        [OneS("Guid?", "ТекущееПодразделениеКомпании_Key", "-", "-")]
        [JsonProperty("ТекущееПодразделениеКомпании_Key")]
        public Guid? CurrentCompanyDivisionId { get; set; }

        [OneS("Catalog_Подразделения", "NavProp(ТекущееПодразделениеКомпании)", "СправочникСсылка.Подразделения", "ТекущееПодразделениеКомпании")]
        [JsonIgnore]
        public Division NavProp_CurrentCompanyDivision { get; set; }

        [OneS("Guid?", "ТекущаяДолжностьКомпании_Key", "-", "-")]
        [JsonProperty("ТекущаяДолжностьКомпании_Key")]
        public Guid? CurrentCompanyPositionId { get; set; }

        [OneS("Catalog_ДолжностиОрганизаций", "NavProp(ТекущаяДолжностьКомпании)", "Справочник.ДолжностиОрганизаций", "ТекущаяДолжностьКомпании")]
        [JsonIgnore]
        public PositionInOrganization NavProp_CurrentCompanyPosition { get; set; }

        [OneS("DateTime?", "ДатаПриемаНаРаботуВКомпанию", "Дата", "ДатаПриемаНаРаботуВКомпанию")]
        [JsonProperty("ДатаПриемаНаРаботуВКомпанию")]
        public DateTime? DateOfEmploymentInCompany { get; set; }

        [OneS("DateTime?", "ДатаУвольненияИзКомпании", "Дата", "ДатаУвольненияИзКомпании")]
        [JsonProperty("ДатаУвольненияИзКомпании")]
        public DateTime? DateOfDismisalFromCompany { get; set; }

        [OneS("Guid?", "Контрагент_Key", "-", "-")]
        [JsonProperty("Контрагент_Key")]
        public Guid? ContractorId { get; set; }

        [OneS("Catalog_Контрагенты", "NavProp(Контрагент)", "Справочник.Контрагенты", "Контрагент")]
        [JsonIgnore]
        public Contractor NavProp_Contractor { get; set; }

        [OneS("Boolean?", "ПоНаправлениюОргановЗанятости", "Булево", "ПоНаправлениюОргановЗанятости")]
        [JsonProperty("ПоНаправлениюОргановЗанятости")]
        public Boolean? ByDirectionOfEmploymentOrganizations { get; set; }

        [OneS("String", "IdCreatio", "Строка", "IdCreatio", Color = ConsoleColor.Yellow)]
        [JsonProperty("IdCreatio")]
        public String IdCreatio { get; set; }

        public override String ToString() => $"{Description}";
    }
}
