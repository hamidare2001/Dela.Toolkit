using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dela.Toolkit.Extensions;

public class ObjectConverter : JsonConverter<object>
{
    public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Number)
            return reader.GetInt32();

        if (reader.TokenType == JsonTokenType.String)
            return reader.GetString();

        return reader.GetString();
    }

    public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }

    public static JsonSerializerOptions Option()
    {
        var options = new JsonSerializerOptions();
        options.Converters.Add(new ObjectConverter());
        return options;
    }
}