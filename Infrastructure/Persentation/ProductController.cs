using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Abstractions.About_Caches;
using Shared.About_Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IServiceManager _serviceManager , ICacheService _cacheService) : ControllerBase
    {
    
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductQueryParams Params)
        {
            string cacheKey = $"products-{Params.PageIndex}-{Params.PageSize}-{Params.Sorting}-{Params.Search}-{Params.BrandId}-{Params.TypeId}";

            var cachedResult = await _cacheService.GetAsync(cacheKey); 

            if(cachedResult is not null)
            {
                // Return the cached result if it exists
                return Ok(cachedResult);
            }
            var Result = await _serviceManager.ProductService.GetAllProductsAsync(Params);
            await _cacheService.SetAsync(cacheKey, Result, TimeSpan.FromMinutes(60));

            if (Result is null) return BadRequest(); //400

            return Ok(Result); //200
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int? id)
        {
            string CacheKey = $"product-{id}";
            var cachedProduct = await _cacheService.GetAsync(CacheKey);
            if (cachedProduct is not null)
            {
                return Ok(cachedProduct);
            }

            if (id is null || id <= 0) return BadRequest(); //400
            var Result = await _serviceManager.ProductService.GetProductByIdAsync(id.Value);
            await _cacheService.SetAsync(CacheKey, Result, TimeSpan.FromMinutes(60));
            return Ok(Result); //200
        }
        [HttpGet("brands")]
        public async Task<IActionResult> GetAllProductBrands()
        {
            var cacheKey = "product-brands";
            var cachedBrands = await _cacheService.GetAsync(cacheKey);
            var Result = await _serviceManager.ProductService.GetAllBrandsAsync();
            if (Result is null) return BadRequest(); //400
            await _cacheService.SetAsync(cacheKey, Result, TimeSpan.FromMinutes(60));
            return Ok(Result); //200
        }
        [HttpGet("types")]
        public async Task<IActionResult> GetAllProductTypes()
        {
            var cacheKey = "product-types";
            var cachedTypes = await _cacheService.GetAsync(cacheKey);
            var Result = await _serviceManager.ProductService.GetAllTypesAsync();
            if (Result is null) return BadRequest(); //400
            await _cacheService.SetAsync(cacheKey, Result, TimeSpan.FromMinutes(60));

            return Ok(Result); //200
        }
    }
}
