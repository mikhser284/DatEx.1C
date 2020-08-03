namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using System;

    /// <summary> Настройки визирования раздела </summary>
    public class SysModuleVisa : BaseEntity
    {
        /// <summary> Использовать пользоваительский провайдер уведомлений </summary>
        public Boolean UseCustomNotificationProvider { get; set; }

        /// <summary> Идентификтор схемы визы </summary>
        public Guid VisaSchemfUId { get; set; }

        /// <summary> Идентификатор колонки объекта раздела </summary>
        public Guid MasterColumnUId { get; set; }
    }
}
