using Microsoft.Extensions.Logging;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components.Components.Tooltip;
using KubePilot.Services;

namespace KubePilot;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();
        builder.Services.AddScoped(sp => new HttpClient()/* { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) }*/);
        builder.Services.AddFluentUIComponents();

        builder.Services.AddScoped<IClipboardService, ClipboardService>();
        builder.Services.AddScoped<ITooltipService, TooltipService>();
        builder.Services.AddScoped<IKubernetesClientService>(_ =>
        {
            var client = new KubernetesClientService();
            client.LoadKubeConfig();
            client.LoadConfig(null, null);
            return client;
        });

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
