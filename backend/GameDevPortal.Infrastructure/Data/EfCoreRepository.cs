using GameDevPortal.Core.Entities;
using GameDevPortal.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GameDevPortal.Infrastructure.Data;

public class EfCoreRepository : IRepository
{
    private readonly ProjectContext _context;

    public EfCoreRepository(ProjectContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> List<T>(CancellationToken cancellationToken = default) where T : Entity
    {
        return await _context.Set<T>().OrderBy(t => t.Id).ToListAsync(cancellationToken);
    }

    // https://github.com/dotnet-architecture/eShopOnWeb
    public async Task<IEnumerable<T>> List<T>(ISpecification<T> spec, CancellationToken cancellationToken = default) where T : Entity
    {
        // fetch a Queryable that includes all expression-based includes
        var queryableResultWithIncludes = spec.Includes.Aggregate(_context.Set<T>().AsQueryable(), (current, include) => current.Include(include));

        // modify the IQueryable to include any string-based include statements
        var secondaryResult = spec.IncludeStrings.Aggregate(queryableResultWithIncludes, (current, include) => current.Include(include));

        // return the result of the query using the specification's criteria expression
        return await secondaryResult.Where(spec.Criteria).OrderBy(t => t.Id).Skip(spec.PageSize * spec.PageIndex).Take(spec.PageSize).ToListAsync(cancellationToken);
    }

    public async Task Insert<T>(T entity, CancellationToken cancellationToken = default) where T : Entity
    {
        await _context.Set<T>().AddAsync(entity, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<T> Get<T>(Guid id, CancellationToken cancellationToken = default) where T : Entity
    {
        return await _context.Set<T>().FindAsync(id, cancellationToken) ?? throw new KeyNotFoundException(id.ToString());
    }

    public async Task<T> Get<T>(Guid id, IIncludeList<T> includeList, CancellationToken cancellationToken = default) where T : Entity
    {
        var queryableResultWithIncludes = includeList.Includes.Aggregate(_context.Set<T>().AsQueryable(), (current, include) => current.Include(include));

        // modify the IQueryable to include any string-based include statements
        var secondaryResult = includeList.IncludeStrings.Aggregate(queryableResultWithIncludes, (current, include) => current.Include(include));

        // return the result of the query using the specification's criteria expression
        return await secondaryResult.SingleAsync(t => t.Id == id, cancellationToken) ?? throw new KeyNotFoundException(id.ToString());
    }

    public async Task Update<T>(T entity, CancellationToken cancellationToken = default) where T : Entity
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete<T>(Guid id, CancellationToken cancellationToken = default) where T : Entity
    {
        _context.Set<T>().Remove(await _context.Set<T>().FindAsync(id, cancellationToken) ?? throw new KeyNotFoundException());
        await _context.SaveChangesAsync(cancellationToken);
    }
}