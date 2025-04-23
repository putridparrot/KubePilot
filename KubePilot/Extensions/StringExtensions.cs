
namespace KubePilot.Extensions;

public static class StringExtensions
{
    public static bool HasValue(this string? str) =>
        !string.IsNullOrWhiteSpace(str);
    public static string NamespaceParameter(this string? ns) =>
            string.IsNullOrWhiteSpace(ns) ? "--all-namespaces" : $"-n {ns}";
}