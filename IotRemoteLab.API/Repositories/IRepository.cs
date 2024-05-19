namespace IotRemoteLab.API.Repositories;

public interface IRepository<TEntity>
{
    Task<Result<TEntity?>> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<bool>> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<Result<TEntity>> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<TEntity>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<Result<TEntity>> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}