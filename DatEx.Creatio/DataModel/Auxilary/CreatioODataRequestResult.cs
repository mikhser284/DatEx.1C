using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace DatEx.Creatio.DataModel
{
    public class CreatioOdataRequestResult<T>
    {
        [JsonProperty("odatacontext")]
        public String ODataContext { get; set; }

        [JsonProperty("value")]
        public List<T> Values { get; set; } = new List<T>();
    }
}
