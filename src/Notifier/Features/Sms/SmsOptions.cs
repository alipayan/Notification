namespace Notifier.Features.Sms;

public class SmsOptions
{
    public const string SectionName = "Features:Sms";
    public int InquiryInSeconds { get; set; }
    public KavenegarOptions Kavenegar { get; set; }
    public MeliPaymakOptions MeliPaymak { get; set; }

}

public class KavenegarOptions
{
    public string ApiKey { get; set; }

    public string SenderNumber { get; set; }
}
public class MeliPaymakOptions
{
    public string ApiKey { get; set; }

    public string SenderNumber { get; set; }
}
