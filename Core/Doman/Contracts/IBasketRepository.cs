using Doman.Entities.About_Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doman.Contracts
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?>  GetBasketAsync(string id);
        Task<CustomerBasket?> CreateBasketAsync(CustomerBasket basket, TimeSpan span);
        Task<bool> DeleteBasketAsync(string id);
    }
}
