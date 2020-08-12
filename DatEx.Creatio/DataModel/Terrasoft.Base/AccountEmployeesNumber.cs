namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;

    /// <summary> Количество сотрудников контрагента </summary>
    [CreatioType("Количество сотрудников контрагента")]
    public class AccountEmployeesNumber : BaseLookup
    {
        /// <summary> Позиция </summary>
        public Int32 Position { get; set; }
    }
}
