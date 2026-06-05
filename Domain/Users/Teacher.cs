using SmartCampus.Domain.Announcements;
using SmartCampus.Domain.Enums;
using SmartCampus.Domain.Notifications;

namespace SmartCampus.Domain.Users;

public sealed class Teacher : User
{
    public Teacher(string name, string contact, NotificationChannel preferredChannel,
                   INotificationFactory notificationFactory)
        : base(name, contact, preferredChannel, notificationFactory) { }

    public override string Role => "Teacher";

    // Each user type defines its own receiving behaviour; teachers get an extra note.
    protected override string Compose(Announcement announcement) =>
        base.Compose(announcement) + " (Please update your course page if affected.)";
}
