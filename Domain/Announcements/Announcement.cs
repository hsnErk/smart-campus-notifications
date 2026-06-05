using SmartCampus.Domain.Enums;

namespace SmartCampus.Domain.Announcements;

/// <summary>
/// Stable abstraction that every announcement shares. The publisher, observers and
/// factories all depend on THIS type, never on a concrete subclass - that is what
/// keeps the dependency arrows pointing inward toward the Domain.
/// </summary>
public abstract class Announcement
{
    public string Title { get; }
    public DateTime CreatedAt { get; }

    protected Announcement(string title)
    {
        Title = title;
        CreatedAt = DateTime.Now;
    }

    /// <summary>Short category label, also used by the Factory / logs.</summary>
    public abstract AnnouncementType Type { get; }

    /// <summary>
    /// Each concrete announcement decides how it renders itself into a message.
    /// Because this is polymorphic, the notification code never has to switch on type.
    /// </summary>
    public abstract string BuildMessage();
}
