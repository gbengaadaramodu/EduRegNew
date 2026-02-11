using Microsoft.Extensions.Caching.Memory;
using Microsoft.Identity.Client;

namespace EduReg.Services.Repositories
{
    public class TokenCacheRepositories
    {
        private readonly IMemoryCache _cache;
        private readonly MemoryCacheEntryOptions _cacheOptions;
        private readonly ILogger<TokenCache> _logger;

        public TokenCacheRepositories(IMemoryCache cache, ILogger<TokenCache> logger)
        {
            _cache = cache;
            _logger = logger;
            TimeSpan tp = new TimeSpan(0, 1, 0);
            _cacheOptions = new MemoryCacheEntryOptions
            {
                SlidingExpiration = tp,
                PostEvictionCallbacks =
            {
                new PostEvictionCallbackRegistration
                {
                    EvictionCallback = OnTokenEvicted
                }
            }
            };
        }

        public void AddToken(string token)
        {
            _cache.Set(token, token, _cacheOptions);
            _logger.LogInformation("Token {Token} added to cache at {Time}", token, DateTime.UtcNow);
        }

        public bool ContainsToken(string token)
        {
            return _cache.TryGetValue(token, out _);
        }

        public void RemoveToken(string token)
        {
            _cache.Remove(token);
            _logger.LogInformation("Token {Token} explicitly removed from cache at {Time}", token, DateTime.UtcNow);
        }

        private void OnTokenEvicted(object key, object value, EvictionReason reason, object state)
        {
            _logger.LogInformation("Token {Token} was automatically removed from cache due to {Reason} at {Time}", key, reason, DateTime.UtcNow);
        }
    }
}
