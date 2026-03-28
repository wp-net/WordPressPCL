using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WordPressPCL.Utility
{
    /// <summary>
    /// A <see cref="JsonConverterFactory"/> that serializes and deserializes enum values using
    /// their <see cref="EnumMemberAttribute.Value"/> when present, falling back to the lower-case
    /// member name otherwise.
    /// </summary>
    public sealed class JsonStringEnumMemberConverter : JsonConverterFactory
    {
        /// <inheritdoc />
        public override bool CanConvert(Type typeToConvert) => typeToConvert.IsEnum;

        /// <inheritdoc />
        public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            Type converterType = typeof(EnumMemberConverter<>).MakeGenericType(typeToConvert);
            return (JsonConverter?)Activator.CreateInstance(converterType);
        }

        private sealed class EnumMemberConverter<TEnum> : JsonConverter<TEnum> where TEnum : struct, Enum
        {
            private readonly Dictionary<string, TEnum> _readMap;
            private readonly Dictionary<TEnum, string> _writeMap;

            public EnumMemberConverter()
            {
                _readMap = new Dictionary<string, TEnum>(StringComparer.OrdinalIgnoreCase);
                _writeMap = new Dictionary<TEnum, string>();

                foreach (TEnum value in Enum.GetValues<TEnum>())
                {
                    string name = value.ToString();
                    FieldInfo? field = typeof(TEnum).GetField(name);
                    EnumMemberAttribute? attr = field?.GetCustomAttribute<EnumMemberAttribute>();
                    string memberValue = attr?.Value ?? name.ToLowerInvariant();
                    _readMap[memberValue] = value;
                    _writeMap[value] = memberValue;
                }
            }

            public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                string? value = reader.GetString();
                if (value != null && _readMap.TryGetValue(value, out TEnum result))
                {
                    return result;
                }
                throw new JsonException($"Unable to convert \"{value}\" to enum {typeof(TEnum).Name}.");
            }

            public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
            {
                if (_writeMap.TryGetValue(value, out string? memberValue))
                {
                    writer.WriteStringValue(memberValue);
                }
                else
                {
                    writer.WriteStringValue(value.ToString().ToLowerInvariant());
                }
            }
        }
    }
}
