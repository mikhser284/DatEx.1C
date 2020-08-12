namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using System;
    using DatEx.Creatio.DataModel.Auxilary;

    /// <summary> Объект раздела </summary>
    [CreatioType("Объект раздела")]
    public class SysModuleEntry : BaseEntity
    {
        /// <summary> Колонка типа </summary>
        public Guid TypeColumnUid { get; set; }

        /// <summary> Уникальный идентификатор объекта </summary>
        public Guid SysEntitySchemaUId { get; set; }
    }
}
