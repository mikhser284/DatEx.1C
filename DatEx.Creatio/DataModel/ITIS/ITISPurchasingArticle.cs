namespace DatEx.Creatio.DataModel.ITIS
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;
    using Terrasoft = DatEx.Creatio.DataModel.Terrasoft.Base;


    /// <summary> Статья закупоки </summary>
    [CreatioType("Статья закупок")]
    public class ITISPurchasingArticle : Terrasoft.BaseLookup
    {
        /// <summary> Id объекта в 1C </summary>
        [CreatioPropNotExistInDataModelOfITIS]
        [Map(true, DataType.Lookup, "Catalog_ПодразделенияОрганизаций", DataType.Guid, "Ref_Key")]
        [CreatioProp("Guid", "Id объекта в 1C", Color = ConsoleColor.Red)]
        public Guid ITISOneSId { get; set; }
    }
}
