using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DatEx.OneC.DataModel.Auxilary;
using Newtonsoft.Json;

namespace DatEx.OneC.DataModel
{
    /// <summary> Контрагенты </summary>
    [OneS("Catalog_Контрагенты", "Catalog_Контрагенты", "Справочник.Контрагенты", "Справочник.Контрагенты")]
    [JsonObject("Catalog_Контрагенты")]
    public class Contractor : OneSBaseHierarchicalLookup
    {
        /// <summary> Полное наименование </summary>
        [OneS("String", "НаименованиеПолное", "Строка", "НаименованиеПолное", Color = ConsoleColor.Yellow)]
        [JsonProperty("НаименованиеПолное")]
        public String FullName { get; set; }



        /// <summary> Комментарий </summary>
        [OneS("String", "Комментарий", "Строка", "Комментарий")]
        [JsonProperty("Комментарий")]
        public String Comment { get; set; }



        /// <summary> Дополнительное описание</summary>
        [OneS("String", "ДополнительноеОписание", "Строка", "ДополнительноеОписание")]
        [JsonProperty("ДополнительноеОписание")]
        public String AuxilaryDescription { get; set; }



        /// <summary> Головной контрагент (Id) </summary>
        [OneS("Guid?", "ГоловнойКонтрагент_Key", "Справочник.Контрагенты", "ГоловнойКонтрагент")]
        [JsonProperty("ГоловнойКонтрагент_Key")]
        public Guid? PrimaryContractorId { get; set; }



        /// <summary> Источник информации при обращении </summary>
        [OneS("Guid?", "ИсточникИнформацииПриОбращении_Key", "Справочник.ИсточникиИнформацииПриОбращенииПокупателей", "ИсточникИнформацииПриОбращении")]
        [JsonProperty("ИсточникИнформацииПриОбращении_Key")]
        public Guid? SourceOfInformationWhenContactingId { get; set; }



        /// <summary> ИНН </summary>
        [OneS("String", "ИНН", "(Регл) Идентификационный номер налогоплательщика", "ИНН", Color = ConsoleColor.Yellow)]
        [JsonProperty("ИНН")]
        public String INN { get; set; }



        /// <summary> Юр. / физ. лицо </summary>
        [OneS("String", "ЮрФизЛицо", "Перечисление.ЮрФизЛицо", "ЮрФизЛицо", Color = ConsoleColor.Yellow)]
        [JsonProperty("ЮрФизЛицо")]
        public String LegalOrIndividual { get; set; }



        /// <summary> Основной банковский счет </summary>
        [OneS("Guid?", "ОсновнойБанковскийСчет_Key", "Справочник.БанковскиеСчета", "ОсновнойБанковскийСчет")]
        [JsonProperty("ОсновнойБанковскийСчет_Key")]
        public Guid? PrimaryBankAccountId { get; set; }



        /// <summary> Основной договор контрагента </summary>
        [OneS("Guid?", "ОсновнойДоговорКонтрагента_Key", "", "ОсновнойДоговорКонтрагента")]
        [JsonProperty("ОсновнойДоговорКонтрагента_Key")]
        public Guid? PrimaryConctactOfContractorId { get; set; }



        /// <summary> Основной вид деятельности </summary>
        [OneS("Guid?", "ОсновнойВидДеятельности_Key", "Справочник.ВидыДеятельностиКонтрагентов", "ОсновнойВидДеятельности")]
        [JsonProperty("ОсновнойВидДеятельности_Key")]
        public Guid? PrimaryActivityOfContractorId { get; set; }



        /// <summary> Документ, удостоверяющий личность </summary>
        [OneS("String", "ДокументУдостоверяющийЛичность", "Строка", "ДокументУдостоверяющийЛичность")]
        [JsonProperty("ДокументУдостоверяющийЛичность")]
        public String IdentityDocument { get; set; }



        /// <summary> Основной менеджер покупателя </summary>
        [OneS("Guid?", "ОсновнойМенеджерПокупателя_Key", "Справочник.Пользователи", "ОсновнойМенеджерПокупателя")]
        [JsonProperty("ОсновнойМенеджерПокупателя_Key")]
        public Guid? MainBuyerManagerId { get; set; }



        /// <summary> Покупатель </summary>
        [OneS("Boolean?", "Покупатель", "Булево", "Покупатель", Color = ConsoleColor.Yellow)]
        [JsonProperty("Покупатель")]
        public Boolean? IsBuyer { get; set; }



        /// <summary> Поставщик </summary>
        [OneS("Boolean?", "Поставщик", "Булево", "Поставщик", Color = ConsoleColor.Yellow)]
        [JsonProperty("Поставщик")]
        public Boolean? IsProvider { get; set; }



        /// <summary> Расписание работы строкой </summary>
        [OneS("String", "РасписаниеРаботыСтрокой", "Строка", "РасписаниеРаботыСтрокой")]
        [JsonProperty("РасписаниеРаботыСтрокой")]
        public String WorkSheduleString { get; set; }



        /// <summary> Срок выполнения заказа поставщиком (в днях) </summary>
        [OneS("Int16?", "СрокВыполненияЗаказаПоставщиком", "Число", "СрокВыполненияЗаказаПоставщиком")]
        [JsonProperty("СрокВыполненияЗаказаПоставщиком")]
        public Int16? TimeOfOrderExecutionBySupplier { get; set; }



        /// <summary> Основное контактное лицо </summary>
        [OneS("Guid?", "ОсновноеКонтактноеЛицо_Key", "Справочник.КонтактныеЛицаКонтрагентов", "ОсновноеКонтактноеЛицо")]
        [JsonProperty("ОсновноеКонтактноеЛицо_Key")]
        public Guid? PrimaryContactPersonId { get; set; }



        /// <summary>  </summary>
        [OneS("String", "КодПоЕДРПОУ", "Строка", "КодПоЕДРПОУ", Color = ConsoleColor.Yellow)]
        [JsonProperty("КодПоЕДРПОУ")]
        public String CodeOfEdrpo { get; set; }



        /// <summary> Номер свидетельства плательщика НДС </summary>
        [OneS("String", "НомерСвидетельства", "Строка", "НомерСвидетельства")]
        [JsonProperty("НомерСвидетельства")]
        public String CertificateNumber { get; set; }



        /// <summary> Не является резидентом </summary>
        [OneS("Boolean?", "Булево", "Булево", "НеЯвляетсяРезидентом", Color = ConsoleColor.Yellow)]
        [JsonProperty("НеЯвляетсяРезидентом")]
        public Boolean? IsNotResident { get; set; }



        /// <summary> Группа доступа контрагента </summary>
        [OneS("Guid?", "ГруппаДоступаКонтрагента_Key", "Справочник.ГруппыДоступаКонтрагентов", "ГруппаДоступаКонтрагента")]
        [JsonProperty("ГруппаДоступаКонтрагента_Key")]
        public Guid? ContractorAccessGroupId { get; set; }



        /// <summary> В налоговых накладных дополнять наименование и адрес данными головного контрагента </summary>
        [OneS("Boolean?", "ДополнятьНаименованиеАдресДаннымиГоловногоКонтрагентаВНН", "Булево", "ДополнятьНаименованиеАдресДаннымиГоловногоКонтрагентаВНН")]
        [JsonProperty("ДополнятьНаименованиеАдресДаннымиГоловногоКонтрагентаВНН")]
        public Boolean? ShouldSupplementNameAddressDataHeadContractorVNN { get; set; }



        /// <summary> Код филиала (для налоговых накладных) </summary>
        [OneS("String", "КодФилиала", "Строка", "КодФилиала")]
        [JsonProperty("КодФилиала")]
        public String CodeOfBranch { get; set; }



        /// <summary> Использовать 1С:ЭДО </summary>
        [OneS("Boolean?", "ИспользоватьЭДО1СЗвит", "Булево", "ИспользоватьЭДО1СЗвит")]
        [JsonProperty("ИспользоватьЭДО1СЗвит")]
        public Boolean? ShouldUseEDO1Report { get; set; }



        /// <summary> Id creatio </summary>
        [OneS("String", "IdCreatio", "Строка", "IdCreatio")]
        [JsonProperty("IdCreatio")]
        public String IdCreatio { get; set; }




        [JsonProperty("ВидыДеятельности")]
        public List<Object> Activities { get; set; }


        [JsonProperty("МенеджерыПокупателя")]
        public List<Object> BuyersManagers { get; set; }

        public override String ToString() => $"{FullName} {CodeOfEdrpo}";
    }
}