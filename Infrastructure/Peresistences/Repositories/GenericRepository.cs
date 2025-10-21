using Doman.Contracts;
using Doman.Entities;
using Doman.Entities.About_Product;
using Microsoft.EntityFrameworkCore;
using Peresistences.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peresistences.Repositories
{
    public class GenericRepository<TKey, TEntity>(StoreDbContext _context) : IGenericRepository<TKey, TEntity> where TEntity : BaseEntity<TKey>
    {
        public async Task AddAsync(TEntity entity)
        {
           await _context.Set<TEntity>().AddAsync(entity);
        }

        public  void Delete(TEntity entity)
        {
             _context.Set<TEntity>().Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool changeTracker = false)
        {
            if(typeof(TEntity) == typeof(Product))
            {
                return changeTracker ?
                  await _context.Products.Include(d=>d.Brand).Include(P=>P.Type).ToListAsync() as IEnumerable<TEntity>
                : await _context.Products.Include(d => d.Brand).Include(P => P.Type).AsNoTracking().ToListAsync() as IEnumerable<TEntity>;
            }
            return changeTracker ?
                  await _context.Set<TEntity>().ToListAsync()
                : await _context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity?> GetAsync(TKey key)
        {
            if(typeof(TEntity) == typeof(Product)){
                return await _context.Products.Include(P=>P.Brand).Include(P=>P.Type).FirstOrDefaultAsync(P=>P.Id == key as int?) as TEntity;
            }
            return await _context.Set<TEntity>().FindAsync(key);
        }

        public void Update(TEntity entity)
        {
             _context.Set<TEntity>().Update(entity);
        }
    }
}
