using Notifier.Features.Sms.Models;

namespace Notifier.Features.Sms;

public class SmsService(
    IEnumerable<ISmsProvider> smsProvider,
    SmsDbContext smsDbContext)
{
    private readonly IEnumerable<ISmsProvider> _smsProvider = smsProvider;
    private readonly SmsDbContext _smsDbContext = smsDbContext;

    public async Task<bool> Send(string mobile, string message)
    {
        foreach (var provider in _smsProvider)
        {
            var result = await provider.Send(mobile, message);
            if (!result.IsSuccess)
                continue;

            _smsDbContext.Add(SmsTrace.Create(provider.Name, message, mobile, result.InquiryId));
            await _smsDbContext.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<SmsTraceStatus> Inquiry(SmsTrace trace)
    {
        var provider = _smsProvider.FirstOrDefault(x => x.Name == trace.ProviderName);

        if (provider is null)
            throw new InvalidProviderOnInquiryException();

        return await provider.Inquiry(trace.InquiryId);


    }
}
