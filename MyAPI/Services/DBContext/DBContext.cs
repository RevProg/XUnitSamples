namespace MyAPI.Services;

public class DBContext : IDBContext
{
    public async Task UpdateItem<T>(T item)
    {
        await Task.Delay(500);
    }
    public async Task<T?> GetItemById<T>(int id) where T : class, new()
    {
        await Task.Delay(500);
        return new T();
    }
}
