using SmartCampus.Domain.Enums;

namespace SmartCampus.Domain.Notifications;

/// <summary>
/// Abstraction over notification creation. Observers depend on THIS, not on the
/// concrete factory, so the Domain never needs to know which channels exist or how
/// they are constructed (Dependency Inversion - the concrete factory is injected).
/// </summary>
public interface INotificationFactory
{
    INotification Create(NotificationChannel channel);
}
