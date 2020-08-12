namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;

    /// <summary> Язык </summary>
    [CreatioType("Язык")]
    public class SysLanguage : BaseCodeLookup
    {
        /// <summary> Используется </summary>
        public Boolean IsUsed { get; set; }
    }
}
