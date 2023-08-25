using MyAPI.Models;

namespace MyAPI.Services;

public class Storage : IStorage
{
    public async Task<StorageData> DownloadDataForUser(int userId)
    {
        await Task.Delay(500);
        return new StorageData(userId, "something");
    }

    public async Task UploadDataForUser(StorageData storageData)
    {
        await Task.Delay(500);
    }
}
