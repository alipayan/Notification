namespace Notifier.Features.Sms.Providers;

public class ProviderSendSmsException : Exception
{
    private const string _message = "Provider{0} can't send sms";

    public ProviderSendSmsException(string providerName) : base(string.Format(_message, providerName)) { }
}
