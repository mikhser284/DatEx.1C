namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;

    /// <summary> Базовый справочник с кодом </summary>
    [CreatioType("Базовый справочник с кодом")]
    public class BaseCodeLookup : BaseLookup
    {
        /// <summary> Код </summary>
        public String Code { get; set; }
    }
}
