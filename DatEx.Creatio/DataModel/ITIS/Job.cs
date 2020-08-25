namespace DatEx.Creatio.DataModel.ITIS
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;
    using Terrasoft = DatEx.Creatio.DataModel.Terrasoft.Base;

    [CreatioType("Должность")]

    /// <summary> Должность </summary>
    public class Job : Terrasoft.Job
    {
        /// <summary> Id объекта в 1C </summary>
        [CreatioProp("Guid", "Id объекта в 1C")]
        public Guid ITISOneSId { get; set; }
    }
}
