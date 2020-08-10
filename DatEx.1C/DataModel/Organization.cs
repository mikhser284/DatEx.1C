
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
        [CreatioPropertyMap("Guid", "Id", "IdCreatio")]
        [JsonProperty("IdCreatio")]
        public String IdCreatio { get; set; }


        [CreatioAux]
        [JsonProperty("Префикс")]
        public String Prefix { get; set; }

        [CreatioAux]
        [JsonProperty("ГоловнаяОрганизация_Key")]
        public Guid? PrimaryOrganizationId { get; set; }

        [CreatioAux]
        [JsonProperty("НаименованиеПолное")]
        public String FullName { get; set; }

        [CreatioAux]
        [JsonProperty("ЮрФизЛицо")]
        public String PropName { get; set; }

        [CreatioAux]
        [JsonProperty("ОсновнойБанковскийСчет_Key")]
        public Guid? PrimaryBankAccount { get; set; }

        [CreatioAux]
        [JsonProperty("НаименованиеПлательщикаПриПеречисленииНалогов")]
        public String NameOfPayerWhnTransferringTaxes { get; set; }

        [CreatioAux]
        [JsonProperty("ИндивидуальныйПредприниматель_Key")]
        public Guid? ИндивидуальныйПредприниматель_Key { get; set; }

        [CreatioAux]
        [JsonProperty("ОтражатьВРегламентированномУчете")]
        public Boolean ShowInRegulatedAccounting { get; set; }

        [CreatioAux]
        [JsonProperty("БанковскийСчетДляРасчетовСФСС_Key")]
        public Guid? BankAccountForSettlementsSFFSId { get; set; }

        [CreatioAux]
        [JsonProperty("ДатаНачалаИспользованияЗвит1С")]
        public DateTime? DateStartOfUseInOneCReport { get; set; }

        [CreatioAux]
        [JsonProperty("ВестиУчетРегистраБухгалтерииПоОбособленнымПодразделениям")]
        public Boolean? MaintainAccountingRegisterAccountingBySeparateSubdivisions { get; set; }

        public override string ToString()
        {
            return $"[{Prefix}] {Description}";
        }
    }
}
