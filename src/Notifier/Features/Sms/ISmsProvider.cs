using Notifier.Features.Sms.Models;
using Notifier.Features.Sms.Providers;

namespace Notifier.Features.Sms;

public interface ISmsProvider
{
    public string Name { get; }
    Task<ProviderServiceResult> Send(string mobile, string message);

    Task<SmsTraceStatus> Inquiry(string inquiryId);
}
