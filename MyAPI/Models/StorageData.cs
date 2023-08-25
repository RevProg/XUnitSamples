namespace MyAPI.Models;

public record StorageData
{
    public int UserId { get; init; }

    public string UserData { get; init; }

    public StorageData()
    {
        UserData = string.Empty;
    }

    public StorageData(int userId, string userData)
    {
        UserId = userId;
        UserData = userData;
    }
}
