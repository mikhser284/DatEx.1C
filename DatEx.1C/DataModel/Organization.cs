
namespace DatEx.OneC.DataModel
{
    using DatEx.OneC.DataModel.Auxilary;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Text;

    
    [JsonObject("Catalog_Организации")]
    public class Organization : OneCBaseHierarchicalLookup
    {
        
        [JsonProperty("IdCreatio")]
        public String IdCreatio { get; set; }


        
        [JsonProperty("Префикс")]
        public String Prefix { get; set; }

        
        [JsonProperty("ГоловнаяОрганизация_Key")]
        public Guid? PrimaryOrganizationId { get; set; }

        
        [JsonProperty("НаименованиеПолное")]
        public String FullName { get; set; }

        
        [JsonProperty("ЮрФизЛицо")]
        public String PropName { get; set; }

        
        [JsonProperty("ОсновнойБанковскийСчет_Key")]
        public Guid? PrimaryBankAccount { get; set; }

        
        [JsonProperty("НаименованиеПлательщикаПриПеречисленииНалогов")]
        public String NameOfPayerWhnTransferringTaxes { get; set; }

        
        [JsonProperty("ИндивидуальныйПредприниматель_Key")]
        public Guid? ИндивидуальныйПредприниматель_Key { get; set; }

        
        [JsonProperty("ОтражатьВРегламентированномУчете")]
        public Boolean ShowInRegulatedAccounting { get; set; }

        
        [JsonProperty("БанковскийСчетДляРасчетовСФСС_Key")]
        public Guid? BankAccountForSettlementsSFFSId { get; set; }

        
        [JsonProperty("ДатаНачалаИспользованияЗвит1С")]
        public DateTime? DateStartOfUseInOneCReport { get; set; }

        
        [JsonProperty("ВестиУчетРегистраБухгалтерииПоОбособленнымПодразделениям")]
        public Boolean? MaintainAccountingRegisterAccountingBySeparateSubdivisions { get; set; }

        public override string ToString()
        {
            return $"[{Prefix}] {Description}";
        }
    }
}
