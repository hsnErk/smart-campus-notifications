using SmartCampus.Domain.Notifications;
using SmartCampus.Infrastructure.Configuration;
using SmartCampus.Infrastructure.Logging;

namespace SmartCampus.Infrastructure.Notifications;

public sealed class EmailNotification : INotification
{
    public void Send(string recipient, string message)
    {
        Console.WriteLine($"   -> EMAIL to {recipient}: {message}");
        Logger.Instance.Log($"{ConfigurationManager.Instance.SystemName}: email sent to {recipient}.");
    }
}
