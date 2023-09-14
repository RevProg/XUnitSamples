using MyAPI.Models;
using System.Runtime.CompilerServices;

namespace MyAPI.Services;

public class DBContext : IDBContext
{
    private readonly ILogger<DBContext> _logger;

    public DBSet<User> Users { get; init; }

    public DBContext(ILogger<DBContext> logger)
    {
        _logger = logger;

        Users = new DBSet<User>();
    }

    public async Task UpdateItem<T>(T item)
    {
        _logger.LogInformation("Updating and item");
        await Task.Delay(500);
    }
    public async Task<T?> GetItemById<T>(int id) where T : class, new()
    {
        await Task.Delay(500);
        return new T();
    }
}

public class DBSet<T> where T : class
{
    private List<T> _items = new List<T>();

    public void Add(T item) => _items.Add(item);

    public async Task ExecuteUpdate(T item, string property)
    {
        // some DB update code

        await Task.Delay(100);
    }
}

public static class DBContextExtensions
{
    public async static Task HelperMethod<T>(this DBSet<T> dbSet, T item) where T : class
    {
        // usually some more complex call. Check LoggerExtensions for example.
        await dbSet.ExecuteUpdate(item, "Name");
    }
}