﻿namespace KubePilot.Extensions;

public static class EnumerableExtensions
{
    public static string? CsvJoin<T>(this IEnumerable<T>? e) => 
        e is null ? string.Empty : string.Join(", ", e);
}