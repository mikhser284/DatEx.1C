namespace DatEx.OneS.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using DatEx.OneS.DataModel.Auxilary;
    using Newtonsoft.Json;

    /// <summary> Стати затрат </summary>
    [OneS("Catalog_СтатьиЗатрат", "Catalog_СтатьиЗатрат", "Справочник.СтатьиЗатрат", "Справочник.СтатьиЗатрат")]
    [JsonObject("Catalog_СтатьиЗатрат")]
    public class CostArticle : OneSBaseHierarchicalLookup
    {
        /// <summary> Вид затрат </summary>
        [OneS("String", "ВидЗатрат", "Перечисление.ВидыЗатрат", "ВидЗатрат")]
        [JsonProperty("ВидЗатрат")]
        public String FullName { get; set; }



        /// <summary> Статус материальных затрат </summary>
        [OneS("String", "СтатусМатериальныхЗатрат", "Перечисление.СтатусыМатериальныхЗатратНаПроизводство", "СтатусМатериальныхЗатрат")]
        [JsonProperty("СтатусМатериальныхЗатрат")]
        public String StatusOfMaterialCosts { get; set; }



        /// <summary> Характер затрат </summary>
        [OneS("String", "ХарактерЗатрат", "Перечисление.ХарактерЗатрат", "ХарактерЗатрат")]
        [JsonProperty("ХарактерЗатрат")]
        public String CostCharacter { get; set; }



        /// <summary> Постоянная затрата </summary>
        [OneS("Boolean?", "ПостояннаяЗатрата", "Булево", "ПостояннаяЗатрата")]
        [JsonProperty("ПостояннаяЗатрата")]
        public Boolean? IsConstantCost { get; set; }



        /// <summary> Счет 8-го класса </summary>
        [OneS("Guid?", "Счет8Класса_Key", "ПланСчетов.Хозрасчетный", "Счет8Класса")]
        [JsonProperty("Счет8Класса_Key")]
        public Guid? Account8thClassId { get; set; }



        /// <summary> Статья декларации по налогу на прибыль </summary>
        [OneS("Guid?", "СтатьяДекларацииПоНалогуНаПрибыль_Key", "Справочник.СтатьиНалоговыхДеклараций", "СтатьяДекларацииПоНалогуНаПрибыль")]
        [JsonProperty("СтатьяДекларацииПоНалогуНаПрибыль_Key")]
        public Guid? ArticleDeclarationProfitTaxId { get; set; }



        /// <summary> Не добавлять в пару базисов </summary>
        [OneS("Boolean?", "ИНАГРО_НеДобавлятьВПаруБазисов", "Булево", "ИНАГРО_НеДобавлятьВПаруБазисов")]
        [JsonProperty("ИНАГРО_НеДобавлятьВПаруБазисов")]
        public Boolean? Inagro_DontAddToPairOfBases { get; set; }



        /// <summary> Место возникновения </summary>
        [OneS("String", "ИНАГРО_МестоВозникновения", "Перечисление.ИНАГРО_МестаВозниконовенияЗатрат", "ИНАГРО_МестоВозникновения")]
        [JsonProperty("ИНАГРО_МестоВозникновения")]
        public String Inagro_PlaceOfccurrence { get; set; }



        /// <summary> Операционная </summary>
        [OneS("Boolean?", "ИНАГРО_Операционная", "Булево", "ИНАГРО_Операционная")]
        [JsonProperty("ИНАГРО_Операционная")]
        public Boolean? Inagro_IsOperational { get; set; }
    }
}
