using GameDevPortal.Core.Entities;
using GameDevPortal.Core.Interfaces;
using GameDevPortal.Core.Interfaces.Repositories;
using GameDevPortal.Core.Models;
using GameDevPortal.Core.Specifications;
using Microsoft.Extensions.Logging;

namespace GameDevPortal.Core.Services.DomainServices;

public class CategoryDomainService : ICategoryDomainService
{
    private readonly IRepository _repository;
    private readonly ILogger _logger;

    public CategoryDomainService(IRepository repository, ILogger logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<OperationResult<IEnumerable<Category>>> List(Pagination pagination, CancellationToken cancellationToken = default)
    {
        try
        {
            ISpecification<Category> specification = new BaseSpecification<Category>(pagination);
            IEnumerable<Category> categories = await _repository.List(specification, cancellationToken);
            return OperationResult<IEnumerable<Category>>.CreateSuccessResult(categories);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return OperationResult<IEnumerable<Category>>.CreateFailure(ex);
        }
    }

    public async Task<OperationResult> Insert(Category category, CancellationToken cancellationToken = default)
    {
        try
        {
            await _repository.Insert(category, cancellationToken);
            return OperationResult.CreateSuccessResult();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return OperationResult.CreateFailure(ex);
        }
    }

    public async Task<OperationResult<Category>> Get(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            Category result = await _repository.Get<Category>(id, cancellationToken);
            return OperationResult<Category>.CreateSuccessResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return OperationResult<Category>.CreateFailure(ex);
        }
    }

    public async Task<OperationResult> Update(Category updateCategory, CancellationToken cancellationToken = default)
    {
        try
        {
            await _repository.Update(updateCategory, cancellationToken);
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
            await _repository.Delete<Category>(id, cancellationToken);
            return OperationResult.CreateSuccessResult();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return OperationResult.CreateFailure(ex);
        }
    }
}