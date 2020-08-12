namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;

    /// <summary> Тип адреса </summary>
    [CreatioType("Тип адреса")]
    public class AddressType : BaseLookup
    {
        /// <summary> Использовать в контактах </summary>
        public Boolean ForContact { get; set; }

        /// <summary> Использовать в контрагентах </summary>
        public Boolean ForAccount { get; set; }
    }
}
