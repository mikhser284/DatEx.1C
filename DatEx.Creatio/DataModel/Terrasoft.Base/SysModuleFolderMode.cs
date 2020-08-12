namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;

    /// <summary> Режим групп раздела </summary>
    [CreatioType("Режим групп раздела")]
    public class SysModuleFolderMode : BaseEntity
    {
        /// <summary> Название </summary>
        public String Name { get; set; }

        /// <summary> Code </summary>
        public String Code { get; set; }
    }
}
