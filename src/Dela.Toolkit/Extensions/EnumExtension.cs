using System.ComponentModel;
using System.Reflection;

namespace Dela.Toolkit.Extensions;

public static class EnumExtension
{
    public static List<TEnum> GetEnumValues<TEnum>() where TEnum : Enum
    {
        if (!typeof(TEnum).IsEnum)
        {
            throw new ArgumentException("Type parameter must be an Enum.", nameof(TEnum));
        }

        return Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToList();
    }

    public static string? Description(this Enum @enum)
    {
        try
        {
            var @string = @enum.ToString();

            var attribute = @enum.GetType()?.GetField(@string)?.GetCustomAttribute<DescriptionAttribute>(false);

            return attribute != null ? attribute.Description : @string;
        }
        catch // Log nothing, just return an empty string
        {
            return string.Empty;
        }
    }
}