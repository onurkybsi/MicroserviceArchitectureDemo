using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KybInfrastructure.Data
{
    public interface IEntityRepository<T> where T : IEntity
    {
        Task<T> Get(Expression<Func<T, bool>> filter);
        Task<List<T>> GetList(Expression<Func<T, bool>> filter = null);
        Task Create(T entity);
        Task Update(T entity);
        Task FindAndUpdate(Expression<Func<T, bool>> filterDefinition, Action<T> updateDefinition);
        Task Remove(T entity);
    }
}