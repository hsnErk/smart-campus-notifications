using SmartCampus.Domain.Users;

namespace SmartCampus.Infrastructure.Data;

/// <summary>
/// A tiny in-memory "repository" of users - the assignment's "basit veri yonetimi"
/// (simple data management). In a real system this would sit behind a repository
/// interface and talk to a database.
/// </summary>
public sealed class InMemoryUserStore
{
    private readonly List<User> _users = new();

    public void Add(User user) => _users.Add(user);

    public IReadOnlyList<User> GetAll() => _users.AsReadOnly();
}
