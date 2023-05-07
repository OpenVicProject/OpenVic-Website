using System.Linq.Expressions;
using System.Text;
using System;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GameDevPortal.Core.Extensions;

public static partial class StringExtensions
{
    public static void ThrowIfEmptyOrNull(this string str, string parameterName)
    {
        str = (str ?? string.Empty).Trim();
        if (str.Length == 0) throw new ArgumentException($"String parameter {parameterName} is empty.");
    }

    public static bool IsEmptyContent(this string str)
    {
        return string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str);
    }

    public static string FillTemplate(this string str, params Expression<Func<string, object>>[] args)
    {
        var parameters = args.ToDictionary(e => $"{{{e.Parameters[0].Name}}}", e => e.Compile()(e.Parameters[0].Name));

        var sb = new StringBuilder(str);
        foreach (var parameter in parameters)
        {
            sb.Replace(parameter.Key, parameter.Value != null ? parameter.Value.ToString() : string.Empty);
        }

        return sb.ToString();
    }

    public static void ThrowIfNotHex(this string str, string parameterName)
    {
        Regex rx = HexRegex();

        if (!rx.Match(str).Success) throw new ArgumentException($"Hex string {parameterName} is not a valid hexadecimal number.");
    }

    [GeneratedRegex("#[A-F0-9]{6}")]
    private static partial Regex HexRegex();
}