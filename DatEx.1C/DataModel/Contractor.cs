using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DatEx.OneC.DataModel.Auxilary;
using Newtonsoft.Json;

namespace DatEx.OneC.DataModel
{
    [CreatioTypeMap("Account", "Catalog_Контрагенты")]
    [JsonObject("Catalog_Контрагенты")]
    public class Contractor : OneCBase
    {
        [CreatioPropertyMap("Уникальный идентификатор (guid)", "Id", "IdCreatio")]
        [JsonProperty("IdCreatio")]
        public String IdCreatio { get; set; }

        [CreatioPropertyMap("??? Строка (250)", "IdOneC", "Ref_Key")]
        [JsonProperty("Ref_Key")]
        public Guid Id { get; set; }

        [CreatioPropertyMap("Справочник<???>", "ITISCounterpartyLegalStatus", "НеЯвляетсяРезидентом")]
        [JsonProperty("НеЯвляетсяРезидентом")]
        public Boolean? IsNotResident { get; set; }

        [CreatioPropertyMap("Строка (250)", "Name", "Description")]
        [JsonProperty("Description")]
        public String Description { get; set; }

        [CreatioPropertyMap("Строка (250)", "Name", "НаименованиеПолное")]
        [JsonProperty("НаименованиеПолное")]
        public String FullName { get; set; }

        [CreatioPropertyMap("Строка (50)", "KPP", "КодПоЕДРПОУ")]
        [JsonProperty("КодПоЕДРПОУ")]
        public String CodeOfEdrpo { get; set; }

        [CreatioPropertyMap("Строка (50)", "INN", "ИНН")]
        [JsonProperty("ИНН")]
        public String INN { get; set; }

        [CreatioPropertyMap("Справочник<Тип контрагента>", "Type", "Покупатель", Remarks = "Зависит от полей Поставщик и Покупатель (см. описание маппингов)")]
        [JsonProperty("Покупатель")]
        public Boolean? IsBuyer { get; set; }

        [CreatioPropertyMap("Справочник<Тип контрагента>", "Type", "Поставщик", Remarks = "Зависит от полей Поставщик и Покупатель (см. описание маппингов)")]
        [JsonProperty("Поставщик")]
        public Boolean? IsProvider { get; set; }

        [CreatioPropertyMap("Справочник<Форма собственности контрагента>", "Ownership", "ЮрФизЛицо", Remarks = "см. описание маппингов")]
        [JsonProperty("ЮрФизЛицо")]
        public String LegalOrIndividual { get; set; }

        // ····· Служебные или информативные поля ···································································································
        
        [CreatioAux]
        [JsonProperty("IsFolder")]
        public Boolean? IsFolder { get; set; }

        [CreatioAux]
        [JsonProperty("DeletionMark")]
        public Boolean? DeletionMark { get; set; }

        [CreatioAux]
        [JsonProperty("Code")]
        public String Code { get; set; }

        [CreatioAux]
        [JsonProperty("Parent_Key")]
        public Guid? ParentId { get; set; }

        [CreatioAux]
        [JsonProperty("Комментарий")]
        public String Comment { get; set; }

        [CreatioAux]
        [JsonProperty("ДополнительноеОписание")]
        public String AuxilaryDescription { get; set; }

        // ───── Не используемые поля ───────────────────────────────────────────────────────────────────────────────────────────────────────────────

        [CreatioIgnore]
        [JsonProperty("Predefined")]
        public Boolean? Predefined { get; set; }

        [CreatioIgnore]
        [JsonProperty("PredefinedDataName")]
        public String PredefinedDataName { get; set; }

        [CreatioIgnore]
        [JsonProperty("DataVersion")]
        public String DataVersion { get; set; }

        [CreatioIgnore]
        [JsonProperty("ГоловнойКонтрагент_Key")]
        public Guid? PrimaryContractorId { get; set; }

        [CreatioIgnore]
        [JsonProperty("ИсточникИнформацииПриОбращении_Key")]
        public Guid? SourceOfInformationWhenContactingId { get; set; }

        [CreatioIgnore]
        [JsonProperty("ОсновнойБанковскийСчет_Key")]
        public Guid? PrimaryBankAccountId { get; set; }

        [CreatioIgnore]
        [JsonProperty("ОсновнойДоговорКонтрагента_Key")]
        public Guid? PrimaryConctactOfContractorId { get; set; }

        [CreatioIgnore]
        [JsonProperty("ОсновнойВидДеятельности_Key")]
        public Guid? PrimaryActivityOfContractorId { get; set; }

        [CreatioIgnore]
        [JsonProperty("ДокументУдостоверяющийЛичность")]
        public String IdentityDocument { get; set; }

        [CreatioIgnore]
        [JsonProperty("ОсновнойМенеджерПокупателя_Key")]
        public Guid? MainBuyerManagerId { get; set; }

        [CreatioIgnore]
        [JsonProperty("РасписаниеРаботыСтрокой")]
        public String WorkSheduleString { get; set; }

        [CreatioIgnore]
        [JsonProperty("СрокВыполненияЗаказаПоставщиком")]
        public Int16? TimeOfOrderExecutionBySupplier { get; set; }

        [CreatioIgnore]
        [JsonProperty("ОсновноеКонтактноеЛицо_Key")]
        public Guid? PrimaryContactPersonId { get; set; }

        [CreatioIgnore]
        [JsonProperty("НомерСвидетельства")]
        public String CertificateNumber { get; set; }

        [CreatioIgnore]
        [JsonProperty("ГруппаДоступаКонтрагента_Key")]
        public Guid? ContractorAccessGroupId { get; set; }

        [CreatioIgnore]
        [JsonProperty("ДополнятьНаименованиеАдресДаннымиГоловногоКонтрагентаВНН")]
        public Boolean? ShouldSupplementNameAddressDataHeadContractorVNN { get; set; }

        [CreatioIgnore]
        [JsonProperty("КодФилиала")]
        public String CodeOfBranch { get; set; }

        [CreatioIgnore]
        [JsonProperty("ИспользоватьЭДО1СЗвит")]
        public Boolean? ShouldUseEDO1Report { get; set; }

        [CreatioIgnore]
        [JsonProperty("ВидыДеятельности")]
        public List<Object> Activities { get; set; }

        [CreatioIgnore]
        [JsonProperty("МенеджерыПокупателя")]
        public List<Object> BuyersManagers { get; set; }

        [CreatioIgnore]
        [JsonProperty("Parent")]
        public String ParentNavigationLinkUrl { get; set; }

        [CreatioIgnore]
        [JsonProperty("ГоловнойКонтрагентnavigationLinkUrl")]
        public String HeadContractorNavigationLinkUrl { get; set; }
    }
}