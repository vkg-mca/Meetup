using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meetup.Repository
{
    public interface IRepository<in TIdentity,TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllAsync()=>throw new NotImplementedException();
        Task<TEntity> GetAsync(TIdentity identity) => throw new NotImplementedException();
        Task CreateAsync(TEntity entity) => throw new NotImplementedException();
        Task DeleteAsync(TIdentity identity) => throw new NotImplementedException();
        Task UpdateAsync(TIdentity identity, TEntity entity) => throw new NotImplementedException();
    }
}
