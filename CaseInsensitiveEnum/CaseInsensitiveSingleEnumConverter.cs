using System;
using System.Collections.Generic;

namespace Codibre.CaseInsensitiveEnum
{
    class CaseInsensitiveSingleEnumConverter<T> : JsonConverter<T> {
        private readonly Type TypeRef;
        private readonly Dictionary<string, T> _map = new();
        public CaseInsensitiveSingleEnumConverter(Type type): base() {
            foreach(var item in Enum.GetNames(type)) {
                _map.Add(item.ToLower(), (T)Enum.Parse(type, item));
            }
            TypeRef = type;
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return true;
        }

        public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            if (value is null) return default;
            return _map.TryGetValue(value.ToLower(), out var result) ? result : throw new ArgumentException($"Invalid {TypeRef.Name}");
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            if (value is null) writer.WriteNullValue();
            else writer.WriteStringValue(value.ToString());
        }
    }
}