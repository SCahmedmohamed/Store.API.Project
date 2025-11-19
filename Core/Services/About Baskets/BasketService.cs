using AutoMapper;
using Doman.Contracts;
using Doman.Entities.About_Basket;
using Doman.Exceptions.BadRequest;
using Doman.Exceptions.NotFound;
using Services.Abstractions.About_Baskets;
using Shared.About_Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.About_Baskets
{
    public class BasketService(IBasketRepository _basketRepository , IMapper _mapper) : IBasketService
    {

        public async Task<BasketDto?> GetBasketAsync(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            if (basket == null) throw new BasketNotFoundException(id);
            var res = _mapper.Map<BasketDto>(basket);
            return res;
        }
        public async Task<BasketDto?> CreateBasketAsync(BasketDto dto, TimeSpan span)
        {
            var basket = _mapper.Map<CustomerBasket>(dto);
            var createdBasket = await _basketRepository.CreateBasketAsync(basket, span);
            if (createdBasket == null) throw new CreateOrUpdateBasketBadRequestException();
            return _mapper.Map<BasketDto>(createdBasket);
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
            var flag =  await _basketRepository.DeleteBasketAsync(id);
            if(flag == false) throw new DeleteBasketBadRequestException();

            return flag;
        }
    }
}
