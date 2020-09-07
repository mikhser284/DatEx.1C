namespace DatEx.OneC.DataModel
{
    using DatEx.OneC.DataModel.Auxilary;
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



        ///// <summary> Базовая единица измерения (Id) </summary>
        //[OneS("Guid?", "БазоваяЕдиницаИзмерения_Key", "Справочник.КлассификаторЕдиницИзмерения", "БазоваяЕдиницаИзмерения")]
        //[JsonProperty("БазоваяЕдиницаИзмерения_Key")]
        //public Guid? MeasureUnitForRemainsStoragingId { get; set; }
    }
}
