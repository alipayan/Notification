using Kavenegar;
using Microsoft.Extensions.Options;
using Notifier.Features.Sms.Models;

namespace Notifier.Features.Sms.Providers;

public class MeliPaymakProvider
    (IOptions<SmsOptions> options,
    ILogger<MeliPaymakProvider> logger)
    : ISmsProvider
{
    private readonly MeliPaymakOptions _options = options.Value.MeliPaymak;
    private readonly ILogger<MeliPaymakProvider> _logger = logger;

    private readonly static List<int> SuccessSendStatus = [1, 2, 4, 5, 10];
    public string Name => nameof(MeliPaymakProvider);

    public async Task<ProviderServiceResult> Send(string mobile, string message)
    {
        try
        {
            var api = new KavenegarApi(_options.ApiKey);

            var result = await api.Send(_options.SenderNumber, mobile, message);

            if (SuccessSendStatus.Exists(x => result.Status == x))
                return new ProviderServiceResult(true, result.Messageid.ToString());

            throw new ProviderSendSmsException(Name);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Kavenegar exception on sending sms.");
            return new ProviderServiceResult(false, string.Empty);
        }
    }

    public async Task<SmsTraceStatus> Inquiry(string inquiryId)
    {
        //we have to check count of inquiry to do not keep in loop of inquiry

        var api = new KavenegarApi(_options.ApiKey);
        var inquiryResult = await api.Status(inquiryId);

        if (inquiryResult.Status == Kavenegar.Core.Models.Enums.MessageStatus.Delivered)
            return SmsTraceStatus.Success;

        return SmsTraceStatus.Inquiry;
    }
}
