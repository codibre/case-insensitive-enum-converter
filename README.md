[![Actions Status](https://github.com/Codibre/dotnet-case-insensitive-enum-converter/workflows/build/badge.svg)](https://github.com/Codibre/dotnet-case-insensitive-enum-converter/actions)
[![Actions Status](https://github.com/Codibre/dotnet-case-insensitive-enum-converter/workflows/test/badge.svg)](https://github.com/Codibre/dotnet-case-insensitive-enum-converter/actions)
[![Actions Status](https://github.com/Codibre/dotnet-case-insensitive-enum-converter/workflows/lint/badge.svg)](https://github.com/Codibre/dotnet-case-insensitive-enum-converter/actions)
[![benchmark](https://github.com/Codibre/dotnet-case-insensitive-enum-converter/actions/workflows/benchmark.yml/badge.svg)](https://github.com/Codibre/dotnet-case-insensitive-enum-converter/actions/workflows/benchmark.yml)
[![Test Coverage](https://api.codeclimate.com/v1/badges/1c4f61aa33d8e5f7c29e/test_coverage)](https://codeclimate.com/github/codibre/dotnet-case-insensitive-enum/test_coverage)
[![Maintainability](https://api.codeclimate.com/v1/badges/1c4f61aa33d8e5f7c29e/maintainability)](https://codeclimate.com/github/codibre/dotnet-case-insensitive-enum/maintainability)

JsonConvert to treat any Enum as case insensitive

## Why

Case insensitive enum conversion can lead to more compatibility throughOut a cross-platform as, for best practice, we never use two enum values equivalents case insensitive, and depending on language best practices, some service implemented in a different language may implement the enum differently, so, a case insensitive conversion can save you a lot of discussion time.

## How to use it

First, import the application namespace:

```c#
using Codibre.CaseInsensitiveEnum
```

Now, create a singleton option to use in very needed conversion. It is important to create it singleton to avoid performances issues (not related to this package, though):

```c#
readonly static JsonSerializerOptions options = new JsonSerializerOptions
{
    Converters = { new CaseInsensitiveEnumConverter() },
    PropertyNameCaseInsensitive = true,
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
};
```

Finally, use the created option on your Serializer:

```c#
var converted = JsonSerializer.Deserialize<MyTargetClass>(serializedValue, options);
```

That's it! For best practice, is good to create a serializer class, or method extension, centralized to use through your project, guaranteeing, that way, a singleton option definition.

Also, notice that, if we're talking about case insensitive serialization for enum, it is implied that you're serializing it to the enum property name, not the value, so, be aware that using this package for your serialization will lead to that behavior.

## License

Licensed under [MIT](https://en.wikipedia.org/wiki/MIT_License).
