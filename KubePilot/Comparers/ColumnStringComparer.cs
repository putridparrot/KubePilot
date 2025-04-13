namespace KubePilot.Comparers;

public class ColumnStringComparer : IComparer<string>
{
    public static readonly IComparer<string>? Instance = new ColumnStringComparer();

    public int Compare(string? x, string? y) =>
        string.CompareOrdinal(x, y);
}
