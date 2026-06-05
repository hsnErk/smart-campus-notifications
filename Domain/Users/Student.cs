using SmartCampus.Domain.Enums;
using SmartCampus.Domain.Notifications;

namespace SmartCampus.Domain.Users;

public sealed class Student : User
{
    public Student(string name, string contact, NotificationChannel preferredChannel,
                   INotificationFactory notificationFactory)
        : base(name, contact, preferredChannel, notificationFactory) { }

    public override string Role => "Student";
}
