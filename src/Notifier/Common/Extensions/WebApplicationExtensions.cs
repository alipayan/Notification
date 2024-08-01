namespace Notifier.Common.Extensions;

public static class WebApplicationExtensions
{

    public static void ConfigureBroker(this IHostApplicationBuilder builder)
    {

    }

    public static void ConfigureAppSettings(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<NotifierOptions>(builder.Configuration);
    }
}
