using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DatEx.Creatio.DataModel.Auxilary
{
    public class JsonConverter_Date : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            JToken token = JToken.Load(reader);
            DateTime result = token.Value<DateTime>();
            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null) return;
            DateTime dateTime = (DateTime)value;
            if (dateTime == default(DateTime))
            {
                writer.WriteNull();
                return;
            }
            writer.WriteValue($"{dateTime:yyyy-MM-dd}");
        }
    }
}
