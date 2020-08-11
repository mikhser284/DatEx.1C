namespace DatEx.Creatio.DataModel.Terrasoft.Base
{
    using DatEx.Creatio.DataModel.Auxilary;
    using Newtonsoft.Json;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary> Базовый объект </summary>
    [CreatioType("Базовый объект")]
    public class BaseEntity
    {
        private Guid? _id;
        /// <summary> Id </summary>
        [CreatioProp("Id")]
        public Guid? Id { get => _id; set => _id = value.AsNullable(); }

        /// <summary> Дата создания </summary>
        [CreatioProp("Дата создания")]
        [JsonIgnoreSerialization]
        public DateTime? CreatedOn { get; set; }

        /// <summary> Создал </summary>
        [CreatioProp("Создал")]
        [JsonIgnore]
        public Contact CreatedBy { get; set; }

        private Guid? _createdById;
        /// <summary> Создал (Id) </summary>
        [CreatioProp("Создал (Id)")]
        [JsonIgnoreSerialization]
        public Guid? CreatedById { get => _createdById; set => _createdById = value.AsNullable(); }

        /// <summary> Дата изменения </summary>
        [CreatioProp("Дата изменения")]
        [JsonIgnoreSerialization]
        public DateTime? ModifiedOn { get; set; }

        [CreatioProp("Изменил")]
        [JsonIgnore]
        /// <summary> Изменил </summary>
        public Contact ModifiedBy { get; set; }

        private Guid? _modifiedById;
        /// <summary> Изменил (Id) </summary>
        [CreatioProp("Изменил (Id)")]
        [JsonIgnoreSerialization]
        public Guid? ModifiedById { get => _modifiedById; set => _modifiedById = value.AsNullable(); }

        /// <summary> Активные процессы </summary>
        [CreatioProp("Активные процессы")]
        [JsonIgnoreSerialization]
        public Int32 ProcesListeners { get; set; }



        public void Show()
        {
            var properties = this.GetType().GetProperties();

            Int32 maxPropNameLen = properties.Max(x => x.Name.Length);
            Int32 maxPropTitleLen = properties.Select(p => ((CreatioPropAttribute)p.GetCustomAttributes(typeof(CreatioPropAttribute), false)
                .FirstOrDefault())?.Title.Length).Max(x => x) ?? 0;

            ShowPropertiesBlock(this, properties, maxPropNameLen, maxPropTitleLen);
            
            static void ShowPropertiesBlock<T>(T obj, IEnumerable<PropertyInfo> propsInfo, Int32 maxPropNameLen, Int32 maxPropTitleLen)
            {
                foreach (var p in propsInfo)
                {
                    var attribute = (CreatioPropAttribute)p.GetCustomAttributes(typeof(CreatioPropAttribute), false).FirstOrDefault();
                    String propValue = p.GetValue(obj)?.ToString();
                    if (p.GetValue(obj) == null) propValue = "---";
                    else if (p.PropertyType != typeof(String) && typeof(ICollection).IsAssignableFrom(p.PropertyType))
                    {
                        var val = (ICollection)p.GetValue(obj);
                        propValue = $"{val.Count} шт.";
                    }
                    else if (p.PropertyType == typeof(Guid))
                    {
                        Guid val = (Guid)p.GetValue(obj);
                        propValue = Guid.Empty == (Guid)p.GetValue(obj) ? "---" : val.ToString();
                    }                    
                    Console.WriteLine($" {p.Name.PadRight(maxPropNameLen)} │ {(attribute?.Title ?? "---").PadRight(maxPropTitleLen)} │ {propValue}");
                }
            }
        }
    }
}
