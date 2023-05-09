using GameDevPortal.Core.Entities;
using GameDevPortal.Core.Interfaces;
using GameDevPortal.Core.Interfaces.Notifications;
using GameDevPortal.Core.Interfaces.Repositories;
using GameDevPortal.Core.Models;
using GameDevPortal.Core.Models.Notifications;
using GameDevPortal.Core.Models.Notifications.NotificationData;
using GameDevPortal.Core.Specifications;
using Microsoft.Extensions.Logging;

namespace GameDevPortal.Core.Services.DomainServices;

public class ProjectDomainService : IProjectDomainService
{
    private readonly IRepository _repository;
    private readonly ILogger _logger;

    public ProjectDomainService(IRepository repository, ILogger logger, ITemplateProvider templateProvider)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<OperationResult<IEnumerable<Project>>> List(Pagination pagination, CancellationToken cancellationToken = default)
    {
        try
        {
            ISpecification<Project> specification = new BaseSpecification<Project>(pagination);
            IEnumerable<Project> projects = await _repository.List(specification, cancellationToken);
            return OperationResult<IEnumerable<Project>>.CreateSuccessResult(projects);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return OperationResult<IEnumerable<Project>>.CreateFailure(ex);
        }
    }

    public async Task<OperationResult> Insert(Project project, CancellationToken cancellationToken = default)
    {
        try
        {
            await _repository.Insert(project, cancellationToken);
            return OperationResult.CreateSuccessResult();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return OperationResult.CreateFailure(ex);
        }
    }

    public async Task<OperationResult<Project>> Get(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            Project result = await _repository.Get<Project>(id, cancellationToken);
            return OperationResult<Project>.CreateSuccessResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return OperationResult<Project>.CreateFailure(ex);
        }
    }

    public async Task<OperationResult> Update(Project updateProject, CancellationToken cancellationToken = default)
    {
        try
        {
            await _repository.Update(updateProject, cancellationToken);
            return OperationResult.CreateSuccessResult();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return OperationResult.CreateFailure(ex);
        }
    }

    public async Task<OperationResult> Delete(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            await _repository.Delete<Project>(id, cancellationToken);
            return OperationResult.CreateSuccessResult();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return OperationResult.CreateFailure(ex);
        }
    }

    public async Task<OperationResult<IEnumerable<ProgressReport>>> ListProgressReports(Guid id, Pagination pagination, CancellationToken cancellationToken = default)
    {
        try
        {
            ISpecification<ProgressReport> specification = new FullProgressReportSpecification(pr => pr.ProjectId == id, pagination);
            IEnumerable<ProgressReport> result = await _repository.List(specification, cancellationToken);
            return OperationResult<IEnumerable<ProgressReport>>.CreateSuccessResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return OperationResult<IEnumerable<ProgressReport>>.CreateFailure(ex);
        }
    }

    public async Task<OperationResult<IEnumerable<Category>>> ListCategories(Guid id, Pagination pagination, CancellationToken cancellationToken = default)
    {
        try
        {
            ISpecification<Category> specification = new BaseSpecification<Category>(c => c.ProjectId == null || c.ProjectId == id, pagination);
            IEnumerable<Category> result = await _repository.List(specification, cancellationToken);
            return OperationResult<IEnumerable<Category>>.CreateSuccessResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return OperationResult<IEnumerable<Category>>.CreateFailure(ex);
        }
    }
}