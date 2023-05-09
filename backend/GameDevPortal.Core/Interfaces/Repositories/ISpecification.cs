using System.Linq.Expressions;

namespace GameDevPortal.Core.Interfaces.Repositories;

public interface ISpecification<T>
{
    int PageSize { get; }
    int PageIndex { get; }

    Expression<Func<T, bool>> Criteria { get; }
    List<Expression<Func<T, object>>> Includes { get; }
    List<string> IncludeStrings { get; }
}