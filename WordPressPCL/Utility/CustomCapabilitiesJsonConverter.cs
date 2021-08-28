using Newtonsoft.Json;
using System;
using System.Globalization;

namespace WordPressPCL.Utility
{
    /// <summary>
    /// Custom JSON converter to convert string values to boolean in capabilities and extra_capabilities properties
    /// <see cref="Models.User.Capabilities"/>
    /// <see cref="Models.User.ExtraCapabilities"/>
    /// </summary>
    public class CustomCapabilitiesJsonConverter : JsonConverter<bool>
    {
        /// <inheritdoc />
        public override bool ReadJson(JsonReader reader, Type objectType, bool existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return Convert.ToBoolean(reader?.ValueType == typeof(string) ? Convert.ToByte(reader.Value, CultureInfo.InvariantCulture) : reader.Value, CultureInfo.InvariantCulture);
        }

        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, bool value, JsonSerializer serializer)
        {
            writer?.WriteValue(value);
        }
    }
}