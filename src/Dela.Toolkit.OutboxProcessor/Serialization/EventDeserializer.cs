using System.Text.Json;
using System.Text.Json.Serialization;
using Dela.Toolkit.Application.Events;

namespace Dela.Toolkit.OutboxProcessor.Serialization;

public static class EventDeserializer
{
    private static readonly JsonSerializerOptions _options;
    static EventDeserializer()
    {
        _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        
        // For private setters, you need to add a custom converter or use a different approach
        // since System.Text.Json doesn't have built-in PrivateSetterContractResolver like Newtonsoft.Json
        _options.Converters.Add(new PrivateSetterConverter());
    }

    public static IEvent? Deserialize(Type type, string body)
    {
        return JsonSerializer.Deserialize(body, type, _options) as IEvent;
    }
}

public class PrivateSetterConverter : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        // Only convert types that are classes (not structs)
        return typeToConvert.IsClass && !typeToConvert.IsAbstract;
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        // Create a generic converter for the specific type
        var converterType = typeof(PrivateSetterConverter<>).MakeGenericType(typeToConvert);
        return (JsonConverter)Activator.CreateInstance(converterType)!;
    }
}

public class PrivateSetterConverter<T> : JsonConverter<T> where T : class
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
            return null!;

        // Create a temporary copy of options without this converter to avoid infinite recursion
        var optionsWithoutConverter = new JsonSerializerOptions(options);
        foreach (var converter in options.Converters)
        {
            if (converter is not PrivateSetterConverter)
            {
                optionsWithoutConverter.Converters.Add(converter);
            }
        }

        // Deserialize using the standard approach first
        var obj = JsonSerializer.Deserialize<T>(ref reader, optionsWithoutConverter);

        if (obj == null)
            return null!;

        // For more complex scenarios with truly private setters, you would need to use reflection
        // or source generators. Alternatively, consider using init-only properties in C# 9+

        return obj;
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, options);
    }
}
