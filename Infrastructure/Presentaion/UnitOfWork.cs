using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Moduls;
using Presistance.Reposatories;

namespace Presistance
{
    public class UnitOfWork : IUnitofwork
    {
        private readonly Dictionary<string, object> Reposatories;
        private readonly StoreDbContext storeDbContext;
        public UnitOfWork(StoreDbContext storeDbContext)
        {
            this.storeDbContext = storeDbContext;
            Reposatories = new Dictionary<string, object>();
        }

        public IGenericReposatory<TEntity, TKey> GenericReposatory<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var type = typeof(TEntity).Name;
            if (!Reposatories.ContainsKey(type))
            {
                var Reposatory = new GenericReposatory<TEntity, TKey>(storeDbContext);
                Reposatories.Add(type, Reposatory);
            }
            return (IGenericReposatory<TEntity, TKey>)Reposatories[type];
        }

        public async Task<int> SaveChangesAsync()
        {
           return  await storeDbContext.SaveChangesAsync();
        }
    }
}
