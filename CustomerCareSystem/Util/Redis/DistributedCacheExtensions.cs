using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Caching.Distributed;

namespace CustomerCareSystem.Util.Redis;

public static class DistributedCacheExtensions
{
    private static JsonSerializerOptions _jsonSerializerOptions
        = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = null,
            WriteIndented = true,
            AllowTrailingCommas = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

    public static Task SetAsync<T>(this IDistributedCache cache, string key, T value,
        DistributedCacheEntryOptions options)
    {
        var bytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value, _jsonSerializerOptions));
        return cache.SetAsync(key, bytes, options);
    }

    public static Task SetAsync<T>(this IDistributedCache cache, string key, T value)
    {
        return SetAsync(cache, key, value,
            new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(30))
                .SetAbsoluteExpiration(TimeSpan.FromHours(1)));
    }

    public static bool TryGetValue<T>(this IDistributedCache cache, string key, out T? value)
    {
        var bytes = cache.Get(key);
        value = default;
        if (bytes == null)
        {
            return false;
        }
        value = JsonSerializer.Deserialize<T>(bytes, _jsonSerializerOptions);
        return true;
        
    }

    public static async Task<T?> GetOrSetAsync<T>(this IDistributedCache cache, string key, Func<Task<T>> task,
        DistributedCacheEntryOptions? options = null)
    {
        if (options == null)
        {
            options = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(30))
                .SetAbsoluteExpiration(TimeSpan.FromHours(1));
        }

        if (cache.TryGetValue(key, out T? value))
        {
            return value;
        }
        value = await task();
        if (value is not null)
        {
            await cache.SetAsync<T>(key, value, options);
        }
        return value;   
    }
}