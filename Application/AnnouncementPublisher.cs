using SmartCampus.Domain.Announcements;
using SmartCampus.Domain.Logging;
using SmartCampus.Domain.Observers;

namespace SmartCampus.Application;

/// <summary>
/// OBSERVER PATTERN - the Subject. Keeps a list of observers and pushes every
/// published announcement to all of them. It knows NOTHING about who the observers
/// are or how they choose to be notified; that decoupling is the whole point.
/// </summary>
public sealed class AnnouncementPublisher : ISubject
{
    private readonly List<IObserver> _observers = new();
    private readonly ILogger _logger;

    public AnnouncementPublisher(ILogger logger) => _logger = logger;

    public void Subscribe(IObserver observer)
    {
        if (!_observers.Contains(observer))
            _observers.Add(observer);
    }

    public void Unsubscribe(IObserver observer) => _observers.Remove(observer);

    public void NotifyAll(Announcement announcement)
    {
        _logger.Log($"Publishing '{announcement.Title}' to {_observers.Count} subscriber(s).");
        foreach (IObserver observer in _observers)
            observer.Update(announcement);
    }
}
