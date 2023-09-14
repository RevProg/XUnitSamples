using MyAPI.Models;

namespace MyAPI.Services;

public class Storage : IStorage
{
    public async Task<StorageData> DownloadDataForUser(int userId)
    {
        if (userId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(userId), "Parameter should be positive integer.");
        }

        await Task.Delay(500);
        return new StorageData(userId, "something");
    }

    public async Task UploadDataForUser(StorageData storageData)
    {
        ArgumentNullException.ThrowIfNull(storageData, nameof(storageData));

        await Task.Delay(500);
    }
}
