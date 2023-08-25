namespace MyAPI.Services
{
    public interface IDBContext
    {
        Task<T?> GetItemById<T>(int id) where T : class, new();
        Task UpdateItem<T>(T item);
    }
}