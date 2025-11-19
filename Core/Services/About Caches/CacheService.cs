using Doman.Contracts;
using Services.Abstractions.About_Caches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.About_Caches
{
    public class CacheService(ICacheRepository cacheRepository) : ICacheService
    {
        public async Task<string?> GetAsync(string key)
        {
            var Result = await cacheRepository.GetAsync(key);
            return Result;
        }

        public async Task SetAsync(string key, object value, TimeSpan span)
        {
            await cacheRepository.SetAsync(key, value, span);
        }
    }
}
