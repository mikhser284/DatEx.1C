using System;
using System.Collections.Generic;
using System.Text;
using DatEx.OneC.DataModel.Auxilary;
using Newtonsoft.Json;

namespace DatEx.OneC.DataModel
{
    [OneC("Catalog_ВидыКонтактнойИнформации", "Catalog_ВидыКонтактнойИнформации", "", "")]
    [JsonObject("Catalog_ВидыКонтактнойИнформации")]
    public class ContactInfoType : OneCBaseLookup
    {
        [OneC("String", "Тип", "Перечисление.ТипыКонтактнойИнформации", "Тип", Color = ConsoleColor.Yellow)]
        [JsonProperty("Тип")]
        public String Type { get; set; }

        [OneC("String", "ВидОбъектаКонтактнойИнформации", "Перечисление.ВидыОбъектовКонтактнойИнформации", "ВидОбъектаКонтактнойИнформации")]
        [JsonProperty("ВидОбъектаКонтактнойИнформации")]
        public String KindOfContactInfoObject { get; set; }

        public override String ToString() => $"{Type} {KindOfContactInfoObject}";

        // ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■

        //[OneC("", "", "", "")]

        //[OneC("String", "", "Строка", "")]
        //[JsonProperty("")]
        //public String Prop { get; set; }

        //[OneC("Boolean", "", "Булево", "")]
        //[JsonProperty("")]
        //public Boolean? Prop { get; set; }


        //[OneC("Guid?", "", "-", "-")]
        //[JsonProperty("")]
        //public Guid? PropId { get; set; }

        //[OneC("?", "NavProp()", "", "")]
        //[JsonIgnore]
        //public Object NavProp_ { get; set; }
    }
}
