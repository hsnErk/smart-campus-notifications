using SmartCampus.Domain.Notifications;
using SmartCampus.Infrastructure.Configuration;
using SmartCampus.Infrastructure.Logging;

namespace SmartCampus.Infrastructure.Notifications;

public sealed class PushNotification : INotification
{
    public void Send(string recipient, string message)
    {
        Console.WriteLine($"   -> PUSH  to {recipient}: {message}");
        Logger.Instance.Log($"{ConfigurationManager.Instance.SystemName}: push sent to {recipient}.");
    }
}
