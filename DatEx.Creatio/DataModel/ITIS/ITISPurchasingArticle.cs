namespace DatEx.Creatio.DataModel.ITIS
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;
    using Newtonsoft.Json;
    using Terrasoft = DatEx.Creatio.DataModel.Terrasoft.Base;


    /// <summary> Статья закупоки </summary>
    [CreatioType("Статья закупок")]
    public class ITISPurchasingArticle : Terrasoft.BaseLookup
    {
        /// <summary> Название </summary>
        [JsonIgnore]
        [MapRemarks("Этого свойства на самом деле не существует, оно создано только с той целью что бы продемонстрировать маппинг на реальное свойство Название")]
        [Map(true, DataType.Lookup, "Catalog_КлассификаторЕдиницИзмерения", DataType.String, "Description")]
        [CreatioProp("Строка", "Название", Color = ConsoleColor.Red)]
        public String Name_Virtual { get; set; }



        /// <summary> Id объекта в 1C </summary>
        [CreatioPropNotExistInDataModelOfITIS]
        [Map(true, DataType.Lookup, "Catalog_ПодразделенияОрганизаций", DataType.Guid, "Ref_Key")]
        [CreatioProp("Guid", "Id объекта в 1C", Color = ConsoleColor.Red)]
        public Guid ITISOneSId { get; set; }
    }
}
