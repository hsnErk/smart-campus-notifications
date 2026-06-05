using SmartCampus.Domain.Notifications;
using SmartCampus.Infrastructure.Configuration;
using SmartCampus.Infrastructure.Logging;

namespace SmartCampus.Infrastructure.Notifications;

public sealed class SmsNotification : INotification
{
    public void Send(string recipient, string message)
    {
        Console.WriteLine($"   -> SMS   to {recipient}: {message}");
        Logger.Instance.Log($"{ConfigurationManager.Instance.SystemName}: SMS sent to {recipient}.");
    }
}
