using System.Linq.Expressions;

namespace GameDevPortal.Core.Interfaces.Repositories;

public interface IIncludeList<T>
{
    List<Expression<Func<T, object>>> Includes { get; }
    List<string> IncludeStrings { get; }
}