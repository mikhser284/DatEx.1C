namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;

    /// <summary> Базовый объект с заметками </summary>
    [CreatioType("Базовый объект с заметками")]
    public class BaseEntityNotes : BaseEntity
    {
        /// <summary> Заметки </summary>
        public String Notes { get; set; }
    }
}
