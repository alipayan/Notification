using FluentEmail.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Notifier.Features.Email.Models;

namespace Notifier.Features.Email;

public class EmailService(
    IOptions<EmailOptions> options,
    ILogger<EmailService> logger,
    EmailDbContext emailDbContext,
    IFluentEmail fluentEmail)
{
    private readonly EmailOptions _options = options.Value;
    private readonly IFluentEmail _fluentEmail = fluentEmail;
    private readonly ILogger<EmailService> _logger = logger;
    private readonly EmailDbContext _dbContext = emailDbContext;

    public async Task<bool> SendAsync(string emailAddress, string subject, string body)
    {
        //we inject our endpoint route for enable ability of tracking email which if opened by user or not
        var trackId = Guid.NewGuid().ToString();
        var trackingUrl = $"{_options.TrackingUrl}/{trackId}";
        body = body + $"<img src=\"{trackingUrl}\" height='1px' width='1px'/>";

        var email = _fluentEmail.To(emailAddress)
            .Subject(subject)
            .Body(body, true);
        try
        {
            var response = await email.SendAsync();
            if (response.Successful)
            {
                var trace = EmailTrace
                    .Create(_options.Sender, response.MessageId, emailAddress, subject, body);

                _dbContext.EmailTraces.Add(trace);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            throw new Exception(string.Join(",", response.ErrorMessages));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "failed to send email");
            return false;

        }
    }

    public async Task OpendedAsync(string trackId)
    {
        var track = await _dbContext.EmailTraces.FirstOrDefaultAsync(x => x.TrackId == trackId);

        if (track is null)
            throw new Exception($"Failed to open {trackId}");

        track.Status = EmailTraceStatus.Opend;
        await _dbContext.SaveChangesAsync();
    }
}
