using AutoMapper;
using Doman.Contracts;
using Services.About_Products;
using Services.Abstractions;
using Services.Abstractions.About_Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager(IUnitOfWork _unitOfWork , IMapper _mapper) : IServiceManager
    {
        public IProductService ProductService { get; } = new ProductService(_unitOfWork,_mapper);
    }
}
