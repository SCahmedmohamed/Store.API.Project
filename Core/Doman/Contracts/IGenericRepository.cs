using Doman.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doman.Contracts
{
    public interface IGenericRepository<TKey,TEntity> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool changeTracker = false);
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TKey,TEntity> specifications ,bool changeTracker = false);
        Task<TEntity?> GetAsync(TKey key);
        Task<TEntity?> GetAsync(ISpecifications<TKey,TEntity> specifications);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        
    }
}
