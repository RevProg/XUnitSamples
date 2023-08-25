using MyAPI.Models;

namespace MyAPI.Services;

public class ComplexV1 : IComplex
{
    private readonly IDBContext _dbContext;
    private readonly IStorage _storage;
    private readonly ICalculator _calculator;

    public ComplexV1(IDBContext dbContext, IStorage storage, ICalculator calculator)
    {
        _dbContext = dbContext;
        _storage = storage;
        _calculator = calculator;
    }

    public async Task ProcessUser(int userId)
    {
        var user = await _dbContext.GetItemById<User>(userId);
        if (user == null)
        {
            throw new InvalidOperationException("User does not exist");
        }

        var storageData = await _storage.DownloadDataForUser(user.Id);

        user.Counter = _calculator.Add(user.Counter, 1);

        var dataToUpdate = new StorageData(user.Id, storageData.UserData + user.Counter.ToString());

        await _dbContext.UpdateItem(user);
        await _storage.UploadDataForUser(dataToUpdate);
    }
}
