namespace KubePilot.Components.ViewModels;

public class Selectable<T>(T item)
{
    public bool Selected { get; set; }
    public T Item { get; set; } = item;
}