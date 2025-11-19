using Shared.About_Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions.About_Baskets
{
    public interface IBasketService
    {
        Task<BasketDto?> GetBasketAsync(string id);
        Task<BasketDto?> CreateBasketAsync(BasketDto dto , TimeSpan span);
        Task<bool> DeleteBasketAsync(string id);

    }
}
