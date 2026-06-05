using SmartCampus.Domain.Enums;
using SmartCampus.Domain.Notifications;

namespace SmartCampus.Infrastructure.Notifications;

/// <summary>
/// FACTORY PATTERN (2 of 2). Centralises notification-channel creation: given a
/// channel it returns the right INotification implementation, so callers (the users
/// /observers) depend only on the INotification abstraction. It implements the
/// Domain INotificationFactory contract.
/// </summary>
public sealed class NotificationFactory : INotificationFactory
{
    public INotification Create(NotificationChannel channel) => channel switch
    {
        NotificationChannel.Email => new EmailNotification(),
        NotificationChannel.Sms   => new SmsNotification(),
        NotificationChannel.Push  => new PushNotification(),
        _ => throw new ArgumentOutOfRangeException(
                 nameof(channel), channel, "Unknown notification channel.")
    };
}
