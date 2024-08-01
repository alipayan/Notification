using MassTransit;
using Notifier.Subscriptions.IntegrationMessage;

namespace Notifier.Subscriptions;

public class BatchNotifyMessageConsumer : IConsumer<BatchNotifyMessage>
{
    public Task Consume(ConsumeContext<BatchNotifyMessage> context)
    {
        foreach (var notify in context.Message.Notifies)
        {
            switch (notify.Channel)
            {
                case Models.NotifyChannel.SMS:
                    {
                        //get user data 
                        //use sms service for sending message
                    }
                    break;
                case Models.NotifyChannel.Email:
                    break;
                case Models.NotifyChannel.MSTeams:
                    break;
                case Models.NotifyChannel.Telegram:
                    break;
                default:
                    break;
            }
        }








        return Task.CompletedTask;
    }
}
