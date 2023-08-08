namespace AcmeBank.Contracts
{
    public interface IAsyncRepository<TEntity> where TEntity : class
    {
        Task<IReadOnlyList<TEntity>> ListAllAsync(CancellationToken cancellationToken);

        Task<IReadOnlyList<TEntity>> ListAllAsync(int perPage, int page, CancellationToken cancellationToken);

        void Add(TEntity entity);

        void Update(TEntity entity);
        void Delete(TEntity entity);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
