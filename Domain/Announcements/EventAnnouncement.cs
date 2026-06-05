using SmartCampus.Domain.Enums;

namespace SmartCampus.Domain.Announcements;

public sealed class EventAnnouncement : Announcement
{
    public string Location { get; }
    public DateTime StartsAt { get; }

    public EventAnnouncement(string title, string location, DateTime startsAt)
        : base(title)
    {
        Location = location;
        StartsAt = startsAt;
    }

    public override AnnouncementType Type => AnnouncementType.Event;

    public override string BuildMessage() =>
        $"[EVENT] {Title} at {Location}, starting {StartsAt:dd MMM yyyy, HH:mm}.";
}
