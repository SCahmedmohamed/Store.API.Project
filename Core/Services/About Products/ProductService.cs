using AutoMapper;
using Doman.Contracts;
using Doman.Entities.About_Product;
using Doman.Exceptions.NotFound;
using Services.Abstractions.About_Products;
using Services.Specifications;
using Services.Specifications.About_Product;
using Shared;
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
        public async Task<PaginatedResponse<ProductResponse>> GetAllProductsAsync(ProductQueryParams Params)
        {
            var spec = new ProductWithBrandAndTypeSpecification(Params);
            //spec.Includes.Add(p => p.Brand);
            //spec.Includes.Add(p => p.Type);
            var products = await _unitOfWork.GetRepository<int,Product>().GetAllAsync(spec);
            var Result = _mapper.Map<IEnumerable<ProductResponse>>(products);
            return new PaginatedResponse<ProductResponse>(Params.PageIndex,Params.PageSize,0,Result);
        }

        public async Task<IEnumerable<BrandTypeResponse>> GetAllBrandsAsync()
        {
            var spec = new BaseSpecifications<int, ProductBrand>(null);
            var Brands =await _unitOfWork.GetRepository<int, ProductBrand>().GetAllAsync(spec);
            return _mapper.Map<IEnumerable<BrandTypeResponse>>(Brands);
        }

        public async Task<IEnumerable<BrandTypeResponse>> GetAllTypesAsync()
        {
            var spec = new BaseSpecifications<int, ProductType>(null);

            var Types = await _unitOfWork.GetRepository<int, ProductType>().GetAllAsync(spec);
            return _mapper.Map<IEnumerable<BrandTypeResponse>>(Types);
        }

        public async Task<ProductResponse> GetProductByIdAsync(int Id)
        {
            var spec = new ProductWithBrandAndTypeSpecification(Id);
            var product = await _unitOfWork.GetRepository<int, Product>().GetAsync(spec);
            if (product is null) throw new ProductNotFoundException(Id);
            return _mapper.Map<ProductResponse>(product);
        }
    }
}
