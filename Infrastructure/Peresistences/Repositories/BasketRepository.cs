using Doman.Contracts;
using Doman.Entities.About_Basket;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
namespace Peresistences.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private IDatabase _database = connection.GetDatabase();
        public async Task<CustomerBasket?> GetBasketAsync(string id)
        {
            var redisValue = await _database.StringGetAsync(id);
            if(redisValue.IsNullOrEmpty) return null;
            var basket = JsonSerializer.Deserialize<CustomerBasket>(redisValue);
            if(basket == null) return null;
            return basket;
        }
        public async Task<CustomerBasket?> CreateBasketAsync(CustomerBasket customerBasket, TimeSpan span)
        {
            var redisValue = JsonSerializer.Serialize(customerBasket);
            var flag = await _database.StringSetAsync(customerBasket.Id, redisValue , span);
            if(!flag) return null;

            return await GetBasketAsync(customerBasket.Id);
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
           return await _database.KeyDeleteAsync(id);

             

        }

    }
}
