using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WordPressPCL.Utility
{
    class CustomCapabilitiesJsonConverter : JsonConverter<bool>
    {
        public override bool ReadJson(JsonReader reader, Type objectType, bool existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return Convert.ToBoolean(reader.ValueType == typeof(string) ? Convert.ToByte(reader.Value) : reader.Value);
        }

        public override void WriteJson(JsonWriter writer, bool value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
