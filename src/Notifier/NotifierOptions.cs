namespace Notifier;

public sealed class NotifierOptions
{
    public BrokerOptions BrokerOptions { get; set; } = null!;
    public MongoDbSetting MongoDbSettings { get; set; } = null!;
}


public sealed class MongoDbSetting
{
    public required string Host { get; set; }
    public required string DataBase { get; set; }
}
public sealed class BrokerOptions
{
    public const string SectionName = "BrokerOptions";

    public required string Host { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}