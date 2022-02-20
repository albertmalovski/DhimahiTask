using System;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IResponseCacheService
    {
        Task CacheResponse(string cacheKey, object response, TimeSpan timeToLive);
        Task<string> GetCachedResponse(string cacheKey);
    }
}