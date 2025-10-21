using AutoMapper;
using Doman.Contracts;
using Doman.Entities.About_Product;
using Services.Abstractions.About_Products;
using Shared.About_Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.About_Products
{
    public class ProductService(IUnitOfWork _unitOfWork , IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<ProductResponse>> GetAllProductsAsync()
        {
           var products = await _unitOfWork.GetRepository<int,Product>().GetAllAsync();
           return _mapper.Map<IEnumerable<ProductResponse>>(products);
        }

        public async Task<IEnumerable<BrandTypeResponse>> GetAllBrandsAsync()
        {
            var Brands =await _unitOfWork.GetRepository<int, ProductBrand>().GetAllAsync();
            return _mapper.Map<IEnumerable<BrandTypeResponse>>(Brands);
        }

        public async Task<IEnumerable<BrandTypeResponse>> GetAllTypesAsync()
        {
            var Types = await _unitOfWork.GetRepository<int, ProductType>().GetAllAsync();
            return _mapper.Map<IEnumerable<BrandTypeResponse>>(Types);
        }

        public async Task<ProductResponse> GetProductByIdAsync(int Id)
        {
            var product = await _unitOfWork.GetRepository<int, Product>().GetAsync(Id);
            return _mapper.Map<ProductResponse>(product);
        }
    }
}
