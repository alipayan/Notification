
using Notifier.Models;

namespace Notifier.Subscriptions.IntegrationMessage;

public class BatchNotifyMessage
{
    public ICollection<BatchNotifyItme> Notifies { get; set; }
}

public class BatchNotifyItme
{
    public string Message { get; set; }

    public Guid UserId { get; set; }

    public NotifyChannel Channel { get; set; }
}