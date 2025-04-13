
namespace KubePilot.Services;

public class ClipboardService : IClipboardService
{
    public async Task ToClipboardAsync(string value)
    {
        await Clipboard.SetTextAsync(value);
    }
}