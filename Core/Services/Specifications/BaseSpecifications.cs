using Doman.Contracts;
using Doman.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class BaseSpecifications<TKey, TEntity> : ISpecifications<TKey, TEntity> where TEntity : BaseEntity<TKey>
    {
        // List of navigation properties to include
        public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new List<Expression<Func<TEntity, object>>>();
            // Criteria for filtering
        public Expression<Func<TEntity, bool>>? Criteria { get ; set; }
        // Ordering expressions
        public Expression<Func<TEntity, object>>? OrderBy { get; set; } // Ascending order
        public Expression<Func<TEntity, object>>? OrderByDesc { get; set; } // Descending order
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool IsPagination { get; set; }


        // Constructor initializing the Includes list
        public BaseSpecifications(Expression<Func<TEntity,bool>>? expression)
            {
                Criteria = expression;
        }
     
        public void AddOrderBy(Expression<Func<TEntity, object>>? orderByExpression)
        {
            OrderBy = orderByExpression;
        }
        public void AddOrderByDesc(Expression<Func<TEntity, object>>? orderByDescExpression)
        {
            OrderByDesc = orderByDescExpression;
        }
        public void ApplyPagination(int PageSize, int PageIndex)
        {
            Skip = (PageIndex - 1) * PageSize;
            Take = PageSize;
            IsPagination = true;
        }

    }
}
