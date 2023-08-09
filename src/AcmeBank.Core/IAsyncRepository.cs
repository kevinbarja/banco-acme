namespace AcmeBank.Contracts
{
    public interface IAsyncRepository<TEntity> where TEntity : class
    {
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);

        Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);

        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);

        Task<IReadOnlyList<TEntity>> ListAllAsync(CancellationToken cancellationToken);

        Task<IReadOnlyList<TEntity>> ListAllAsync(int perPage, int page, CancellationToken cancellationToken);

        void Update(TEntity entity);
        void Delete(TEntity entity);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
