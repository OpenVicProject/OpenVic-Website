using GameDevPortal.Core.Entities;
using GameDevPortal.Core.Interfaces;
using GameDevPortal.Core.Interfaces.Repositories;
using GameDevPortal.Core.Models;
using GameDevPortal.Core.Specifications;
using Microsoft.Extensions.Logging;

namespace GameDevPortal.Core.Services.DomainServices;

public class FaqDomainService : IFaqDomainService
{
    private readonly IRepository _repository;
    private readonly ILogger _logger;

    public FaqDomainService(IRepository repository, ILogger logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<OperationResult<IEnumerable<Faq>>> List(Pagination pagination, CancellationToken cancellationToken = default)
    {
        try
        {
            ISpecification<Faq> specification = new BaseSpecification<Faq>(pagination);
            IEnumerable<Faq> categories = await _repository.List(specification, cancellationToken);
            return OperationResult<IEnumerable<Faq>>.CreateSuccessResult(categories);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return OperationResult<IEnumerable<Faq>>.CreateFailure(ex);
        }
    }

    public async Task<OperationResult> Insert(Faq faq, CancellationToken cancellationToken = default)
    {
        try
        {
            await _repository.Insert(faq, cancellationToken);
            return OperationResult.CreateSuccessResult();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return OperationResult.CreateFailure(ex);
        }
    }

    public async Task<OperationResult<Faq>> Get(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            Faq result = await _repository.Get<Faq>(id, cancellationToken);
            return OperationResult<Faq>.CreateSuccessResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return OperationResult<Faq>.CreateFailure(ex);
        }
    }

    public async Task<OperationResult> Update(Faq updateFaq, CancellationToken cancellationToken = default)
    {
        try
        {
            await _repository.Update(updateFaq, cancellationToken);
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
            await _repository.Delete<Faq>(id, cancellationToken);
            return OperationResult.CreateSuccessResult();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return OperationResult.CreateFailure(ex);
        }
    }
}