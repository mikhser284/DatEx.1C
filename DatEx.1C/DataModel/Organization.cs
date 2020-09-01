
namespace DatEx.OneC.DataModel
{
    using DatEx.OneC.DataModel.Auxilary;
    using Newtonsoft.Json;
    using System;

    [OneS("Catalog_Организации", "Catalog_Организации", "Организации", "Организации")]
    [JsonObject("Catalog_Организации")]
    public class Organization : OneCBaseHierarchicalLookup
    {
        /// <summary> Связанный объект. Связь с контрагентом (из регистра сведений собственные контрагенты) </summary>
        public IROwnContracror RelatedObj_LinkWithContractor { get; set; }

        /// <summary> Связанный объект. Связанный контрагент </summary>
        public Contractor RelatedObj_Contractor { get; set; }

        [OneS("String", "Префикс", "Строка", "Префикс", Color = ConsoleColor.Yellow)]
        [JsonProperty("Префикс")]
        public String Prefix { get; set; }

        [OneS("String", "ГоловнаяОрганизация_Key", "СправочникСсылка.Организации", "ГоловнаяОрганизация")]
        [JsonProperty("ГоловнаяОрганизация_Key")]
        public Guid? PrimaryOrganizationId { get; set; }

        [OneS("?", "NavProp(ГоловнаяОрганизация)", "Справочник.Организации", "Головная организация")]
        [JsonIgnore]
        public Object NavProp_PrimaryOrganization { get; set; }

        [OneS("String", "НаименованиеПолное", "Строка", "НаименованиеПолное", Color = ConsoleColor.Yellow)]
        [JsonProperty("НаименованиеПолное")]
        public String FullName { get; set; }

        [OneS("String", "ЮрФизЛицо", "Перечисление.ЮрФизЛицо", "ЮрФизЛицо", Color = ConsoleColor.Yellow)]
        [JsonProperty("ЮрФизЛицо")]
        public String LegalOrNaturalPerson { get; set; }

        [OneS("Guid?", "ОсновнойБанковскийСчет_Key", "-", "-")]
        [JsonProperty("ОсновнойБанковскийСчет_Key")]
        public Guid? PrimaryBankAccount { get; set; }

        [OneS("?", "NavProp(ОсновнойБанковскийСчет)", "Справочник.БанковскиеСчета", "ОсновнойБанковскийСчет")]
        [JsonIgnore]
        public Object NavProp_PrimaryBankAccount { get; set; }

        [OneS("String", "НаименованиеПлательщикаПриПеречисленииНалогов", "Строка", "НаименованиеПлательщикаПриПеречисленииНалогов")]
        [JsonProperty("НаименованиеПлательщикаПриПеречисленииНалогов")]
        public String NameOfPayerWhnTransferringTaxes { get; set; }

        [OneS("Guid?", "ИндивидуальныйПредприниматель_Key", "-", "")]
        [JsonProperty("ИндивидуальныйПредприниматель_Key")]
        public Guid? IndividualEntrepreneurId { get; set; }

        [OneS("Catalog_ФизическиеЛица", "NavProp()", "СправочникСсылка.ФизическиеЛица", "ИндивидуальныйПредприниматель")]
        [JsonIgnore]
        public Person NavProp_IndividualEntrepreneur { get; set; }

        [OneS("Boolean", "ОтражатьВРегламентированномУчете", "Булево", "ОтражатьВРегламентированномУчете")]
        [JsonProperty("ОтражатьВРегламентированномУчете")]
        public Boolean ShowInRegulatedAccounting { get; set; }

        [OneS("Guid?", "БанковскийСчетДляРасчетовСФСС_Key", "-", "-")]
        [JsonProperty("БанковскийСчетДляРасчетовСФСС_Key")]
        public Guid? BankAccountForSettlementsSFFSId { get; set; }

        [OneS("?", "NavProp(БанковскийСчетДляРасчетовСФСС)", "Справочник.БанковскиеСчета", "БанковскийСчетДляРасчетовСФСС")]
        [JsonIgnore]
        public Object NavProp_BankAccountForSettlementsSFFS { get; set; }

        [OneS("DateTime?", "ДатаНачалаИспользованияЗвит1С", "Строка", "ДатаНачалаИспользованияЗвит1С")]
        [JsonProperty("ДатаНачалаИспользованияЗвит1С")]
        public DateTime? DateStartOfUseInOneCReport { get; set; }

        [OneS("Boolean?", "ВестиУчетРегистраБухгалтерииПоОбособленнымПодразделениям", "Булево", "ВестиУчетРегистраБухгалтерииПоОбособленнымПодразделениям)")]
        [JsonProperty("ВестиУчетРегистраБухгалтерииПоОбособленнымПодразделениям")]
        public Boolean? MaintainAccountingRegisterAccountingBySeparateSubdivisions { get; set; }

        [OneS("String", "IdCreatio", "Строка", "IdCreatio")]
        [JsonProperty("IdCreatio")]
        public String IdCreatio { get; set; }
        
        public override string ToString() => $"[{Prefix}] {Description}";
    }
}
