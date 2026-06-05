namespace SmartCampus.Domain.Notifications;

/// <summary>
/// One channel's way of delivering a message. Concrete channels live in
/// Infrastructure; every other layer depends only on this contract.
/// </summary>
public interface INotification
{
    void Send(string recipient, string message);
}
