using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace World.Application.Contracts.Persistence
{
    public interface IGenericRepository <T> where T : class
    {
        Task<IReadOnlyList<T>> FindAllAsync();
        Task<T> FindByIdAsync(Guid id);
        Task<T> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> whereExpression);
        IQueryable<T> GetQuery();
        IQueryable<T> GetQuery(string linkedElements);
        IQueryable<T> FilterQuery(IQueryable<T> query, Expression<Func<T, bool>> whereExpression);
    }
}
