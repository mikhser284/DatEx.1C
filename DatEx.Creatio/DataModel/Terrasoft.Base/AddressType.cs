namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using System;
    /// <summary> Тип адреса </summary>
    public class AddressType : BaseLookup
    {
        /// <summary> Использовать в контактах </summary>
        public Boolean ForContact { get; set; }

        /// <summary> Использовать в контрагентах </summary>
        public Boolean ForAccount { get; set; }
    }
}
