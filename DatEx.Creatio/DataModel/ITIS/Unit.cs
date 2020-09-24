namespace DatEx.Creatio.DataModel.ITIS
{
    using DatEx.Creatio.DataModel.Auxilary;
    using System;
    using System.Text.Json.Serialization;
    using Terrasoft = DatEx.Creatio.DataModel.Terrasoft.Base;

    /// <summary> Единицы измерения </summary>
    [CreatioType("Единицы измерения")]
    public class Unit : Terrasoft.Unit
    {
        /// <summary> Название </summary>
        [JsonIgnore]
        [MapRemarks("Этого свойства на самом деле не существует, оно создано только с той целью что бы продемонстрировать маппинг на реальное свойство Название")]
        [Map(true, DataType.Lookup, "Catalog_КлассификаторЕдиницИзмерения", DataType.String, "Description")]
        [CreatioProp("Строка", "Название", Color = ConsoleColor.Red)]
        public String Name_Virtual { get; set; }

        /// <summary> Id объекта в 1C </summary>
        [CreatioPropNotExistInDataModelOfITIS]
        [Map(true, DataType.Lookup, "Catalog_КлассификаторЕдиницИзмерения", DataType.Guid, "Ref_Key")]
        [CreatioProp("Guid", "Id объекта в 1C", Color = ConsoleColor.Red)]
        public Guid ITISOneSId { get; set; }



        /// <summary> Полное название </summary>
        [CreatioPropNotExistInDataModelOfITIS]
        [Map(false, DataType.Lookup, "Catalog_КлассификаторЕдиницИзмерения", DataType.String, "НаименованиеПолное")]
        [CreatioProp("Полное название", Color = ConsoleColor.Yellow)]
        public String ITISFullName { get; set; }



        /// <summary> Международное сокращение </summary>
        [CreatioPropNotExistInDataModelOfITIS]
        [Map(false, DataType.Lookup, "Catalog_КлассификаторЕдиницИзмерения", DataType.String, "МеждународноеСокращение")]
        [CreatioProp("Международное сокращение", Color = ConsoleColor.Yellow)]
        public String ITISInternationalShortName { get; set; }
    }
}
