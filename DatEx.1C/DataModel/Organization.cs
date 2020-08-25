
namespace DatEx.OneC.DataModel
{
    using DatEx.OneC.DataModel.Auxilary;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Text;

    [OneC("Catalog_Организации", "Catalog_Организации", "Организации", "Организации")]
    [JsonObject("Catalog_Организации")]
    public class Organization : OneCBaseHierarchicalLookup
    {
        [OneC("String", "Префикс", "Строка", "Префикс", Color = ConsoleColor.Yellow)]
        [JsonProperty("Префикс")]
        public String Prefix { get; set; }

        [OneC("String", "ГоловнаяОрганизация_Key", "СправочникСсылка.Организации", "ГоловнаяОрганизация")]
        [JsonProperty("ГоловнаяОрганизация_Key")]
        public Guid? PrimaryOrganizationId { get; set; }

        [OneC("?", "NavProp(ГоловнаяОрганизация)", "Справочник.Организации", "Головная организация")]
        [JsonIgnore]
        public Object NavProp_PrimaryOrganization { get; set; }

        [OneC("String", "НаименованиеПолное", "Строка", "НаименованиеПолное", Color = ConsoleColor.Yellow)]
        [JsonProperty("НаименованиеПолное")]
        public String FullName { get; set; }

        [OneC("String", "ЮрФизЛицо", "Перечисление.ЮрФизЛицо", "ЮрФизЛицо", Color = ConsoleColor.Yellow)]
        [JsonProperty("ЮрФизЛицо")]
        public String LegalOrNaturalPerson { get; set; }

        [OneC("Guid?", "ОсновнойБанковскийСчет_Key", "-", "-")]
        [JsonProperty("ОсновнойБанковскийСчет_Key")]
        public Guid? PrimaryBankAccount { get; set; }

        [OneC("?", "NavProp(ОсновнойБанковскийСчет)", "Справочник.БанковскиеСчета", "ОсновнойБанковскийСчет")]
        [JsonIgnore]
        public Object NavProp_PrimaryBankAccount { get; set; }

        [OneC("String", "НаименованиеПлательщикаПриПеречисленииНалогов", "Строка", "НаименованиеПлательщикаПриПеречисленииНалогов")]
        [JsonProperty("НаименованиеПлательщикаПриПеречисленииНалогов")]
        public String NameOfPayerWhnTransferringTaxes { get; set; }

        [OneC("Guid?", "ИндивидуальныйПредприниматель_Key", "-", "")]
        [JsonProperty("ИндивидуальныйПредприниматель_Key")]
        public Guid? IndividualEntrepreneurId { get; set; }

        [OneC("Catalog_ФизическиеЛица", "NavProp()", "СправочникСсылка.ФизическиеЛица", "ИндивидуальныйПредприниматель")]
        [JsonIgnore]
        public Person NavProp_IndividualEntrepreneur { get; set; }

        [OneC("Boolean", "ОтражатьВРегламентированномУчете", "Булево", "ОтражатьВРегламентированномУчете")]
        [JsonProperty("ОтражатьВРегламентированномУчете")]
        public Boolean ShowInRegulatedAccounting { get; set; }

        [OneC("Guid?", "БанковскийСчетДляРасчетовСФСС_Key", "-", "-")]
        [JsonProperty("БанковскийСчетДляРасчетовСФСС_Key")]
        public Guid? BankAccountForSettlementsSFFSId { get; set; }

        [OneC("?", "NavProp(БанковскийСчетДляРасчетовСФСС)", "Справочник.БанковскиеСчета", "БанковскийСчетДляРасчетовСФСС")]
        [JsonIgnore]
        public Object NavProp_BankAccountForSettlementsSFFS { get; set; }

        [OneC("DateTime?", "ДатаНачалаИспользованияЗвит1С", "Строка", "ДатаНачалаИспользованияЗвит1С")]
        [JsonProperty("ДатаНачалаИспользованияЗвит1С")]
        public DateTime? DateStartOfUseInOneCReport { get; set; }

        [OneC("Boolean?", "ВестиУчетРегистраБухгалтерииПоОбособленнымПодразделениям", "Булево", "ВестиУчетРегистраБухгалтерииПоОбособленнымПодразделениям)")]
        [JsonProperty("ВестиУчетРегистраБухгалтерииПоОбособленнымПодразделениям")]
        public Boolean? MaintainAccountingRegisterAccountingBySeparateSubdivisions { get; set; }

        [OneC("String", "IdCreatio", "Строка", "IdCreatio")]
        [JsonProperty("IdCreatio")]
        public String IdCreatio { get; set; }
        
        public override string ToString() => $"[{Prefix}] {Description}";
    }
}
