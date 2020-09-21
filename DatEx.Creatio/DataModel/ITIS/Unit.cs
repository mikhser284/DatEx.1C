namespace DatEx.Creatio.DataModel.ITIS
{
    using DatEx.Creatio.DataModel.Auxilary;
    using System;
    using Terrasoft = DatEx.Creatio.DataModel.Terrasoft.Base;

    /// <summary> Единицы измерения </summary>
    [CreatioType("Единицы измерения")]
    public class Unit : Terrasoft.Unit
    {
        /// <summary> Id объекта в 1C </summary>
        [CreatioPropNotExistInDataModelOfITIS]
        [Map(true, DataType.Lookup, "Catalog_ПодразделенияОрганизаций", DataType.Guid, "Ref_Key")]
        [CreatioProp("Guid", "Id объекта в 1C", Color = ConsoleColor.Red)]
        public Guid ITISOneSId { get; set; }
    }
}
