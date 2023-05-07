using GameDevPortal.Core.Interfaces.Repositories;
using System.Linq.Expressions;

namespace GameDevPortal.Core.IncludeLists;

public class BaseIncludeList<T> : IIncludeList<T>
{
    public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
    public List<string> IncludeStrings { get; } = new List<string>();

    public BaseIncludeList() { }

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