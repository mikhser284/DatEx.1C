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
        [OneC("Guid", "Ref_Key", "-", "-", Color = ConsoleColor.DarkBlue)]
        [JsonProperty("Ref_Key")]
        public Guid Id { get; set; }

        [OneC("Boolean?", "Predefined", "Булево", "?")]
        [JsonProperty("Predefined")]
        public Boolean? Predefined { get; set; }

        [OneC("String", "PredefinedDataName", "Строка", "?")]
        [JsonProperty("PredefinedDataName")]
        public String PredefinedDataName { get; set; }


        [OneC("String", "DataVersion", "Строка", "?")]
        [JsonProperty("DataVersion")]
        public String DataVersion { get; set; }

        /// <summary> Наименование </summary>
        [OneC("String", "Description", "Строка", "Наименование", Color = ConsoleColor.DarkBlue)]
        [JsonProperty("Description")]
        public String Description { get; set; }

        /// <summary> Код </summary>
        [OneC("String", "Code", "Строка", "Код", Color = ConsoleColor.DarkBlue)]
        [JsonProperty("Code")]
        public String Code { get; set; }

        /// <summary> Пометка удаления </summary>
        [OneC("Boolean?", "DeletionMark", "Булево", "Пометка удаления", Color = ConsoleColor.DarkBlue)]
        [JsonProperty("DeletionMark")]
        public Boolean? DeletionMark { get; set; }
    }

    public class OneCBaseHierarchicalLookup : OneCBaseLookup
    {
        /// <summary> Родитель </summary>
        [OneC("Guid?", "Parent_Key", "?", "Родитель", Color = ConsoleColor.DarkBlue)]
        [JsonProperty("Parent_Key")]
        public Guid? ParentId { get; set; }

        /// <summary> Является папкой </summary>
        [OneC("Boolean?", "IsFolder", "Булево", "Является папкой", Color = ConsoleColor.DarkBlue)]
        [JsonProperty("IsFolder")]
        public Boolean? IsFolder { get; set; }
    }

    public class OneCObject
    {
        public void Show()
        {
            Int32 maxPropNameLen = this.GetType().GetProperties().Max(x => x.Name.Length);
            
            Int32 maxOneCODataTypeLen = this.GetType().GetProperties()
                .Select(p => ((OneCAttribute)p.GetCustomAttributes(typeof(OneCAttribute), false)
                .FirstOrDefault())?.ODataType.Length).Max(x => x) ?? 0;
            Int32 maxOneCODataName = this.GetType().GetProperties()
                .Select(p => ((OneCAttribute)p.GetCustomAttributes(typeof(OneCAttribute), false)
                .FirstOrDefault())?.ODataName.Length).Max(x => x) ?? 0;

            Int32 maxOneCTypeLen = this.GetType().GetProperties()
                .Select(p => ((OneCAttribute)p.GetCustomAttributes(typeof(OneCAttribute), false)
                .FirstOrDefault())?.OneCType.Length).Max(x => x) ?? 0;
            Int32 maxOneCNameLen = this.GetType().GetProperties()
                .Select(p => ((OneCAttribute)p.GetCustomAttributes(typeof(OneCAttribute), false)
                .FirstOrDefault())?.OneCName.Length).Max(x => x) ?? 0;

            var oneC = this.GetType().GetProperties().Where(p => p.IsDefined(typeof(OneCAttribute), false));

            ShowPropertiesBlock(this, oneC, maxOneCODataTypeLen, maxOneCODataName, maxOneCTypeLen, maxOneCNameLen, maxPropNameLen);
            Console.ResetColor();

            static void ShowPropertiesBlock<T>(T obj, IEnumerable<PropertyInfo> propsInfo, Int32 maxOneCODataTypeLen, Int32 maxOneCODataName, Int32 maxOneCTypeLen, Int32 maxOneCNameLen, Int32 maxPropNameLen)
            {
                foreach(var p in propsInfo)
                {
                    var attribute = (OneCAttribute)p.GetCustomAttributes(typeof(OneCAttribute), false).FirstOrDefault();
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
                    Console.ForegroundColor = attribute?.Color ?? ConsoleColor.DarkGray;

                    Console.WriteLine($" {(attribute?.ODataType ?? "---").PadRight(maxOneCODataTypeLen)} | {(attribute?.ODataName ?? "---").PadRight(maxOneCODataName)} | {(attribute?.OneCType ?? "---").PadRight(maxOneCTypeLen)} │ {(attribute?.OneCName ?? "---").PadRight(maxOneCNameLen)} │ {p.Name.PadRight(maxPropNameLen)} │ {propValue}");
                }
            }
        }
    }
}
