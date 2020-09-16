namespace DatEx.OneS.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using DatEx.OneS.DataModel.Auxilary;
    using Newtonsoft.Json;

    /// <summary> Единицы измерения </summary>
    [OneS("Catalog_ЕдиницыИзмерения", "Catalog_ЕдиницыИзмерения", "Справочник.ЕдиницыИзмерения", "Справочник.ЕдиницыИзмерения")]
    [JsonObject("Catalog_ЕдиницыИзмерения")]
    public class MeasureUnit : OneSBaseLookup
    {
        /// <summary> Владелец </summary>
        [OneS("String", "Owner", "?", "?")]
        [JsonProperty("Owner")]
        public String Owner { get; set; }



        /// <summary> Единица по классификатору (Id) </summary>
        [OneS("Guid?", "ЕдиницаПоКлассификатору_Key", "Справочник.КлассификаторЕдиницИзмерения", "ЕдиницаПоКлассификатору")]
        [JsonProperty("ЕдиницаПоКлассификатору_Key")]
        public Guid? MeasureUnitByClassifierId { get; set; }



        /// <summary> Единица по классификатору </summary>
        [OneS("Guid?", "ЕдиницаПоКлассификатору_Key", "Справочник.КлассификаторЕдиницИзмерения", "ЕдиницаПоКлассификатору")]
        [JsonProperty("ЕдиницаПоКлассификатору_Key")]
        public Guid? MeasureUnitByClassifier { get; set; }



        /// <summary> Вес </summary>
        [OneS("Double?", "Вес", "Число", "Вес")]
        [JsonProperty("Вес")]
        public Double? Weight { get; set; }



        /// <summary> Объем </summary>
        [OneS("Double?", "Объем", "Число", "Объем")]
        [JsonProperty("Объем")]
        public Double? Volume { get; set; }



        /// <summary> Коэффициент </summary>
        [OneS("Double?", "Коэффициент", "Число", "Коэффициент")]
        [JsonProperty("Коэффициент")]
        public Double? Coefficient { get; set; }



        /// <summary> Порог округления </summary>
        [OneS("Int64?", "ПорогОкругления", "Число", "ПорогОкругления")]
        [JsonProperty("ПорогОкругления")]
        public Int64? RoundingTheshold { get; set; }



        /// <summary> При округлении предупреждать о нецелых местах </summary>
        [OneS("Int64?", "ПредупреждатьОНецелыхМестах", "Булево", "ПредупреждатьОНецелыхМестах")]
        [JsonProperty("ПредупреждатьОНецелыхМестах")]
        public Int64? WarnAboutNonitegerPlacesAtRounding { get; set; }



        /// <summary> Тип владельца </summary>
        [OneS("String", "Owner_Type", "?", "?")]
        [JsonProperty("Owner_Type")]
        public String OwnerType { get; set; }
    }
}
