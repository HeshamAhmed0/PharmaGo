using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Moduls;
using Microsoft.EntityFrameworkCore;

namespace Presistance.Reposatories
{
    public class GenericReposatory<TEntity, TKey>(StoreDbContext storeDbContext) : IGenericReposatory<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public async Task AddAsync(TEntity entity)
        {
           await storeDbContext.AddAsync(entity);
        }

        public  void DeleteAsync(TEntity entity)
        {
            storeDbContext.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
         return  await storeDbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(TKey id)
        {
           return await storeDbContext.Set<TEntity>().FindAsync(id);
        }

        public void UpadateAsync(TEntity entity)
        {
            storeDbContext.Update(entity);
        }
    }
}
