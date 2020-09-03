namespace DatEx.Creatio.DataModel.ITIS
{
    using DatEx.Creatio.DataModel.Auxilary;
    using Newtonsoft.Json;
    using System;
    using Terrasoft = DatEx.Creatio.DataModel.Terrasoft.Base;

    /// <summary> Департамент </summary>
    [CreatioType("Департамент")]
    public class Department : Terrasoft.Department
    {

        /// <summary> Id объекта в 1C </summary>
        [CreatioPropNotExistInDataModelOfITIS]
        [JsonConverter(typeof(JsonConverter_Guid))]
        [Map(true, DataType.Lookup, "Catalog_ПодразделенияОрганизаций", DataType.Guid, "Ref_Key")]
        [CreatioProp("Guid", "Id объекта в 1C", Color = ConsoleColor.Red)]
        public Guid ITISOneSId { get; set; }
    }
}
