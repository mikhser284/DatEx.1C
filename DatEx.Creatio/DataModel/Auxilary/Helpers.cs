namespace DatEx.Creatio.DataModel
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json.Serialization;
    using System.Linq;
    using System.Reflection;

    public class JsonIgnoreSerializationAttribute : Attribute { }

    class JsonPropertiesResolver : DefaultContractResolver
    {
        protected override List<MemberInfo> GetSerializableMembers(Type objectType)
        {
            //Return properties that do NOT have the JsonIgnoreSerializationAttribute
            return objectType.GetProperties()
                             .Where(pi => !Attribute.IsDefined(pi, typeof(JsonIgnoreSerializationAttribute)))
                             .ToList<MemberInfo>();
        }
    }

    public static class Ext_Guid
    {
        public static Guid? AsNullable(this Guid? guid) => guid == Guid.Empty ? null : guid;
    }

}