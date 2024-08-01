using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Notifier.Features.Sms;

public class SmsInquiryBackgroundService
    : BackgroundService
{

    private readonly ILogger<SmsInquiryBackgroundService> _logger;
    private readonly SmsDbContext _smsDbContext;
    private readonly SmsService _smsService;
    private readonly SmsOptions _options;

    public SmsInquiryBackgroundService(ILogger<SmsInquiryBackgroundService> logger,
    IServiceScopeFactory serviceScopeFactory,
    IOptions<SmsOptions> options)
    {
        _logger = logger;
        _options = options.Value;
        var scope = serviceScopeFactory.CreateScope();
        _smsDbContext = scope.ServiceProvider.GetRequiredService<SmsDbContext>();
        _smsService = scope.ServiceProvider.GetRequiredService<SmsService>();
    }

    private async Task Inquiring()
    {
        var requiredInquiries = await _smsDbContext.SmsTraces
                    .Where(x => x.Status == Models.SmsTraceStatus.Inquiry)
                    .ToListAsync();
        foreach (var trace in requiredInquiries)
        {
            trace.Status = await _smsService.Inquiry(trace);
        }
        await _smsDbContext.SaveChangesAsync();
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Sms inquiry job in up .");
        while (true)
        {
            await Inquiring();
            await Task.Delay(_options.InquiryInSeconds * 1000);
        }
    }
}
