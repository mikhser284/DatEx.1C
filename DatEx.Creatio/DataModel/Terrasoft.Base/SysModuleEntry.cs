namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using System;

    /// <summary> Объект раздела </summary>
    public class SysModuleEntry : BaseEntity
    {
        /// <summary> Колонка типа </summary>
        public Guid TypeColumnUid { get; set; }

        /// <summary> Уникальный идентификатор объекта </summary>
        public Guid SysEntitySchemaUId { get; set; }
    }
}
