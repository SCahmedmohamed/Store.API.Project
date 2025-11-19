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
    public class ProductPictureUrlResolver(IConfiguration _configuration) : IValueResolver<Product, ProductResponse, string>
    {
        public string Resolve(Product source, ProductResponse destination, string destMember, ResolutionContext context)
        {
            return $"{_configuration["BaseUrl"]}/{source.PictureUrl}";
        }
    }
}
