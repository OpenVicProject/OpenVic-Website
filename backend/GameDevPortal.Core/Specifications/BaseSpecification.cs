using System.Linq.Expressions;
using GameDevPortal.Core.Interfaces.Repositories;
using GameDevPortal.Core.Models;

namespace GameDevPortal.Core.Specifications;

public class BaseSpecification<T> : ISpecification<T>
{
    public int PageSize { get; private set; }
    public int PageIndex { get; private set; }

    public BaseSpecification(Expression<Func<T, bool>> criteria, Pagination pagination)
    {
        Criteria = criteria;
        PageSize = pagination.Size;
        PageIndex = pagination.Index;
    }

    public BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
        PageSize = int.MaxValue;
        PageIndex = 0;
    }

    public BaseSpecification(Pagination pagination)
    {
        Criteria = _ => true;
        PageSize = pagination.Size;
        PageIndex = pagination.Index;
    }

    public Expression<Func<T, bool>> Criteria { get; }
    public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
    public List<string> IncludeStrings { get; } = new List<string>();

    protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }

    // string-based includes allow for including children of children, e.g. Basket.Items.Product
    protected virtual void AddInclude(string includeString)
    {
        IncludeStrings.Add(includeString);
    }
}