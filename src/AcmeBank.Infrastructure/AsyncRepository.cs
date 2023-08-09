using AcmeBank.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AcmeBank.Persistence
{
    public class AsyncRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : class
    {
        protected readonly AcmeBankDbContext _dbContext;

        public AsyncRepository(AcmeBankDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            // Filter by id
            var type = typeof(TEntity);
            var property = type.GetProperty("Id")!;
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);

            var expression =
                Expression.Lambda<Func<TEntity?, bool>>(Expression.Equal(propertyAccess, Expression.Constant(id)), parameter);

            return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(expression, cancellationToken);
        }

        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<TEntity>> ListAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Set<TEntity>().ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<TEntity>> ListAllAsync(
            int perPage,
            int page,
            CancellationToken cancellationToken)
        {
            return await _dbContext.Set<TEntity>().Skip(perPage * (page - 1)).Take(perPage).ToListAsync(cancellationToken);
        }

        public void Update(TEntity entity)
        {
            _dbContext.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Unchanged;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
