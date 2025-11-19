using AutoMapper;
using Doman.Contracts;
using Doman.Entities.About_Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Services.About_Auth;
using Services.About_Baskets;
using Services.About_Caches;
using Services.About_Products;
using Services.Abstractions;
using Services.Abstractions.About_Auth;
using Services.Abstractions.About_Baskets;
using Services.Abstractions.About_Caches;
using Services.Abstractions.About_Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager(
        IUnitOfWork _unitOfWork ,
        IMapper _mapper ,
        IBasketRepository basketRepository ,
        ICacheRepository cacheRepository,
        UserManager<AppUser> userManager,
        IConfiguration configuration
        ) : IServiceManager
    {
        public IProductService ProductService { get; } = new ProductService(_unitOfWork,_mapper);

        public IBasketService BasketService { get; } = new BasketService(basketRepository, _mapper);
        public ICacheService CacheService { get; } = new CacheService(cacheRepository);
        public IAuthService AuthService { get; } = new AuthService(userManager , configuration);
    }
}
