using Services.Abstractions.About_Auth;
using Services.Abstractions.About_Baskets;
using Services.Abstractions.About_Caches;
using Services.Abstractions.About_Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IServiceManager
    {
        IProductService ProductService { get; }
        IBasketService BasketService { get; }
        ICacheService CacheService { get; }
        IAuthService AuthService { get; }
    }
}
