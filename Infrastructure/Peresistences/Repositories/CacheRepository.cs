using Doman.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Peresistences.Repositories
{
    public class CacheRepository(IConnectionMultiplexer connection) : ICacheRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();
        public async Task<string?> GetAsync(string Key)
        {
            var RedisValue = await _database.StringGetAsync(Key);

            return RedisValue;
        }

        public async Task SetAsync(string Key, object value, TimeSpan span)
        {
            await _database.StringSetAsync(Key, JsonSerializer.Serialize(value), span);
        }
    }
}
