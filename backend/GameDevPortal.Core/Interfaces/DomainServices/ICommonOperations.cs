using GameDevPortal.Core.Models;

namespace GameDevPortal.Core.Interfaces.DomainServices;

public interface ICommonOperations<TEntity>
{
    public Task<OperationResult<IEnumerable<TEntity>>> List(Pagination pagination, CancellationToken cancellationToken = default);

    public Task<OperationResult> Insert(TEntity entity, CancellationToken cancellationToken = default);

    public Task<OperationResult<TEntity>> Get(Guid id, CancellationToken cancellationToken = default);

    public Task<OperationResult> Update(TEntity entity, CancellationToken cancellationToken = default);

    public Task<OperationResult> Delete(Guid id, CancellationToken cancellationToken = default);
}