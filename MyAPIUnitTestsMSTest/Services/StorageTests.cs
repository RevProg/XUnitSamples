using MyAPI.Models;
using MyAPI.Services;

namespace MyAPIUnitTestsMSTest.Services;

[TestClass]
public class StorageTests
{
    private readonly Storage _storage;

    public StorageTests()
    {
        _storage = new Storage();
    }

    [TestMethod]
    public async Task DownloadDataForUser_ExistingUser_Success()
    {
        var userId = 3;

        var actualResult = await _storage.DownloadDataForUser(userId);

        Assert.IsNotNull(actualResult);
        Assert.AreEqual(userId, actualResult.UserId);
    }

    [TestMethod]
    [DataRow(0)]
    [DataRow(-1)]
    [DataRow(-100)]
    public async Task DownloadDataForUser_InvalidId_Exception(int userId)
    {
        await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(
            async () => await _storage.DownloadDataForUser(userId)
        );
    }

    [TestMethod]
    public async Task UploadDataForUser_NullArgument_Exception()
    {
        await Assert.ThrowsExceptionAsync<ArgumentNullException>(
            async () => await _storage.UploadDataForUser(null)
        );
    }

    [TestMethod]
    //[DataRow(new StorageData()]
    [DynamicData(nameof(StorageData))]
    public async Task UploadDataForUser_ValidData_Success(StorageData data)
    {
        await _storage.UploadDataForUser(data);
    }

    [TestMethod]
    [DynamicData(nameof(StorageData))]
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

    [TestMethod]
    [DynamicData(nameof(StorageTestData.UploadDataForUser), typeof(StorageTestData))]
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