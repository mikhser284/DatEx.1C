namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using Newtonsoft.Json;
    using System;

    /// <summary> Базовый объект </summary>
    public class BaseEntity
    {
        private Guid? _id;
        /// <summary> Id </summary>
        public Guid? Id { get => _id; set => _id = value.AsNullable(); }

        /// <summary> Дата создания </summary>
        [JsonIgnoreSerialization]
        public DateTime? CreatedOn { get; set; }

        /// <summary> Создал </summary>
        [JsonIgnore]
        public Contact CreatedBy { get; set; }

        private Guid? _createdById;
        /// <summary> Создал (Id) </summary>
        [JsonIgnoreSerialization]
        public Guid? CreatedById { get => _createdById; set => _createdById = value.AsNullable(); }

        /// <summary> Дата изменения </summary>
        [JsonIgnoreSerialization]
        public DateTime? ModifiedOn { get; set; }

        [JsonIgnore]
        /// <summary> Изменил </summary>
        public Contact ModifiedBy { get; set; }

        private Guid? _modifiedById;
        /// <summary> Изменил (Id) </summary>
        [JsonIgnoreSerialization]
        public Guid? ModifiedById { get => _modifiedById; set => _modifiedById = value.AsNullable(); }

        /// <summary> Активные процессы </summary>
        [JsonIgnoreSerialization]
        public Int32 ProcesListeners { get; set; }

        //[OnDeserialized]
        //internal void OnDeserialized(StreamingContext context)
        //{
        //    Id = Id.AsNullable();
        //    CreatedById = CreatedById.AsNullable();
        //    ModifiedById = ModifiedById.AsNullable();
        //}
    }
}
