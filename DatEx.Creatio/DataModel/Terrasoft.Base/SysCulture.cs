namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;

    /// <summary> Культура </summary>
    [CreatioType("Культура")]
    public class SysCulture : BaseLookup
    {
        /// <summary> Использовать по умолчанию </summary>
        public Boolean Default { get; set; }        
    }
}
