using SmartCampus.Domain.Announcements;

namespace SmartCampus.Domain.Observers;

/// <summary>Anything that wants to react when a new announcement is published.</summary>
public interface IObserver
{
    void Update(Announcement announcement);
}
