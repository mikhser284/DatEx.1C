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
        [CreatioPropertyMap("Guid", "IdOneC", "Ref_Key")]
        [JsonProperty("Ref_Key")]
        public Guid Ref_Key { get; set; }

        [CreatioAux]
        [JsonProperty("Predefined")]
        public Boolean? Predefined { get; set; }

        [CreatioAux]
        [JsonProperty("PredefinedDataName")]
        public String PredefinedDataName { get; set; }


        [CreatioAux]
        [JsonProperty("DataVersion")]
        public String DataVersion { get; set; }


        [CreatioPropertyMap("String", "Name", "Description")]
        [JsonProperty("Description")]
        public String Description { get; set; }


        [CreatioAux]
        [JsonProperty("Code")]
        public String Code { get; set; }

        [CreatioAux]
        [JsonProperty("DeletionMark")]
        public Boolean? DeletionMark { get; set; }
    }

    public class OneCBaseHierarchicalLookup : OneCBaseLookup
    {
        [CreatioAux]
        [JsonProperty("Parent_Key")]
        public Guid? ParentId { get; set; }

        [CreatioAux]
        [JsonProperty("IsFolder")]
        public Boolean? IsFolder { get; set; }
    }

    public class OneCObject
    {
        public void Show()
        {
            Int32 maxCreatioPropNameLen = this.GetType().GetProperties().Max(x => x.Name.Length);
            Int32 maxOneCPropNameLen = this.GetType().GetProperties()
                .Select(p => ((JsonPropertyAttribute)p.GetCustomAttributes(typeof(JsonPropertyAttribute), false)
                .FirstOrDefault())?.PropertyName.Length).Max(x => x) ?? 0;

            var mapableProperties = this.GetType().GetProperties().Where(p => p.IsDefined(typeof(CreatioPropertyMapAttribute), false));
            var auxProperties = this.GetType().GetProperties().Where(p => p.IsDefined(typeof(CreatioAuxAttribute), false));
            var unmapableProperties = this.GetType().GetProperties().Where(p => p.IsDefined(typeof(CreatioIgnoreAttribute), false));

            Console.ForegroundColor = ConsoleColor.Green;
            ShowPropertiesBlock(this, mapableProperties, maxCreatioPropNameLen, maxOneCPropNameLen);
            Console.ForegroundColor = ConsoleColor.Blue;
            ShowPropertiesBlock(this, auxProperties, maxCreatioPropNameLen, maxOneCPropNameLen);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            ShowPropertiesBlock(this, unmapableProperties, maxCreatioPropNameLen, maxOneCPropNameLen);
            Console.ResetColor();

            static void ShowPropertiesBlock<T>(T obj, IEnumerable<PropertyInfo> propsInfo, Int32 maxCreatioPropNameLen, Int32 maxOneCPropNameLen)
            {
                foreach(var p in propsInfo)
                {
                    var attribute = (JsonPropertyAttribute)p.GetCustomAttributes(typeof(JsonPropertyAttribute), false).FirstOrDefault();
                    String propValue = p.GetValue(obj)?.ToString();
                    if(p.PropertyType != typeof(String) && typeof(ICollection).IsAssignableFrom(p.PropertyType))
                    {
                        var val = (ICollection)p.GetValue(obj);
                        propValue = $"{val.Count} шт.";
                    }
                    Console.WriteLine($" {p.Name.PadRight(maxCreatioPropNameLen)} │ {attribute.PropertyName.PadRight(maxOneCPropNameLen)} │ {propValue}");
                }
            }
        }
    }
}
