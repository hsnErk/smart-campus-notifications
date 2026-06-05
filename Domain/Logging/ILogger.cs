namespace SmartCampus.Domain.Logging;

/// <summary>Logging abstraction so upper layers never reference the concrete Logger.</summary>
public interface ILogger
{
    void Log(string message);
}
