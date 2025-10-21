using Doman.Contracts;
using Doman.Entities;
using Peresistences.Data.Contexts;
using Peresistences.Repositories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peresistences
{
    public class UnitOfWork(StoreDbContext _context) : IUnitOfWork
    {
        private ConcurrentDictionary<string, object> _repositories = new ConcurrentDictionary<string, object>();
        public IGenericRepository<TKey, TEntity> GetRepository<TKey, TEntity>() where TEntity : BaseEntity<TKey>
        {
            //var type = typeof(TEntity).Name;
            //if (_repositories.ContainsKey(type))
            //{
            //    var repository = new GenericRepository<TKey, TEntity>(_context);
            //    _repositories.Add(type, repository);
            //}
            //    return _repositories[type] as IGenericRepository<TKey, TEntity>;
           return (IGenericRepository < TKey, TEntity >) _repositories.GetOrAdd(typeof(TEntity).Name, new GenericRepository<TKey, TEntity>(_context));
        }

        public async Task<int> SaveChangesAsync()
        {
           return await _context.SaveChangesAsync();
        }
    }
}
