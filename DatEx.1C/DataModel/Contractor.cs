using System;
using System.Collections.Generic;
using System.Text;
using DatEx._1C.DataModel.Auxilary;
using Newtonsoft.Json;

namespace DatEx._1C.DataModel
{
    [CreatioTypeMap("Account", "Catalog_Контрагенты")]
    [JsonObject("Catalog_Контрагенты")]
    public class Contractor
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




        [JsonProperty("Predefined")]
        public Boolean? Predefined { get; set; }

        [JsonProperty("PredefinedDataName")]
        public String PredefinedDataName { get; set; }

        [JsonProperty("DataVersion")]
        public String DataVersion { get; set; }


        [JsonProperty("Code")]
        public String Code { get; set; }

        [JsonProperty("Parent_Key")]
        public Guid? ParentId { get; set; }

        [JsonProperty("IsFolder")]
        public Boolean? IsFolder { get; set; }

        [JsonProperty("DeletionMark")]
        public Boolean? DeletionMark { get; set; }


        [JsonProperty("Комментарий")]
        public String Comment { get; set; }

        [JsonProperty("ДополнительноеОписание")]
        public String AuxilaryDescription { get; set; }

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


        public override String ToString()
        {
            return ""
                + $"\n IdCreatio:                                        | IdCreatio                                                | {IdCreatio}"
                + $"\n Id:                                               | Ref_Key                                                  | {Id}"
                + $"\n IsNotResident:                                    | НеЯвляетсяРезидентом                                     | {IsNotResident}"
                + $"\n Description:                                      | Description                                              | {Description}"
                + $"\n FullName:                                         | НаименованиеПолное                                       | {FullName}"
                + $"\n CodeOfEdrpo:                                      | КодПоЕДРПОУ                                              | {CodeOfEdrpo}"
                + $"\n INN:                                              | ИНН                                                      | {INN}"
                + $"\n IsBuyer:                                          | Покупатель                                               | {IsBuyer}"
                + $"\n IsProvider:                                       | Поставщик                                                | {IsProvider}"
                + $"\n LegalOrIndividual:                                | ЮрФизЛицо                                                | {LegalOrIndividual}"
                + $"\n ————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————"

                + $"\n Predefined:                                       | Predefined                                               | {Predefined}"
                + $"\n PredefinedDataName:                               | PredefinedDataName                                       | {PredefinedDataName}"
                + $"\n DataVersion:                                      | DataVersion                                              | {DataVersion}"
                + $"\n Code:                                             | Code                                                     | {Code}"
                + $"\n ParentId:                                         | Parent_Key                                               | {ParentId}"
                + $"\n IsFolder:                                         | IsFolder                                                 | {IsFolder}"
                + $"\n DeletionMark:                                     | DeletionMark                                             | {DeletionMark}"
                + $"\n Comment:                                          | Комментарий                                              | {Comment}"
                + $"\n AuxilaryDescription:                              | ДополнительноеОписание                                   | {AuxilaryDescription}"
                + $"\n PrimaryContractorId:                              | ГоловнойКонтрагент_Key                                   | {PrimaryContractorId}"
                + $"\n SourceOfInformationWhenContactingId:              | ИсточникИнформацииПриОбращении_Key                       | {SourceOfInformationWhenContactingId}"
                + $"\n PrimaryBankAccountId:                             | ОсновнойБанковскийСчет_Key                               | {PrimaryBankAccountId}"
                + $"\n PrimaryConctactOfContractorId:                    | ОсновнойДоговорКонтрагента_Key                           | {PrimaryConctactOfContractorId}"
                + $"\n PrimaryActivityOfContractorId:                    | ОсновнойВидДеятельности_Key                              | {PrimaryActivityOfContractorId}"
                + $"\n IdentityDocument:                                 | ДокументУдостоверяющийЛичность                           | {IdentityDocument}"
                + $"\n MainBuyerManagerId:                               | ОсновнойМенеджерПокупателя_Key                           | {MainBuyerManagerId}"
                + $"\n WorkSheduleString:                                | РасписаниеРаботыСтрокой                                  | {WorkSheduleString}"
                + $"\n TimeOfOrderExecutionBySupplier:                   | СрокВыполненияЗаказаПоставщиком                          | {TimeOfOrderExecutionBySupplier}"
                + $"\n PrimaryContactPersonId:                           | ОсновноеКонтактноеЛицо_Key                               | {PrimaryContactPersonId}"
                + $"\n CertificateNumber:                                | НомерСвидетельства                                       | {CertificateNumber}"
                + $"\n ContractorAccessGroupId:                          | ГруппаДоступаКонтрагента_Key                             | {ContractorAccessGroupId}"
                + $"\n ShouldSupplementNameAddressDataHeadContractorVNN: | ДополнятьНаименованиеАдресДаннымиГоловногоКонтрагентаВНН | {ShouldSupplementNameAddressDataHeadContractorVNN}"
                + $"\n CodeOfBranch:                                     | КодФилиала                                               | {CodeOfBranch}"
                + $"\n ShouldUseEDO1Report:                              | ИспользоватьЭДО1СЗвит                                    | {ShouldUseEDO1Report}"
                + $"\n Activities:                                       | ВидыДеятельности                                         | {Activities.Count} шт."
                + $"\n BuyersManagers:                                   | МенеджерыПокупателя                                      | {BuyersManagers.Count} шт."
                + $"\n ParentNavigationLinkUrl:                          | Parent                                                   | {ParentNavigationLinkUrl}"
                + $"\n HeadContractorNavigationLinkUrl:                  | ГоловнойКонтрагент                                       | {HeadContractorNavigationLinkUrl}"
                ;
        }
    }
}