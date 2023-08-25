using MyAPI.Models;

namespace MyAPI.Services
{
    public interface IStorage
    {
        Task<StorageData> DownloadDataForUser(int userId);

        Task UploadDataForUser(StorageData storageData);
    }
}