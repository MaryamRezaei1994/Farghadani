using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application.Common.Extension;

public static class SerializationExtensions
{
    private static JsonSerializerOptions options = new() {
        ReferenceHandler = ReferenceHandler.IgnoreCycles,
        PropertyNameCaseInsensitive = true,
        WriteIndented = true
    };
        
    public static string Serialize(this object obj) => JsonSerializer.Serialize(obj, options);


    public static T Deserialize<T>(this string obj)
    {
        if (string.IsNullOrEmpty(obj))
        {
            return default !;
        }

        return JsonSerializer.Deserialize<T>(obj, options)!;
    }
}