using Doman.Entities.About_Product;
using Shared.About_Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications.About_Product
{ 
    public class ProductWithBrandAndTypeSpecification : BaseSpecifications<int,Product>
    {

        public ProductWithBrandAndTypeSpecification(int id) : base(p=>p.Id == id)
        {
            ApplyIncludes();
        }
 
        public ProductWithBrandAndTypeSpecification(ProductQueryParams Params) : base
            ( 
                p=>
                (Params.BrandId.HasValue ? p.BrandId == Params.BrandId.Value : true) &&
                (Params.TypeId.HasValue ? p.TypeId == Params.TypeId.Value : true) &&
                (!String.IsNullOrEmpty(Params.Search) ? p.Name.ToLower().Contains(Params.Search.ToLower()) : true)
            )
        {

            if(!String.IsNullOrEmpty(Params.Sorting))
            {
                switch (Params.Sorting.ToLower())
                {
                    case "priceasc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "pricedesc":
                        AddOrderByDesc(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(P => P.Name);
            }
            ApplyPagination(Params.PageSize, Params.PageIndex);
            ApplyIncludes();
        }
        
        
        private void ApplyIncludes()
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Type);
        }
        

    }
}
