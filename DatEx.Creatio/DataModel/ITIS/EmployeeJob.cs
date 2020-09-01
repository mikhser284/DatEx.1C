namespace DatEx.Creatio.DataModel.ITIS
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;
    using Terrasoft = DatEx.Creatio.DataModel.Terrasoft.Base;

    [CreatioType("Должность сотрудника")]

    /// <summary> Должность сотрудника </summary>
    public class EmployeeJob : Terrasoft.EmployeeJob
    {
        /// <summary> Id объекта в 1C </summary>
        [Map(true, DataType.Lookup, "Catalog_ДолжностиОрганизаций", DataType.Guid, "Ref_Key")]
        [CreatioPropNotExistInDataModelOfITIS]
        [CreatioProp("Guid", "Id объекта в 1C", Color = ConsoleColor.Red)]
        public Guid ITISOneSId { get; set; }
    }
}
