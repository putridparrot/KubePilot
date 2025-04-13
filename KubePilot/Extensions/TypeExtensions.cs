namespace KubePilot.Extensions;

public static class TypeExtensions
{
    public static string? ToBoolean(this bool? b) =>
        b is null ? false.ToString(): b.ToString();

}