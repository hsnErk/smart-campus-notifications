namespace SmartCampus.Infrastructure.Configuration;

/// <summary>
/// SINGLETON PATTERN (1 of 2). One source of truth for app-wide settings. A single
/// shared instance is genuinely the correct model here. Lazy&lt;T&gt; makes the
/// instance creation thread-safe, and the constructor is private so no one else can
/// create a second one.
/// </summary>
public sealed class ConfigurationManager
{
    private static readonly Lazy<ConfigurationManager> _instance =
        new(() => new ConfigurationManager());

    public static ConfigurationManager Instance => _instance.Value;

    private ConfigurationManager()
    {
        SystemName = "Smart Campus Notification System";
    }

    public string SystemName { get; }
}
