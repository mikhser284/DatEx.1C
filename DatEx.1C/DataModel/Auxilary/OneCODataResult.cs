using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DatEx._1C
{
    class OneCODataResult<T>
    {
        [JsonProperty("odatametadata")]
        public String ODdataMetadata { get; set; }

        [JsonProperty("value")]
        public List<T> Values { get; set; }
    }
}
