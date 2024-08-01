using Microsoft.EntityFrameworkCore;
using Notifier.Features.Sms.Providers;

namespace Notifier.Features.Sms;

public static class SmsApplicationExtensions
{
    public const string ConnectionStringHost = "MongoSettings:Host";
    public const string ConnectionStringHostName = "MongoSettings:DatabaseName";

    public static void AddSmsFeature(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<SmsService>();
        builder.Services.AddScoped<ISmsProvider, MeliPaymakProvider>();
        builder.Services.AddScoped<ISmsProvider, KavenegarProvider>();
        builder.Services.Configure<SmsOptions>(builder.Configuration.GetSection(SmsOptions.SectionName));
        builder.Services.AddHostedService<SmsInquiryBackgroundService>();
        builder.Services.AddDbContext<SmsDbContext>(options =>
        {
            var hostName = builder.Configuration[ConnectionStringHost];
            if (hostName is null)
                throw new ArgumentNullException(hostName);
            var databaseName = builder.Configuration[ConnectionStringHostName];

            if (databaseName is null)
                throw new ArgumentNullException(databaseName);


            options.UseMongoDB(hostName, databaseName);
        });
    }

    public static void UseSmsFeature(this IEndpointRouteBuilder app)
    {
        app.MapPost("/sms", async (SmsService smsService) =>
        {
            var result = await smsService.Send("09131815446", "Hello");
        });
    }
}
