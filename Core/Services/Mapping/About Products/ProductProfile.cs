using AutoMapper;
using Doman.Entities.About_Product;
using Microsoft.Extensions.Configuration;
using Shared.About_Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mapping.About_Products
{
    public class ProductProfile : Profile
    {
        public ProductProfile(IConfiguration _configuration)
        {
            CreateMap<Product, ProductResponse>()
                .ForMember(D => D.Brand, O => O.MapFrom(S => S.Brand.Name))
                .ForMember(D => D.Type, O => O.MapFrom(S => S.Type.Name))
                .ForMember(D=> D.PictureUrl , O=> O.MapFrom(new ProductPictureUrlResolver(_configuration)));

            CreateMap<ProductBrand, BrandTypeResponse>();
            CreateMap<ProductType, BrandTypeResponse>();
        }
    }
}
