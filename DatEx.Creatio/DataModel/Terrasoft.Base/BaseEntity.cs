﻿namespace DatEx.Creatio.DataModel.Terrasoft.Base
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
        [CreatioProp("Уникальный идентификатор", "Id", Color = ConsoleColor.Blue)]
        public Guid? Id { get => _id; set => _id = value.AsNullable(); }

        /// <summary> Дата создания </summary>
        [CreatioProp("Дата/Время", "Дата создания")]
        [JsonIgnoreSerialization]
        public DateTime? CreatedOn { get; set; }

        private Guid? _createdById;
        
        /// <summary> Создал (Id) </summary>
        [CreatioProp("Guid", "Создал (Id)")]
        [JsonIgnoreSerialization]
        public Guid? CreatedById { get => _createdById; set => _createdById = value.AsNullable(); }

        /// <summary> Создал </summary>
        [CreatioProp("Справочник", "Создал")]
        [JsonIgnore]
        public Contact CreatedBy { get; set; }

        /// <summary> Дата изменения </summary>
        [CreatioProp("Дата/Время", "Дата изменения")]
        [JsonIgnoreSerialization]
        public DateTime? ModifiedOn { get; set; }

        [CreatioProp("Справочник", "Изменил")]
        [JsonIgnore]
        /// <summary> Изменил </summary>
        public Contact ModifiedBy { get; set; }

        private Guid? _modifiedById;
        /// <summary> Изменил (Id) </summary>
        [CreatioProp("Guid", "Изменил (Id)")]
        [JsonIgnoreSerialization]
        public Guid? ModifiedById { get => _modifiedById; set => _modifiedById = value.AsNullable(); }

        /// <summary> Активные процессы </summary>
        [CreatioProp("Целое", "Активные процессы")]
        [JsonIgnoreSerialization]
        public Int32 ProcesListeners { get; set; }



        public void Show(Int32 indentLevel = 0)
        {
            IEnumerable<PropertyInfo> properties = this.GetType().GetProperties();

            Int32 maxTypeTitleLen = properties.Select(p => GetCreatioTypeName(p)?.Length)?.Max(x => x) ?? 0;
            Int32 maxPropTitleLen = properties.Select(p => ((CreatioPropAttribute)p.GetCustomAttributes(typeof(CreatioPropAttribute), false)
                .FirstOrDefault())?.CreatioTitle?.Length)?.Max(x => x) ?? 0;
            Int32 maxPropTypeLen = properties.Select(p => p.PropertyType.Name.Length).Max();
            Int32 maxPropNameLen = properties.Max(x => x.Name.Length);            
            Int32 totalLen = maxTypeTitleLen + maxPropTitleLen + maxPropTypeLen + maxPropNameLen;

            var typeAtribure = (CreatioTypeAttribute)this.GetType().GetCustomAttributes(typeof(CreatioTypeAttribute), false).FirstOrDefault();
            String indent = new String(' ', indentLevel * 4);
            Console.WriteLine($"\n{indent}{new String('─', totalLen)}");
            Console.WriteLine($"{indent}   {(typeAtribure?.Title ?? "---")} * {(this.GetType().Name)}");
            Console.WriteLine($"{indent}{new String('─', totalLen)}");

            ShowPropertiesBlock(this, properties, maxTypeTitleLen, maxPropTitleLen, maxPropTypeLen, maxPropNameLen, indent);
            
            static void ShowPropertiesBlock<T>(T obj, IEnumerable<PropertyInfo> propsInfo, Int32 maxTypeTitleLen, Int32 maxPropTitleLen, Int32 maxPropTypeLen, Int32 maxPropNameLen, String indent)
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
                    else if (p.PropertyType == typeof(Guid) || p.PropertyType == typeof(Guid?))
                    {
                        Guid val = (Guid)p.GetValue(obj);
                        propValue = default(Guid) == (Guid)p.GetValue(obj) ? "---" : val.ToString();
                    }
                    else if (p.PropertyType == typeof(DateTime) || p.PropertyType == typeof(DateTime?))
                    {
                        DateTime val = (DateTime)p.GetValue(obj);
                        propValue = default(DateTime) == (DateTime)p.GetValue(obj) ? "---" : val.ToString();
                    }

                    Console.ForegroundColor = attribute?.Color ?? ConsoleColor.DarkGray;

                    var propType = GetCreatioTypeName(p);

                    Console.WriteLine($"{indent} {(String.IsNullOrEmpty(propType) ? "---" : propType).PadRight(maxTypeTitleLen)} │ {(attribute?.CreatioTitle ?? "---").PadRight(maxPropTitleLen)} " +
                        $"│ {GetTypeName(p).PadRight(maxPropTypeLen)} │ {p.Name.PadRight(maxPropNameLen)} │ {propValue}");
                }
                Console.ResetColor();
            }

            static String GetCreatioTypeName(PropertyInfo p)
            {
                var propType = p.PropertyType.GetCustomAttribute<CreatioTypeAttribute>(false)?.Title;
                if (!string.IsNullOrEmpty(propType))
                    return $"Справочник<{propType}>";
                else
                {
                    var attribute = (CreatioPropAttribute)p.GetCustomAttributes(typeof(CreatioPropAttribute), false).FirstOrDefault();
                    return attribute?.CreatioType;
                }
            }

            static String GetTypeName(PropertyInfo propInfo)
            {
                String typeName = propInfo.PropertyType.Name;
                return propInfo.PropertyType.Name;
            }
        }
    }
}
