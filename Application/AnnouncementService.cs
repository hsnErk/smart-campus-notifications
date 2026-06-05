using SmartCampus.Application.Factories;
using SmartCampus.Domain.Announcements;
using SmartCampus.Domain.Enums;

namespace SmartCampus.Application;

/// <summary>
/// Application-layer use case: turn a request into the right announcement (via the
/// Factory) and publish it (via the Observer subject). Presentation calls THIS and
/// nothing lower-level, so the wiring of patterns stays hidden behind one method.
/// </summary>
public sealed class AnnouncementService
{
    private readonly AnnouncementPublisher _publisher;

    public AnnouncementService(AnnouncementPublisher publisher) => _publisher = publisher;

    public Announcement CreateAndPublish(AnnouncementType type, IDictionary<string, object> data)
    {
        Announcement announcement = AnnouncementFactory.Create(type, data);
        _publisher.NotifyAll(announcement);
        return announcement;
    }
}
