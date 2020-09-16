namespace DatEx.OneS.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using DatEx.OneS.DataModel.Auxilary;
    using Newtonsoft.Json;

    /// <summary> Номенклатурная группа </summary>
    [OneS("Catalog_НоменклатурныеГруппы", "Catalog_НоменклатурныеГруппы", "Справочник.НоменклатурныеГруппы", "Справочник.НоменклатурныеГруппы")]
    [JsonObject("Catalog_НоменклатурныеГруппы")]
    public class NomenclatureGroup : OneSBaseHierarchicalLookup
    {
        /// <summary> Единица хранения остатков </summary>
        [OneS("Guid?", "ЕдиницаХраненияОстатков_Key", "Справочник.ЕдиницыИзмерения", "ЕдиницаХраненияОстатков")]
        [JsonProperty("ЕдиницаХраненияОстатков_Key")]
        public Guid? RemainsMeasureUnitId { get; set; }



        /// <summary> Базовая единица измерения </summary>
        [OneS("Guid?", "БазоваяЕдиницаИзмерения_Key", "Справочник.КлассификаторЕдиницИзмерения", "БазоваяЕдиницаИзмерения")]
        [JsonProperty("БазоваяЕдиницаИзмерения_Key")]
        public Guid? BaseMeasureUnitId { get; set; }



        /// <summary> Ставка НДС </summary>
        [OneS("String", "СтавкаНДС", "Перечисление.СтавкиНДС", "СтавкаНДС")]
        [JsonProperty("СтавкаНДС")]
        public String VATRate { get; set; }



        /// <summary> Налоговое назначение </summary>
        [OneS("Guid?", "НалоговоеНазначение_Key", "Справочник.НалоговыеНазначенияАктивовИЗатрат", "НалоговоеНазначение")]
        [JsonProperty("НалоговоеНазначение_Key")]
        public Guid? TaxAppointment { get; set; }



        /// <summary> Показатель для декларации по СХН </summary>
        [OneS("String", "ПоказательДляДекларацииПоСХН", "ПеречислениеСсылка.ИНАГРО_ПоказателиДляДекларацииПоСХН", "ПоказательДляДекларацииПоСХН")]
        [JsonProperty("ПоказательДляДекларацииПоСХН")]
        public String IndicatorForSHNDeclaration { get; set; }
        //TODO NomenclatureGroup
    }
}
