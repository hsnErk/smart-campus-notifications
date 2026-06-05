using SmartCampus.Domain.Announcements;
using SmartCampus.Domain.Enums;

namespace SmartCampus.Application.Factories;

/// <summary>
/// FACTORY PATTERN (1 of 2). Centralises announcement creation: callers pass a TYPE
/// and get back the correct Announcement subclass, so no other class needs to know
/// the concrete constructors. Adding a new announcement type touches only this file.
/// </summary>
public static class AnnouncementFactory
{
    public static Announcement Create(AnnouncementType type, IDictionary<string, object> data)
    {
        return type switch
        {
            AnnouncementType.Exam => new ExamAnnouncement(
                (string)data["courseCode"],
                (DateTime)data["newExamDate"]),

            AnnouncementType.Event => new EventAnnouncement(
                (string)data["title"],
                (string)data["location"],
                (DateTime)data["startsAt"]),

            AnnouncementType.Library => new LibraryAnnouncement(
                (string)data["newHours"]),

            _ => throw new ArgumentOutOfRangeException(
                     nameof(type), type, "Unknown announcement type.")
        };
    }
}
