using MyAPI.Models;

namespace MyAPI.Services;

public class ComplexV2 : IComplex
{
    private readonly IDBContext _dbContext;
    private readonly IStorage _storage;
    private readonly ICalculator _calculator;

    public ComplexV2(IDBContext dbContext, IStorage storage, ICalculator calculator)
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

        await ProceedWithStorageUpdate(user);
    }

    public async Task ProceedWithStorageUpdate(User user)
    {
        var storageData = await _storage.DownloadDataForUser(user.Id);

        user.Counter = _calculator.Add(user.Counter, 1);

        var dataToUpdate = GenerateNewStorageData(user, storageData);

        await UpdateUserData(user, dataToUpdate);
    }

    public StorageData GenerateNewStorageData(User user, StorageData storageData)
    {
        return new StorageData(user.Id, storageData.UserData + user.Counter.ToString());
    }

    public async Task UpdateUserData(User user, StorageData dataToUpdate)
    {
        await Task.WhenAll(_dbContext.UpdateItem(user), _storage.UploadDataForUser(dataToUpdate));
    }
}
