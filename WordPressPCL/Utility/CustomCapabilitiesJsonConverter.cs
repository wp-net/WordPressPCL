using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WordPressPCL.Utility
{
    /// <summary>
    /// Custom JSON converter for individual boolean capability values that may be
    /// represented as JSON booleans or as the string literals <c>"0"</c>/<c>"1"</c> in
    /// WordPress REST API responses.
    /// <para>
    /// Use <see cref="CapabilitiesDictionaryConverter"/> when you need to deserialize an
    /// entire capabilities dictionary (i.e. on
    /// <see cref="Models.User.Capabilities"/> or
    /// <see cref="Models.User.ExtraCapabilities"/>).
    /// </para>
    /// </summary>
    public class CustomCapabilitiesJsonConverter : JsonConverter<bool>
    {
        /// <inheritdoc />
        public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.True)
            {
                return true;
            }

            if (reader.TokenType == JsonTokenType.False)
            {
                return false;
            }

            if (reader.TokenType == JsonTokenType.String)
            {
                string? value = reader.GetString();
                // WordPress may send "1" for true and "0" for false.
                return Convert.ToBoolean(Convert.ToByte(value, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
            }

            throw new JsonException($"Unexpected token type {reader.TokenType} when reading a boolean capability value.");
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
        {
            writer.WriteBooleanValue(value);
        }
    }

    /// <summary>
    /// Custom JSON converter for <see cref="IDictionary{TKey,TValue}">IDictionary&lt;string, bool&gt;</see>
    /// where each value may be a JSON boolean or the string literals <c>"0"</c>/<c>"1"</c>.
    /// Apply this converter on the
    /// <see cref="Models.User.Capabilities"/> and
    /// <see cref="Models.User.ExtraCapabilities"/> properties.
    /// </summary>
    public class CapabilitiesDictionaryConverter : JsonConverter<IDictionary<string, bool>>
    {
        private static readonly CustomCapabilitiesJsonConverter _boolConverter = new();

        /// <inheritdoc />
        public override bool CanConvert(Type typeToConvert)
            => typeof(IDictionary<string, bool>).IsAssignableFrom(typeToConvert);

        /// <inheritdoc />
        public override IDictionary<string, bool>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException($"Expected start of JSON object, got {reader.TokenType}.");
            }

            Dictionary<string, bool> result = new();
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }

                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException($"Expected property name, got {reader.TokenType}.");
                }

                string key = reader.GetString() ?? throw new JsonException("Property name is null.");
                reader.Read();
                result[key] = _boolConverter.Read(ref reader, typeof(bool), options);
            }

            return result;
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, IDictionary<string, bool> value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            foreach (KeyValuePair<string, bool> pair in value)
            {
                writer.WritePropertyName(pair.Key);
                _boolConverter.Write(writer, pair.Value, options);
            }
            writer.WriteEndObject();
        }
    }
}