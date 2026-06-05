using SmartCampus.Domain.Announcements;
using SmartCampus.Domain.Enums;
using SmartCampus.Domain.Notifications;
using SmartCampus.Domain.Observers;

namespace SmartCampus.Domain.Users;

/// <summary>
/// A campus user who can subscribe to announcements - the Observer in the pattern.
/// Concrete user types decide their own receiving behaviour by overriding Compose().
/// The notification factory is injected, so a user can build its preferred channel
/// without the Domain depending on Infrastructure.
/// </summary>
public abstract class User : IObserver
{
    private readonly INotificationFactory _notificationFactory;

    protected User(string name, string contact, NotificationChannel preferredChannel,
                   INotificationFactory notificationFactory)
    {
        Name = name;
        Contact = contact;
        PreferredChannel = preferredChannel;
        _notificationFactory = notificationFactory;
    }

    public string Name { get; }
    public string Contact { get; }
    public NotificationChannel PreferredChannel { get; }

    /// <summary>Human-readable role, e.g. "Student" or "Teacher".</summary>
    public abstract string Role { get; }

    /// <summary>
    /// Observer entry point. The user asks the Factory for a notification on its
    /// preferred channel, then sends itself the message the announcement rendered.
    /// </summary>
    public void Update(Announcement announcement)
    {
        INotification notification = _notificationFactory.Create(PreferredChannel);
        notification.Send(Contact, Compose(announcement));
    }

    /// <summary>How this user type turns an announcement into its personal message.</summary>
    protected virtual string Compose(Announcement announcement) =>
        $"Dear {Role} {Name}, {announcement.BuildMessage()}";
}
