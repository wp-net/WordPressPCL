using Newtonsoft.Json;
using System;

namespace WordPressPCL.Utility
{
    /// <summary>
    /// Custom JSON converter to convert string values to boolean in capabilities and extra_capabilities properties
    /// <see cref="WordPressPCL.Models.User.Capabilities"/>
    /// <see cref="WordPressPCL.Models.User.ExtraCapabilities"/>
    /// </summary>
    public class CustomCapabilitiesJsonConverter : JsonConverter<bool>
    {
        /// <inheritdoc />
        public override bool ReadJson(JsonReader reader, Type objectType, bool existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return Convert.ToBoolean(reader.ValueType == typeof(string) ? Convert.ToByte(reader.Value) : reader.Value);
        }

        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, bool value, JsonSerializer serializer)
        {
            writer.WriteValue(value);
        }
    }
}