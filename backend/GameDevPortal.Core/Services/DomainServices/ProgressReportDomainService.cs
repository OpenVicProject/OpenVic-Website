using GameDevPortal.Core.Entities;
using GameDevPortal.Core.Extensions;
using GameDevPortal.Core.IncludeLists;
using GameDevPortal.Core.Interfaces;
using GameDevPortal.Core.Interfaces.Repositories;
using GameDevPortal.Core.Models;
using GameDevPortal.Core.Specifications;
using Microsoft.Extensions.Logging;
using System.Text;

namespace GameDevPortal.Core.Services.DomainServices;

public class ProgressReportDomainService : IProgressReportDomainService
{
    private readonly IRepository _repository;
    private readonly ILogger _logger;

    public ProgressReportDomainService(IRepository repository, ILogger logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<OperationResult<IEnumerable<ProgressReport>>> List(Pagination pagination, CancellationToken cancellationToken = default)
    {
        try
        {
            ISpecification<ProgressReport> specification = new FullProgressReportSpecification(pagination);
            IEnumerable<ProgressReport> progressReports = await _repository.List(specification, cancellationToken);
            return OperationResult<IEnumerable<ProgressReport>>.CreateSuccessResult(progressReports);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return OperationResult<IEnumerable<ProgressReport>>.CreateFailure(ex);
        }
    }

    public async Task<OperationResult> Insert(ProgressReport progressReport, CancellationToken cancellationToken = default)
    {
        try
        {
            BaseSpecification<Category> specification = new CategoriesForProjectInSet(progressReport.ProjectId, progressReport.CategoryIds.ToHashSet());
            IEnumerable<Category> categories = await _repository.List(specification, cancellationToken);

            progressReport.CategoryIds.ThrowIfStrictSuperset(categories.Select(c => c.Id), "category ID");

            progressReport.AddCategories(categories);

            await _repository.Insert(progressReport, cancellationToken);
            return OperationResult.CreateSuccessResult();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return OperationResult.CreateFailure(ex);
        }
    }

    public async Task<OperationResult<ProgressReport>> Get(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            FullProgressReportIncludeList includeList = new();
            ProgressReport result = await _repository.Get(id, includeList, cancellationToken);
            result.SyncCategoryIdsToCategories();

            return OperationResult<ProgressReport>.CreateSuccessResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return OperationResult<ProgressReport>.CreateFailure(ex);
        }
    }

    public async Task<OperationResult> Update(ProgressReport updateProgressReport, CancellationToken cancellationToken = default)
    {
        try
        {
            updateProgressReport.ClearCategories();

            BaseSpecification<Category> specification = new CategoriesForProjectInSet(updateProgressReport.ProjectId, updateProgressReport.CategoryIds.ToHashSet());
            IEnumerable<Category> categories = await _repository.List(specification, cancellationToken);

            updateProgressReport.CategoryIds.ThrowIfStrictSuperset(categories.Select(c => c.Id), "category ID");

            updateProgressReport.AddCategories(categories);

            await _repository.Update(updateProgressReport, cancellationToken);
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
            await _repository.Delete<ProgressReport>(id, cancellationToken);
            return OperationResult.CreateSuccessResult();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return OperationResult.CreateFailure(ex);
        }
    }
}