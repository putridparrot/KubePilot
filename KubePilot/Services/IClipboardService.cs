namespace KubePilot.Services;

public interface IClipboardService
{
    Task ToClipboardAsync(string value);
}