using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Codibre.CaseInsensitiveEnum
{
    public class CaseInsensitiveEnumConverter : JsonConverterFactory
    {
        private static Type converterType = typeof(CaseInsensitiveSingleEnumConverter<>);
        private static Dictionary<Type, JsonConverter> Converters = new Dictionary<Type, JsonConverter>();
        public override bool CanConvert(Type typeToConvert) => typeToConvert.IsEnum;

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            if (!Converters.TryGetValue(typeToConvert, out var result)) {
                result = (JsonConverter)Activator.CreateInstance(converterType.MakeGenericType(typeToConvert), new object[] { typeToConvert });
                Converters[typeToConvert] = result;
            }
            return result;
        }
    }
}