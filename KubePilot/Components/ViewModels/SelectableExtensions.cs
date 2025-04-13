namespace KubePilot.Components.ViewModels;

public static class SelectableExtensions
{
    public static IList<Selectable<T>> ToSelectable<T>(this IEnumerable<T> e) =>
        e.Select(i => new Selectable<T>(i)).ToArray();

    public static IQueryable<Selectable<T>> ToSelectableQueryable<T>(this IEnumerable<T> e) =>
        e.Select(i => new Selectable<T>(i)).ToArray().AsQueryable();
}