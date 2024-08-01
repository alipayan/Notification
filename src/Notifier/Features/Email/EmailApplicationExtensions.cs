using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Notifier.Features.Email;

public static class EmailApplicationExtensions
{
    public const string ConnectionStringHost = "MongoSettings:Host";
    public const string ConnectionStringHostName = "MongoSettings:DatabaseName";

    public static void AddEmailFeature(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<EmailOptions>(builder.Configuration.GetSection(EmailOptions.SectionName));
        builder.Services.AddDbContext<EmailDbContext>(options =>
       {
           var hostName = builder.Configuration[ConnectionStringHost];
           if (hostName is null)
               throw new ArgumentNullException(hostName);
           var databaseName = builder.Configuration[ConnectionStringHostName];

           if (databaseName is null)
               throw new ArgumentNullException(databaseName);

           options.UseMongoDB(hostName, databaseName);
       });

        var port = Convert.ToInt32($"{EmailOptions.SectionName}:Port");
        var sender = builder.Configuration[$"{EmailOptions.SectionName}:Sender"];
        var mailServerHost = builder.Configuration[$"{EmailOptions.SectionName}:Host"];
        var mailServerUserName = builder.Configuration[$"{EmailOptions.SectionName}:UserName"];
        var mailServerPassword = builder.Configuration[$"{EmailOptions.SectionName}:Password"];

        builder.Services.AddFluentEmail(sender)
            .AddSmtpSender(mailServerHost, port, mailServerUserName, mailServerPassword);
    }
    public static void UseEmailFeature(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/email");
        group.MapPost("", async (EmailService emailService) =>
        {
            await emailService.SendAsync("alipayan88@gmail.com", "subject", "Hello dear ali");
        });

        group.MapGet("/tracking/{track_id}", async ([FromRoute(Name = "track_id")] string trackId, EmailService emailService) =>
        {
            //we can set counter to reach out how many times that email opened.
            await emailService.OpendedAsync(trackId);
        });

    }
}
