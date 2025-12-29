using System.Text.Json;
using MemoryPack;
using StackExchange.Redis;

namespace FuelStation.PartExchange.Application.Common.Extension;

public static class CacheExtensions
{
    public static byte[] MemoryPackSerialize<T>(this T obj) => MemoryPackSerializer.Serialize(obj);


    public async static Task<T?> GetAsync<T>(this IDatabase database,string key)
    {
        var value = await  database.StringGetAsync(key);
        if (!value.IsNullOrEmpty)
        {
            return MemoryPackSerializer.Deserialize<T>((byte[])value!);
        }

        return default;
    }
    
    public async static Task<T?> JsonGetAsync<T>(this IDatabase database,string key)
    {
        var value = await  database.StringGetAsync(key);
        if (!value.IsNullOrEmpty)
        {
            return JsonSerializer.Deserialize<T>((byte[])value!);
        }

        return default;
    }
}