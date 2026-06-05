using SmartCampus.Domain.Enums;

namespace SmartCampus.Domain.Announcements;

public sealed class LibraryAnnouncement : Announcement
{
    public string NewHours { get; }

    public LibraryAnnouncement(string newHours)
        : base("Library Hours Update")
    {
        NewHours = newHours;
    }

    public override AnnouncementType Type => AnnouncementType.Library;

    public override string BuildMessage() =>
        $"[LIBRARY] New opening hours: {NewHours}.";
}
