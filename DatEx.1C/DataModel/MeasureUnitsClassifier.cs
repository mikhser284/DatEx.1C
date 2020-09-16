namespace DatEx.OneS.DataModel
{
    using System;
    using DatEx.OneS.DataModel.Auxilary;
    using Newtonsoft.Json;

    /// <summary> Классификатор единиц измерения </summary>
    [OneS("Catalog_КлассификаторЕдиницИзмерения", "Catalog_КлассификаторЕдиницИзмерения", "Справочник.КлассификаторЕдиницИзмерения", "Справочник.КлассификаторЕдиницИзмерения")]
    [JsonObject("Catalog_КлассификаторЕдиницИзмерения")]
    public class MeasureUnitsClassifier
    {
        /// <summary> Полное наименование </summary>
        [OneS("String", "НаименованиеПолное", "Строка", "НаименованиеПолное")]
        [JsonProperty("НаименованиеПолное")]
        public String FullName { get; set; }



        /// <summary> Международное сокращение </summary>
        [OneS("String", "МеждународноеСокращение", "Строка", "МеждународноеСокращение")]
        [JsonProperty("МеждународноеСокращение")]
        public String InternationalShortName { get; set; }
    }
}
