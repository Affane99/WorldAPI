using Microsoft.EntityFrameworkCore;
using World.Application.Contracts.Persistence;

namespace World.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly WorldManagementDbContext _dbContext;

        public GenericRepository(WorldManagementDbContext worldManagementDbContext)
        {
            _dbContext = worldManagementDbContext;
        }
        public async Task<T> CreateAsync(T entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(System.Linq.Expressions.Expression<Func<T, bool>> whereExpression)
        {
            return await _dbContext.Set<T>().AnyAsync(whereExpression);
        }

        public IQueryable<T> FilterQuery(IQueryable<T> query, System.Linq.Expressions.Expression<Func<T, bool>> whereExpression)
        {
            return query.Where(whereExpression);
        }

        public async Task<IReadOnlyList<T>> FindAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> FindByIdAsync(Guid id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public IQueryable<T> GetQuery()
        {
            return _dbContext.Set<T>().AsQueryable();
        }

        public IQueryable<T> GetQuery(string linkedElements)
        {
            string[] splited = linkedElements.Split(',');

            IQueryable<T> query = _dbContext.Set<T>().AsQueryable();

            foreach (string element in splited)
            {
                query = query.Include(element);
            }

            return query;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
