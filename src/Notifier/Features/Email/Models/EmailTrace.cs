using MongoDB.Bson;

namespace Notifier.Features.Email.Models;

public class EmailTrace
{
    public ObjectId Id { get; set; }

    public string Sender { get; set; }

    public string TrackId { get; set; }

    public string Email { get; set; }

    public string Body { get; set; }

    public string Subject { get; set; }

    public EmailTraceStatus Status { get; set; }

    public static EmailTrace Create(string sender, string trackId, string email, string subject, string body)
        => new EmailTrace
        {
            Email = email,
            Sender = sender,
            TrackId = trackId,
            Body = body,
            Subject = subject,
            Status = EmailTraceStatus.Notified
        };
}
