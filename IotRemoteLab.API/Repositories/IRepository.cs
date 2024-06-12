namespace IotRemoteLab.API.Repositories;

public interface IRepository<TEntity>
{
    Task<Result<TEntity?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<Result<TEntity>> CreateOrUpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}