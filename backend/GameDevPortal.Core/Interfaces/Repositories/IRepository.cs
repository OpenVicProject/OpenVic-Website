using GameDevPortal.Core.Entities;

namespace GameDevPortal.Core.Interfaces.Repositories;

public interface IRepository
{
    public Task<IEnumerable<T>> List<T>(CancellationToken cancellationToken = default) where T : Entity;

    public Task<IEnumerable<T>> List<T>(ISpecification<T> spec, CancellationToken cancellationToken = default) where T : Entity;

    public Task Insert<T>(T entity, CancellationToken cancellationToken = default) where T : Entity;

    public Task<T> Get<T>(Guid id, CancellationToken cancellationToken = default) where T : Entity;

    public Task<T> Get<T>(Guid id, IIncludeList<T> includeList, CancellationToken cancellationToken = default) where T : Entity;

    public Task Update<T>(T entity, CancellationToken cancellationToken = default) where T : Entity;

    public Task Delete<T>(Guid id, CancellationToken cancellationToken = default) where T : Entity;
}