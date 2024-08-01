namespace Notifier.Models;

[Flags]
public enum NotifyChannel
{
    SMS = 0,
    Email = 1,
    MSTeams = 2,
    Telegram = 3
}