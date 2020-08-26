﻿namespace DatEx.Creatio.DataModel.ITIS
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;
    using Terrasoft = DatEx.Creatio.DataModel.Terrasoft.Base;


    /// <summary> Должность </summary>
    [CreatioType("Должность")]
    public class Job : Terrasoft.Job
    {
        /// <summary> Id объекта в 1C </summary>
        [CreatioPropNotExistInDataModelOfITIS]
        [CreatioProp("Guid", "Id объекта в 1C", Color = ConsoleColor.Red)]
        public Guid ITISOneSId { get; set; }
    }
}
