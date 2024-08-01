using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace Notifier.Features.Sms.Models;

[Collection("sms_traces")]
public class SmsTrace
{
    public ObjectId Id { get; set; }

    public string ProviderName { get; set; }

    public string InquiryId { get; set; }

    public string Mobile { get; set; }

    public string Message { get; set; }

    public SmsTraceStatus Status { get; set; }


    public static SmsTrace Create(string providerName, string message, string mobile, string inquiryId)
        => new SmsTrace
        {
            ProviderName = providerName,
            Message = message,
            Mobile = mobile,
            InquiryId = inquiryId,
            Status = SmsTraceStatus.Inquiry
        };
}
