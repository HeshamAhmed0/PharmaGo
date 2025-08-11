using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Moduls;

namespace Domain.Contracts
{
    public interface IGenericReposatory<TEntity,TKey> where TEntity :BaseEntity<TKey>
    {
        public Task AddAsync(TEntity entity);
        public Task<IEnumerable<TEntity>> GetAllAsync();
        public Task<TEntity> GetByIdAsync(TKey id);
        public void UpadateAsync(TEntity entity);
        public void DeleteAsync(TEntity entity);

    }
}
