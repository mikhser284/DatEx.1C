namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;

    [CreatioType("Базовий иерархический объект")]
    public class BaseHierarchicalLookup : BaseLookup
    {
        /// <summary> Родитель (Id) </summary>
        public Guid BaseId { get; set; }

        /// <summary> Родитель </summary>
        public BaseHierarchicalLookup Base { get; set; }
    }
}
