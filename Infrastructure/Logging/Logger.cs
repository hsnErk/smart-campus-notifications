using SmartCampus.Domain.Logging;

namespace SmartCampus.Infrastructure.Logging;

/// <summary>
/// SINGLETON PATTERN (2 of 2). One application-wide audit log. It also implements
/// the Domain ILogger interface, so upper layers depend on the abstraction while
/// still sharing this one instance.
/// </summary>
public sealed class Logger : ILogger
{
    private static readonly Lazy<Logger> _instance = new(() => new Logger());

    public static Logger Instance => _instance.Value;

    private Logger() { }

    public void Log(string message) =>
        Console.WriteLine($"   [LOG {DateTime.Now:HH:mm:ss}] {message}");
}
