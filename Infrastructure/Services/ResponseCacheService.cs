using System;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Interfaces.Services;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Services
{
  public class ResponseCacheService : IResponseCacheService
  {
    private readonly IDatabase _database;

        public Task CacheResponse(string cacheKey, object response, TimeSpan timeToLive)
        {
            throw new NotImplementedException();
        }

        public async Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive)
        {
                throw new NotImplementedException();
        }

        public async Task<string> GetCachedResponse(string cacheKey)
        {
            throw new NotImplementedException();
        }
  }
}
