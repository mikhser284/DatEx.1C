using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DatEx.OneC.DataModel.Auxilary;
using Newtonsoft.Json;

namespace DatEx.OneC.DataModel
{
    public class OneCBaseLookup : OneCObject
    {
        /// <summary> Id </summary>
        [OneS("Guid", "Ref_Key", "-", "-", Color = ConsoleColor.Magenta)]
        [JsonProperty("Ref_Key")]
        public Guid Id { get; set; }

        [OneS("Boolean?", "Predefined", "Булево", "?")]
        [JsonProperty("Predefined")]
        public Boolean? Predefined { get; set; }

        [OneS("String", "PredefinedDataName", "Строка", "?")]
        [JsonProperty("PredefinedDataName")]
        public String PredefinedDataName { get; set; }


        [OneS("String", "DataVersion", "Строка", "?")]
        [JsonProperty("DataVersion")]
        public String DataVersion { get; set; }

        /// <summary> Наименование </summary>
        [OneS("String", "Description", "Строка", "Наименование", Color = ConsoleColor.Yellow)]
        [JsonProperty("Description")]
        public String Description { get; set; }

        /// <summary> Код </summary>
        [OneS("String", "Code", "Строка", "Код", Color = ConsoleColor.Blue)]
        [JsonProperty("Code")]
        public String Code { get; set; }

        /// <summary> Пометка удаления </summary>
        [OneS("Boolean?", "DeletionMark", "Булево", "Пометка удаления", Color = ConsoleColor.Blue)]
        [JsonProperty("DeletionMark")]
        public Boolean? DeletionMark { get; set; }

        public override string ToString() => Description;
    }

    public class OneCBaseHierarchicalLookup : OneCBaseLookup
    {
        /// <summary> Родитель </summary>
        [OneS("Guid?", "Parent_Key", "?", "Родитель", Color = ConsoleColor.Blue)]
        [JsonProperty("Parent_Key")]
        public Guid? ParentId { get; set; }

        /// <summary> Является папкой </summary>
        [OneS("Boolean?", "IsFolder", "Булево", "Является папкой", Color = ConsoleColor.Blue)]
        [JsonProperty("IsFolder")]
        public Boolean? IsFolder { get; set; }
    }

    public class OneCObject
    {
        public void Show(Int32 indentLevel = 0)
        {
            IEnumerable<PropertyInfo> properties = this.GetType().GetProperties();

            Int32 maxOneCODataTypeLen = properties
                .Select(p => ((OneSAttribute)p.GetCustomAttributes(typeof(OneSAttribute), false)
                .FirstOrDefault())?.ODataType.Length).Max(x => x) ?? 0;
            Int32 maxOneCODataName = properties
                .Select(p => ((OneSAttribute)p.GetCustomAttributes(typeof(OneSAttribute), false)
                .FirstOrDefault())?.ODataName.Length).Max(x => x) ?? 0;
            Int32 maxOneCTypeLen = properties
                .Select(p => ((OneSAttribute)p.GetCustomAttributes(typeof(OneSAttribute), false)
                .FirstOrDefault())?.OneCType.Length).Max(x => x) ?? 0;
            Int32 maxOneCNameLen = properties
                .Select(p => ((OneSAttribute)p.GetCustomAttributes(typeof(OneSAttribute), false)
                .FirstOrDefault())?.OneCName.Length).Max(x => x) ?? 0;            
            Int32 maxPropNameLen = properties.Max(x => x.Name.Length);
            Int32 totalWidth = 16 + maxOneCODataTypeLen + maxOneCODataName + maxOneCTypeLen + maxOneCNameLen + maxPropNameLen;

            var typeAtribure = (OneSAttribute)this.GetType().GetCustomAttributes(typeof(OneSAttribute), false).FirstOrDefault();
            String indent = new String(' ', indentLevel * 4);
            Console.WriteLine($"\n{indent}{new String('─', totalWidth)}");
            Console.WriteLine($"{indent}   {(typeAtribure?.ODataType ?? "---")} * {(typeAtribure?.OneCType ?? "---")}");
            Console.WriteLine($"{indent}{new String('─', totalWidth)}");

            //var oneC = this.GetType().GetProperties();//.Where(p => p.IsDefined(typeof(OneCAttribute)));
            ShowPropertiesBlock(this, properties, maxOneCODataTypeLen, maxOneCODataName, maxOneCTypeLen, maxOneCNameLen, maxPropNameLen, indent);
            
            static void ShowPropertiesBlock<T>(T obj, IEnumerable<PropertyInfo> propsInfo, Int32 maxOneCODataTypeLen, Int32 maxOneCODataName, Int32 maxOneCTypeLen, Int32 maxOneCNameLen, Int32 maxPropNameLen, String indent)
            {
                foreach(var p in propsInfo)
                {
                    var attribute = (OneSAttribute)p.GetCustomAttributes(typeof(OneSAttribute), false).FirstOrDefault();
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
                    Console.WriteLine($"{indent} {(attribute?.ODataType ?? "---").PadRight(maxOneCODataTypeLen)} │ {(attribute?.ODataName ?? "---").PadRight(maxOneCODataName)} │ {(attribute?.OneCType ?? "---").PadRight(maxOneCTypeLen)} │ {(attribute?.OneCName ?? "---").PadRight(maxOneCNameLen)} │ {p.Name.PadRight(maxPropNameLen)} │ {propValue}");
                }
                Console.ResetColor();
            }
        }
    }
}
