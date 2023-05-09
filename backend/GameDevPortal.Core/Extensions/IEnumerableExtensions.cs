using System.Text;
using System.Text.RegularExpressions;

namespace GameDevPortal.Core.Extensions;

public static class IEnumerableExtensions
{
    public static void ThrowIfEmptyOrNull<T>(this IEnumerable<T> enumerable, string parameterName)
    {
        if (enumerable == null) throw new ArgumentNullException($"IEnumerable parameter {parameterName} is null.");
        if (!enumerable.Any()) throw new ArgumentException($"IEnumerable parameter {parameterName} is empty.");
    }

    public static void ThrowIfStrictSuperset<T>(this IEnumerable<T> superset, IEnumerable<T> subset, string itemName = "item")
    {
        if (subset.Count() != superset.Count())
        {
            List<T> missingItems = superset.Except(subset).ToList();

            StringBuilder sb = new();
            sb.Append($"\"{missingItems[0]}\"");
            for (int i = 1; i < missingItems.Count; i++)
            {
                sb.Append($", \"{missingItems[i]}\"");
            }

            throw new KeyNotFoundException($"The following {itemName}s were not found: {sb}");
        }
    }
}