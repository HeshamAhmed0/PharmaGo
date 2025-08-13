using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Moduls;

namespace Domain.Contracts
{
    public interface IUnitofwork
    {
        public Task<int> SaveChangesAsync();
        public IGenericReposatory<TEntity, TKey> GenericReposatory<TEntity, TKey>() where TEntity : BaseEntity<TKey>; 
    }
}
