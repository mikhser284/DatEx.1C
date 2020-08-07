using System;
using System.Collections.Generic;
using System.Text;
using DatEx.OneC.DataModel.Auxilary;
using Newtonsoft.Json;

namespace DatEx.OneC.DataModel
{
    [JsonObject("Catalog_ВидыКонтактнойИнформации")]
    public class ContactInfoType : OneCBaseLookup
    {
        [CreatioAux]
        [JsonProperty("Тип")]
        public String Type { get; set; }

        [CreatioAux]
        [JsonProperty("ВидОбъектаКонтактнойИнформации")]
        public String KindOfContactInfoObject { get; set; }

        public override String ToString() => $"{Type} {KindOfContactInfoObject}";
    }
}
