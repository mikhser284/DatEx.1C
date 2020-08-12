namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;

    /// <summary> Единицы измерения </summary>
    [CreatioType("Единицы измерения")]
    public class Unit : BaseLookup
    {
        /// <summary> краткое название </summary>
        public String ShortName { get; set; }
    }
}
