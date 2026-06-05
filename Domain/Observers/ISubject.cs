using SmartCampus.Domain.Announcements;

namespace SmartCampus.Domain.Observers;

/// <summary>The thing being observed: it manages subscribers and notifies them all.</summary>
public interface ISubject
{
    void Subscribe(IObserver observer);
    void Unsubscribe(IObserver observer);
    void NotifyAll(Announcement announcement);
}
