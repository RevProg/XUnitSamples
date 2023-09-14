using MyAPI.Models;
using MyAPI.Services;

namespace MyAPIUnitTests.Services;

public class StorageTests
{
    private readonly Storage _storage;

    public StorageTests()
    {
        _storage = new Storage();
    }

    [Fact]
    public async Task DownloadDataForUser_ExistingUser_Success()
    {
        var userId = 3;

        var actualResult = await _storage.DownloadDataForUser(userId);

        Assert.NotNull(actualResult);
        Assert.Equal(userId, actualResult.UserId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public async Task DownloadDataForUser_InvalidId_Exception(int userId)
    {
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            async () => await _storage.DownloadDataForUser(userId)
        );
    }

    [Fact]
    public async Task UploadDataForUser_NullArgument_Exception()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await _storage.UploadDataForUser(null)
        );
    }

    [Theory]
    //[InlineData(new StorageData()]
    [MemberData(nameof(StorageData))]
    public async Task UploadDataForUser_ValidData_Success(StorageData data)
    {
        await _storage.UploadDataForUser(data);
    }

    [Theory]
    [MemberData(nameof(StorageData), parameters: 2)]
    public async Task UploadDataForUser_ValidLimitedData_Success(StorageData data)
    {
        await _storage.UploadDataForUser(data);
    }

    public static IEnumerable<object[]> StorageData =>
        new List<object[]>
        {
                new object[] { new StorageData(3, "test") },
                new object[] { new StorageData(5, "") },
                new object[] { new StorageData(8, "") },
        };

    public static IEnumerable<object[]> GetStorageData(int numTests)
    {
        return StorageData.Take(numTests);
    }

    [Theory]
    [MemberData(nameof(StorageTestData.UploadDataForUser), MemberType = typeof(StorageTestData))]
    public async Task UploadDataForUser_ValidDataFromOtherClass_Success(StorageData data)
    {
        await _storage.UploadDataForUser(data);
    }
}

public class StorageTestData
{
    public static IEnumerable<object[]> UploadDataForUser =>
        new List<object[]>
        {
                new object[] { new StorageData(3, "test") },
                new object[] { new StorageData(5, "") },
        };
}