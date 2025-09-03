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
            await storeDbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            storeDbContext.Remove(entity);
            var result= await storeDbContext.SaveChangesAsync();
            if(result<0 ) return false;
            return true;
            
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
         return  await storeDbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(TKey id)
        {
           return await storeDbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task UpadateAsync(TEntity entity)
        {
            storeDbContext.Update(entity);
            await storeDbContext.SaveChangesAsync();
        }
    }
}
