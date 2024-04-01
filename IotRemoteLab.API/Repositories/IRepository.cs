namespace IotRemoteLab.API.Repositories;

public interface IRepository<TEntity>
{
    Task<TEntity?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}