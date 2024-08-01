namespace Notifier.Features.Email;

public class EmailOptions
{
    public const string SectionName = "Features:Email";

    public string Host { get; set; }
    public int Port { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Sender { get; set; }
    public string TrackingUrl { get; set; }

}
