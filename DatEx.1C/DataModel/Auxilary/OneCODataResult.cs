using System;
using System.Collections.Generic;
using DatEx.OneS.DataModel;
using Newtonsoft.Json;

namespace DatEx.OneS
{
    class OneCODataResult<T>
    {
        [JsonProperty("odatametadata")]
        public String ODdataMetadata { get; set; }

        [JsonProperty("value")]
        public List<T> Values { get; set; }
    }

    class ODataIdentifiersResult
    {
        [JsonProperty("odatametadata")]
        public String ODdataMetadata { get; set; }

        [JsonProperty("value")]
        public List<Identifier> Identifiers { get; set; }
    }

    public class Identifier
    {
        [JsonProperty("Ref_Key")]
        public Guid Id { get; set; }
    }
}
