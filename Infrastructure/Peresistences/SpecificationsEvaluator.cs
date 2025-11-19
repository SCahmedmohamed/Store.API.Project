using Doman.Contracts;
using Doman.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Peresistences
{
    public static class SpecificationsEvaluator 
    {
        // Generate Dynamic Query
        public static IQueryable<TEntity> GetQuery<TKey, TEntity>(IQueryable<TEntity> inputQuery, ISpecifications<TKey, TEntity> spec) where TEntity : BaseEntity<TKey>
        {
            var query = inputQuery; // _context.Set<TEntity>()

            if(spec.Criteria is not null)
            {
                // Apply filtering criteria
                query = query.Where(spec.Criteria);
            }

            // Apply ordering if specified
            if (spec.OrderBy is not null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            else if (spec.OrderByDesc is not null)
            {
                query = query.OrderByDescending(spec.OrderByDesc);
            }

            if(spec.IsPagination)
            {
                // Apply pagination
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            // Apply includes for navigation properties
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }
}
