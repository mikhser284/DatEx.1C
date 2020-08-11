using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DatEx.OneC.DataModel.Auxilary;
using Newtonsoft.Json;

namespace DatEx.OneC.DataModel
{
    
    [JsonObject("Catalog_Контрагенты")]
    public class Contractor : OneCBaseHierarchicalLookup
    {
        
        [JsonProperty("IdCreatio")]
        public String IdCreatio { get; set; }

        
        [JsonProperty("Ref_Key")]
        public Guid Id { get; set; }

        
        [JsonProperty("НеЯвляетсяРезидентом")]
        public Boolean? IsNotResident { get; set; }

        
        [JsonProperty("НаименованиеПолное")]
        public String FullName { get; set; }

        
        [JsonProperty("КодПоЕДРПОУ")]
        public String CodeOfEdrpo { get; set; }

        
        [JsonProperty("ИНН")]
        public String INN { get; set; }

        
        [JsonProperty("Покупатель")]
        public Boolean? IsBuyer { get; set; }

        
        [JsonProperty("Поставщик")]
        public Boolean? IsProvider { get; set; }

        
        [JsonProperty("ЮрФизЛицо")]
        public String LegalOrIndividual { get; set; }

        // ····· Служебные или информативные поля ···································································································

        
        [JsonProperty("Комментарий")]
        public String Comment { get; set; }

        
        [JsonProperty("ДополнительноеОписание")]
        public String AuxilaryDescription { get; set; }

        // ───── Не используемые поля ───────────────────────────────────────────────────────────────────────────────────────────────────────────────

        
        [JsonProperty("ГоловнойКонтрагент_Key")]
        public Guid? PrimaryContractorId { get; set; }

        
        [JsonProperty("ИсточникИнформацииПриОбращении_Key")]
        public Guid? SourceOfInformationWhenContactingId { get; set; }

        
        [JsonProperty("ОсновнойБанковскийСчет_Key")]
        public Guid? PrimaryBankAccountId { get; set; }

        
        [JsonProperty("ОсновнойДоговорКонтрагента_Key")]
        public Guid? PrimaryConctactOfContractorId { get; set; }

        
        [JsonProperty("ОсновнойВидДеятельности_Key")]
        public Guid? PrimaryActivityOfContractorId { get; set; }

        
        [JsonProperty("ДокументУдостоверяющийЛичность")]
        public String IdentityDocument { get; set; }

        
        [JsonProperty("ОсновнойМенеджерПокупателя_Key")]
        public Guid? MainBuyerManagerId { get; set; }

        
        [JsonProperty("РасписаниеРаботыСтрокой")]
        public String WorkSheduleString { get; set; }

        
        [JsonProperty("СрокВыполненияЗаказаПоставщиком")]
        public Int16? TimeOfOrderExecutionBySupplier { get; set; }

        
        [JsonProperty("ОсновноеКонтактноеЛицо_Key")]
        public Guid? PrimaryContactPersonId { get; set; }

        
        [JsonProperty("НомерСвидетельства")]
        public String CertificateNumber { get; set; }

        
        [JsonProperty("ГруппаДоступаКонтрагента_Key")]
        public Guid? ContractorAccessGroupId { get; set; }

        
        [JsonProperty("ДополнятьНаименованиеАдресДаннымиГоловногоКонтрагентаВНН")]
        public Boolean? ShouldSupplementNameAddressDataHeadContractorVNN { get; set; }

        
        [JsonProperty("КодФилиала")]
        public String CodeOfBranch { get; set; }

        
        [JsonProperty("ИспользоватьЭДО1СЗвит")]
        public Boolean? ShouldUseEDO1Report { get; set; }

        
        [JsonProperty("ВидыДеятельности")]
        public List<Object> Activities { get; set; }

        
        [JsonProperty("МенеджерыПокупателя")]
        public List<Object> BuyersManagers { get; set; }

        
        [JsonProperty("Parent")]
        public String ParentNavigationLinkUrl { get; set; }

        
        [JsonProperty("ГоловнойКонтрагентnavigationLinkUrl")]
        public String HeadContractorNavigationLinkUrl { get; set; }

        public override String ToString() => $"{FullName} {CodeOfEdrpo}";
    }
}