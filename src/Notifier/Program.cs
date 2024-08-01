using Notifier.Common.Extensions;
using Notifier.Features.Email;
using Notifier.Features.Sms;

var builder = WebApplication.CreateBuilder(args);

#region Configurations

builder.ConfigureBroker();
builder.ConfigureAppSettings();
builder.AddSmsFeature();
builder.AddEmailFeature();

#endregion

var app = builder.Build();

#region Endpoint

app.UseSmsFeature();
app.UseEmailFeature();

#endregion

app.UseHttpsRedirection();

app.Run();
