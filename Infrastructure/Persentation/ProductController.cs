using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IServiceManager _serviceManager) : ControllerBase
    {
    
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            // Logic to retrieve all products
            var Result = await _serviceManager.ProductService.GetAllProductsAsync();
            if(Result is null) return BadRequest(); //400


            return Ok(Result); //200
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int? id)
        {
            if(id is null || id <= 0) return BadRequest(); //400
            var Result = await _serviceManager.ProductService.GetProductByIdAsync(id.Value);
            if (Result is null) return NotFound(); //404

            return Ok(Result); //200
        }
        [HttpGet("brands")]
        public async Task<IActionResult> GetAllProductBrands()
        {
            var Result = await _serviceManager.ProductService.GetAllBrandsAsync();
            if (Result is null) return BadRequest(); //400

            return Ok(Result); //200
        }
        [HttpGet("types")]
        public async Task<IActionResult> GetAllProductTypes()
        {
            var Result = await _serviceManager.ProductService.GetAllTypesAsync();
            if (Result is null) return BadRequest(); //400

            return Ok(Result); //200
        }
    }
}
