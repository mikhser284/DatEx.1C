namespace DatEx.OneS.DataModel
{
    using DatEx.OneS.DataModel.Auxilary;
    using Newtonsoft.Json;
    using System;


    /// <summary> Номенклатура </summary>
    [OneS("Catalog_Номенклатура", "Catalog_Номенклатура", "Номенклатура", "Номенклатура")]
    [JsonObject("Catalog_Номенклатура")]
    public class Nomenclature : OneSBaseHierarchicalLookup
    {
        /// <summary> Артикул </summary>
        [OneS("String", "Артикул", "Строка", "Артикул")]
        [JsonProperty("Артикул")]
        public Guid VendorCode { get; set; }



        /// <summary> Полное наименование </summary>
        [OneS("String", "НаименованиеПолное", "Строка", "НаименованиеПолное")]
        [JsonProperty("НаименованиеПолное")]
        public String FullName { get; set; }



        /// <summary> Весовой </summary>
        [OneS("Boolean?", "Весовой", "Строка", "Весовой")]
        [JsonProperty("Весовой")]
        public Boolean? IsWeighting { get; set; }



        /// <summary> Весовой коэффициент вхождения </summary>
        [OneS("Int64?", "ВесовойКоэффициентВхождения", "Строка", "ВесовойКоэффициентВхождения")]
        [JsonProperty("ВесовойКоэффициентВхождения")]
        public Int64? WeightingFactor { get; set; }



        /// <summary> Вести оперативный учет остатков НЗП </summary>
        [OneS("Boolean?", "ВестиОперативныйУчетОстатковНЗП", "Булево", "ВестиОперативныйУчетОстатковНЗП")]
        [JsonProperty("ВестиОперативныйУчетОстатковНЗП")]
        public Boolean? MaintainOperationalRecordsOfWIPBalances { get; set; }



        /// <summary> Вести партионный учет по сериям </summary>
        [OneS("Boolean?", "ВестиПартионныйУчетПоСериям", "Булево", "ВестиПартионныйУчетПоСериям")]
        [JsonProperty("ВестиПартионныйУчетПоСериям")]
        public Boolean? MaintainPartyAccountingBySeries { get; set; }



        /// <summary> Вести учет по сериям </summary>
        [OneS("Boolean?", "ВестиУчетПоСериям", "Булево", "ВестиУчетПоСериям")]
        [JsonProperty("ВестиУчетПоСериям")]
        public Boolean? MaintainAccountBySeries { get; set; }



        /// <summary> Вести учет по сериям в НЗП </summary>
        [OneS("Boolean?", "ВестиУчетПоСериямВНЗП", "Булево", "ВестиУчетПоСериямВНЗП")]
        [JsonProperty("ВестиУчетПоСериямВНЗП")]
        public Boolean? MaintainAccountByWNZPSeries { get; set; }



        /// <summary> Вести учет по характеристикам </summary>
        [OneS("Boolean?", "ВестиУчетПоХарактеристикам", "Булево", "ВестиУчетПоХарактеристикам")]
        [JsonProperty("ВестиУчетПоХарактеристикам")]
        public Boolean? MaintainAccountByCharacteristics { get; set; }



        /// <summary> Вид воспроизводства </summary>
        [OneS("String", "ВидВоспроизводства", "Перечисление.ВидыВоспроизводстваНоменклатуры", "ВидВоспроизводства")]
        [JsonProperty("ВидВоспроизводства")]
        public String ReproductionType { get; set; }



        /// <summary> Вид номенклатуры (Id) </summary>
        [OneS("Guid?", "ВидНоменклатуры_Key", "Справочник.ВидыНоменклатуры", "ВидНоменклатуры")]
        [JsonProperty("ВидНоменклатуры_Key")]
        public Guid? NomenclatureTypeId { get; set; }



        /// <summary> Единица для отчетов (Id) </summary>
        [OneS("Guid?", "ЕдиницаДляОтчетов_Key", "Справочник.ЕдиницыИзмерения", "ЕдиницаДляОтчетов")]
        [JsonProperty("ЕдиницаДляОтчетов_Key")]
        public Guid? MeasureUnitForReportsId { get; set; }



        /// <summary> Единица хранения остатков (Id) </summary>
        [OneS("Guid?", "ЕдиницаХраненияОстатков_Key", "Справочник.ЕдиницыИзмерения", "ЕдиницаХраненияОстатков")]
        [JsonProperty("ЕдиницаХраненияОстатков_Key")]
        public Guid? MeasureUnitForRemainsStoragingId { get; set; }



        /// <summary> Набор </summary>
        [OneS("Boolean?", "Набор", "Булево", "Набор")]
        [JsonProperty("Набор")]
        public Boolean? IsSet { get; set; }



        /// <summary> (Общ) Ставка НДС по умолчанию для оформления документов </summary>
        [OneS("String", "СтавкаНДС", "Перечисление.СтавкиНДС", "СтавкаНДС")]
        [JsonProperty("СтавкаНДС")]
        public String VATRate { get; set; }



        /// <summary> Комментарий </summary>
        [OneS("String", "Комментарий", "Строка", "Комментарий")]
        [JsonProperty("СтавкаНДС")]
        public String Comments { get; set; }



        /// <summary> Номенклатурная группа затрат (Id) </summary>
        [OneS("Guid?", "НоменклатурнаяГруппаЗатрат_Key", "Справочник.НоменклатурныеГруппы", "НоменклатурнаяГруппаЗатрат")]
        [JsonProperty("НоменклатурнаяГруппаЗатрат_Key")]
        public Guid? NomenclatureCostGroupId { get; set; }



        /// <summary> Услуга </summary>
        [OneS("Boolean?", "Услуга", "Булево", "Услуга")]
        [JsonProperty("Услуга")]
        public Boolean? IsService { get; set; }



        /// <summary> Основное изображение (Id) </summary>
        [OneS("Guid?", "ОсновноеИзображение_Key", "Справочник.ХранилищеДополнительнойИнформации", "ОсновноеИзображение")]
        [JsonProperty("ОсновноеИзображение_Key")]
        public Guid? PrimaryImapeId { get; set; }



        /// <summary> Основной поставщик (Id) </summary>
        [OneS("Guid?", "ОсновнойПоставщик_Key", "Справочник.Контрагенты", "ОсновноеИзображение")]
        [JsonProperty("ОсновнойПоставщик_Key")]
        public Guid? PrimarySupplierId { get; set; }



        /// <summary> Ответственный менеджер за покупки (Id) </summary>
        [OneS("Guid?", "ОтветственныйМенеджерЗаПокупки_Key", "Справочник.Пользователи", "ОтветственныйМенеджерЗаПокупки")]
        [JsonProperty("ОтветственныйМенеджерЗаПокупки_Key")]
        public Guid? ManagerResponsibleForPurchasingId { get; set; }



        /// <summary> Номенклатурная группа (Id) </summary>
        [OneS("Guid?", "НоменклатурнаяГруппа_Key", "Справочник.НоменклатурныеГруппы", "НоменклатурнаяГруппа")]
        [JsonProperty("НоменклатурнаяГруппа_Key")]
        public Guid? NomenclatureGroupId { get; set; }



        /// <summary> Требуется внешняя сертификация </summary>
        [OneS("Boolean?", "ТребуетсяВнешняяСертификация", "Булево", "ТребуетсяВнешняяСертификация")]
        [JsonProperty("ТребуетсяВнешняяСертификация")]
        public Boolean? IsExternalCertificationRequired { get; set; }



        /// <summary> Требуется внутренняя сертификация </summary>
        [OneS("Boolean?", "ТребуетсяВнутренняяСертификация", "Булево", "ТребуетсяВнутренняяСертификация")]
        [JsonProperty("ТребуетсяВнутренняяСертификация")]
        public Boolean? IsInternalCertificationRequired { get; set; }



        /// <summary> Статья затрат (Id) </summary>
        [OneS("Guid?", "СтатьяЗатрат_Key", "Справочник.СтатьиЗатрат", "СтатьяЗатрат")]
        [JsonProperty("СтатьяЗатрат_Key")]
        public Guid? CostItemId { get; set; }



        /// <summary> Транспортная услуга </summary>
        [OneS("Boolean?", "ТранспортнаяУслуга", "Булево", "ТранспортнаяУслуга")]
        [JsonProperty("ТранспортнаяУслуга")]
        public Boolean? IsTransportService { get; set; }



        /// <summary> Измеряется только в суммовом выражении </summary>
        [OneS("Boolean?", "ИзмеряетсяТолькоВСуммовомВыражении", "Булево", "ИзмеряетсяТолькоВСуммовомВыражении")]
        [JsonProperty("ИзмеряетсяТолькоВСуммовомВыражении")]
        public Boolean? MeasuredInSumOnly { get; set; }



        /// <summary> Текст для печати в колонке количество налоговой накладной </summary>
        [OneS("String", "ТекстДляПечатиВКолонкеКоличествоНалоговойНакладной", "Строка", "ТекстДляПечатиВКолонкеКоличествоНалоговойНакладной")]
        [JsonProperty("ТекстДляПечатиВКолонкеКоличествоНалоговойНакладной")]
        public String TextForPrintInColumnNumberOfTaxInvoice { get; set; }



        /// <summary> Льгота по НДС </summary>
        [OneS("String", "ЛьготаНДС", "Строка", "ЛьготаНДС")]
        [JsonProperty("ЛьготаНДС")]
        public String VATBenefit { get; set; }



        /// <summary> Вести серийные номера </summary>
        [OneS("Boolean?", "ВестиСерийныеНомера", "Булево", "ВестиСерийныеНомера")]
        [JsonProperty("ВестиСерийныеНомера")]
        public Boolean? LeadSerialNumbers { get; set; }



        /// <summary> Комплект </summary>
        [OneS("Boolean?", "Комплект", "Булево", "Комплект")]
        [JsonProperty("Комплект")]
        public Boolean? IsComplect { get; set; }



        /// <summary> Направление выпуска </summary>
        [OneS("String", "НаправлениеВыпуска", "ПеречислениеСсылка.НаправленияВыпуска", "НаправлениеВыпуска")]
        [JsonProperty("НаправлениеВыпуска")]
        public String ReleaseDirection { get; set; }



        /// <summary> Направление списания выпущенной продукции (Id) </summary>
        [OneS("Guid?", "НаправлениеСписанияВыпущеннойПродукции_Key", "Справочник.НаправленияСписанияВыпущеннойПродукции", "НаправлениеСписанияВыпущеннойПродукции")]
        [JsonProperty("НаправлениеСписанияВыпущеннойПродукции_Key")]
        public Guid? DirectionOfWriteOffOfReleasedProdocts { get; set; }



        /// <summary> Порядок присвоения серийного номера (Id) </summary>
        [OneS("Guid?", "ПорядокПрисвоенияСерийногоНомера_Key", "Справочник.ПорядокПрисвоенияСерийныхНомеров", "ПорядокПрисвоенияСерийногоНомера")]
        [JsonProperty("ПорядокПрисвоенияСерийногоНомера_Key")]
        public Guid? SerialNumberAssignmentId { get; set; }



        /// <summary> Ценовая группа (Id) </summary>
        [OneS("Guid?", "ЦеноваяГруппа_Key", "Справочник.ЦеновыеГруппы", "ЦеноваяГруппа")]
        [JsonProperty("ЦеноваяГруппа_Key")]
        public Guid? PriceGroupId { get; set; }



        /// <summary> Единица измерения мест (Id) </summary>
        [OneS("Guid?", "ЕдиницаИзмеренияМест_Key", "Справочник.ЕдиницыИзмерения", "ЕдиницаИзмеренияМест")]
        [JsonProperty("ЕдиницаИзмеренияМест_Key")]
        public Guid? PlacesMeasureUnit { get; set; }



        /// <summary> Дополнительное описание </summary>
        [OneS("String", "ДополнительноеОписаниеНоменклатуры", "Строка", "ДополнительноеОписаниеНоменклатуры")]
        [JsonProperty("ДополнительноеОписаниеНоменклатуры")]
        public String AuxilaryDescription { get; set; }



        /// <summary> Вид животного (Id) </summary>
        [OneS("Guid?", "ИНАГРО_ВидЖивотного_Key", "Справочник.ИНАГРО_ВидыЖивотных", "ИНАГРО_ВидЖивотного")]
        [JsonProperty("ИНАГРО_ВидЖивотного_Key")]
        public Guid? Inagro_TypeOfBiologicalAsset { get; set; }



        /// <summary> Подлежит амортизации </summary>
        [OneS("Boolean?", "ПодлежитАмортизации", "Булево", "ПодлежитАмортизации")]
        [JsonProperty("ПодлежитАмортизации")]
        public Boolean? ToBeCushioned { get; set; }



        /// <summary> Дата рождения </summary>
        [OneS("DateTime?", "ИНАГРО_ДатаРождения", "Дата", "ИНАГРО_ДатаРождения")]
        [JsonProperty("ИНАГРО_ДатаРождения")]
        public DateTime? Inagro_DateOfBirth { get; set; }



        /// <summary> Код для НН (по-умолч.) </summary>
        [OneS("String", "НоменклатураГТД", "Справочник.НоменклатураГТД, Справочник.КлассификаторУКТВЭД", "НоменклатураГТД")]
        [JsonProperty("НоменклатураГТД")]
        public String NomenclatureGTD { get; set; }



        /// <summary> УДАЛИТЬ Код УКТВЭД </summary>
        [OneS("Guid?", "УДАЛИТЬКодУКТВЭД_Key", "Справочник.КлассификаторУКТВЭД", "УДАЛИТЬКодУКТВЭД")]
        [JsonProperty("УДАЛИТЬКодУКТВЭД_Key")]
        public Guid? RemoveCodeByUKTVDId { get; set; }



        /// <summary> Дополнительное описание номенклатуры в формате HTML </summary>
        [OneS("Boolean?", "ДополнительноеОписаниеНоменклатурыВФорматеHTML", "Булево", "ДополнительноеОписаниеНоменклатурыВФорматеHTML")]
        [JsonProperty("ДополнительноеОписаниеНоменклатурыВФорматеHTML")]
        public Boolean? AuxilaryNomenclatureDescriptionInHtmlFormat { get; set; }



        /// <summary> Индивидуальный учет </summary>
        [OneS("Boolean?", "ИНАГРО_ИндивидуальныйУчет", "Булево", "ИНАГРО_ИндивидуальныйУчет")]
        [JsonProperty("ИНАГРО_ИндивидуальныйУчет")]
        public Boolean? Inagro_IndividualAccounting { get; set; }



        /// <summary> Подакцизный товар </summary>
        [OneS("Boolean?", "ПодакцизныйТовар", "Булево", "ПодакцизныйТовар")]
        [JsonProperty("ПодакцизныйТовар")]
        public Boolean? IsExciseGoods { get; set; }



        /// <summary> Статья декларации по акцизному налогу </summary>
        [OneS("Guid?", "СтатьяДекларацииПоАкцизномуНалогу_Key", "Справочник.СтатьиНалоговыхДеклараций", "СтатьяДекларацииПоАкцизномуНалогу")]
        [JsonProperty("СтатьяДекларацииПоАкцизномуНалогу_Key")]
        public Guid? ArticleDeclarationsOnExciseTaxId { get; set; }



        /// <summary> Вид культуры (Id) </summary>
        [OneS("Guid?", "ИНАГРО_ВидКультуры_Key", "Справочник.ИНАГРО_ВидыКультур", "ИНАГРО_ВидКультуры")]
        [JsonProperty("ИНАГРО_ВидКультуры_Key")]
        public Guid? CultureTypeId { get; set; }



        /// <summary> Год урожая </summary>
        [OneS("Int16?", "ИНАГРО_ГодУрожая", "Число", "ИНАГРО_ГодУрожая")]
        [JsonProperty("ИНАГРО_ГодУрожая")]
        public Int16? Inagro_CropYear { get; set; }



        /// <summary> Сертификация </summary>
        [OneS("Boolean?", "ИНАГРО_Сертификация", "Булево", "ИНАГРО_Сертификация")]
        [JsonProperty("ИНАГРО_Сертификация")]
        public Boolean? Inagro_Sertification { get; set; }



        /// <summary> Код льготы по НДС (согласно Справочников налоговых льгот) </summary>
        [OneS("String", "КодЛьготы", "Строка", "КодЛьготы")]
        [JsonProperty("КодЛьготы")]
        public String CodeOfBenefit { get; set; }



        /// <summary> ИНАГРО вид продукции </summary>
        [OneS("String", "ИНАГРО_ВидПродукции", "Перечисление.ИНАГРО_ВидыСХПродукции", "ИНАГРО_ВидПродукции")]
        [JsonProperty("ИНАГРО_ВидПродукции")]
        public String Inagro_ProductionType { get; set; }



        /// <summary> ИНАГРО код для обмена </summary>
        [OneS("String", "ИНАГРО_КодДляОбмена", "Строка", "ИНАГРО_КодДляОбмена")]
        [JsonProperty("ИНАГРО_КодДляОбмена")]
        public String Inagro_CodeForExchange { get; set; }



        /// <summary> Выгружать в АРМ агронома </summary>
        [OneS("Boolean?", "ИНАГРО_ВыгружатьАгроном", "Булево", "ИНАГРО_ВыгружатьАгроном")]
        [JsonProperty("ИНАГРО_ВыгружатьАгроном")]
        public Boolean? Inagro_UnloadForAgronomist { get; set; }



        /// <summary> Выгружать в АРМ кладовщика </summary>
        [OneS("Boolean?", "ИНАГРО_ВыгружатьОперСклад", "Булево", "ИНАГРО_ВыгружатьОперСклад")]
        [JsonProperty("ИНАГРО_ВыгружатьОперСклад")]
        public Boolean? Inagro_UnloadForOperationalWarehouse { get; set; }



        /// <summary> У п группа ТМЦ </summary>
        [OneS("Guid?", "УП_ГруппаТМЦ_Key", "Справочник.УП_ГруппыТМЦ", "УП_ГруппаТМЦ_Key")]
        [JsonProperty("УП_ГруппаТМЦ_Key")]
        public Guid? Inagro_UPTMCGroupId { get; set; }

        /// <summary> У п группа ТМЦ </summary>
        
        [OneS("String", "НоменклатураГТД_Type", "—", "—")]
        [JsonProperty("НоменклатураГТД_Type")]
        public String NomenclatureGTDType { get; set; }
    }
}
