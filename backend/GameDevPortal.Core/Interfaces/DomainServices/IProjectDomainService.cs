using GameDevPortal.Core.Entities;
using GameDevPortal.Core.Interfaces.DomainServices;
using GameDevPortal.Core.Models;

namespace GameDevPortal.Core.Interfaces;

public interface IProjectDomainService : ICommonOperations<Project>
{
    Task<OperationResult<IEnumerable<ProgressReport>>> ListProgressReports(Guid id, Pagination pagination, CancellationToken cancellationToken = default);
    Task<OperationResult<IEnumerable<Category>>> ListCategories(Guid id, Pagination pagination, CancellationToken cancellationToken = default);
}
