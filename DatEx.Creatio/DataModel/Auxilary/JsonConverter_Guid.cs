using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DatEx.Creatio.DataModel.Auxilary
{
    public class JsonConverter_Guid : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Guid?);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            JToken token = JToken.Load(reader);
            String tokenValue = token.Value<String>();
            Guid result = new Guid(tokenValue);
            if (result == default(Guid)) return null;
            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is null) return;            
            Guid guid = (Guid)value;
            if (guid == default(Guid))
            {
                writer.WriteNull();
                return;
            }
            writer.WriteValue(guid);
        }
    }
}