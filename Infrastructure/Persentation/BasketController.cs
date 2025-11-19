using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.About_Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpGet("/{id}")]
        public async Task<IActionResult> GetBasketById(string Id)
        {
            var res = await _serviceManager.BasketService.GetBasketAsync(Id);
            return Ok(res);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateBasket([FromBody] BasketDto dto)
        {
            var res = await _serviceManager.BasketService.CreateBasketAsync(dto, TimeSpan.FromDays(1));
            return Ok(res);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteBasket(string Id)
        {
           var res = await _serviceManager.BasketService.DeleteBasketAsync(Id);
            return NoContent();
        }

    }
}
