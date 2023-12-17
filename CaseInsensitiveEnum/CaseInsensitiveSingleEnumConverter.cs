using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Codibre.CaseInsensitiveEnum
{
    class CaseInsensitiveSingleEnumConverter<T> : JsonConverter<T> {
        private readonly Type TypeRef;
        private readonly Dictionary<string, T> _map = new Dictionary<string, T>();
        public CaseInsensitiveSingleEnumConverter(Type type): base() {
            foreach(var item in Enum.GetNames(type)) {
                _map.Add(item.ToLower(), (T)Enum.Parse(type, item));
            }
            TypeRef = type;
        }

        public override bool CanConvert(Type typeToConvert) => typeToConvert == TypeRef;

        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Number) {
                return (T)(object)reader.GetInt32();
            }
            var value = reader.GetString();
            if (value == null) return default;
            return _map.TryGetValue(value.ToLower(), out var result) ? result : throw new ArgumentException($"Invalid {TypeRef.Name}");
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            if (value == null) writer.WriteNullValue();
            else writer.WriteStringValue(value.ToString());
        }
    }
}