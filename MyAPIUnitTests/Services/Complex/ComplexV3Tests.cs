using MyAPI.Models;
using MyAPI.Services;

namespace MyAPIUnitTests.Services.Complex;

public class ComplexV3Tests
{
    private readonly Mock<ComplexV3> _complexV3Mock;
    private readonly Mock<IDBContext> _dbContextMock;
    private readonly Mock<IStorage> _storageMock;
    private readonly Mock<ICalculator> _calculatorMock;

    public ComplexV3Tests()
    {
        const MockBehavior mockBehavior = MockBehavior.Loose;

        _dbContextMock = new Mock<IDBContext>(mockBehavior);
        _storageMock = new Mock<IStorage>(mockBehavior);
        _calculatorMock = new Mock<ICalculator>(mockBehavior);
        _complexV3Mock = new Mock<ComplexV3>(mockBehavior, _dbContextMock.Object, _storageMock.Object, _calculatorMock.Object);
    }

    [Fact]
    public async Task ProcessUser_UserDoesNotExist_ThrowsInvalidOperationException()
    {
        _complexV3Mock
            .Setup(o => o.ProcessUser(It.IsAny<int>()))
            .CallBase();

        _dbContextMock
            .Setup(o => o.GetItemById<User>(It.IsAny<int>()))
            .Returns(Task.FromResult((User?)null));

        await Awaiting(async () => await _complexV3Mock.Object.ProcessUser(0))
            .Should().ThrowAsync<InvalidOperationException>();

        _dbContextMock
            .Verify(o => o.GetItemById<User>(0), Times.Once);
        _dbContextMock
            .Verify(o => o.UpdateItem(It.IsAny<User>()), Times.Never);

        _dbContextMock.VerifyNoOtherCalls();
        _storageMock.VerifyNoOtherCalls();
        _calculatorMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task ProcessUser_ValidInput_Success()
    {
        var userId = 1;
        var initialCounter = 2;
        var expectedUser = new User { Id = userId, Counter = initialCounter };

        _complexV3Mock
            .Setup(o => o.ProcessUser(It.IsAny<int>()))
            .CallBase();

        _complexV3Mock
            .Setup(o => o.ProceedWithStorageUpdate(expectedUser))
            .Returns(Task.CompletedTask);

        _dbContextMock
            .Setup(o => o.GetItemById<User>(It.IsAny<int>()))
            .ReturnsAsync(expectedUser);

        await _complexV3Mock.Object.ProcessUser(expectedUser.Id);

        _complexV3Mock
            .Verify(o => o.ProcessUser(It.IsAny<int>()), Times.Once);
        _complexV3Mock
            .Verify(o => o.ProceedWithStorageUpdate(It.IsAny<User>()), Times.Once);

        _dbContextMock
            .Verify(o => o.GetItemById<User>(expectedUser.Id), Times.Once);

        _complexV3Mock.VerifyNoOtherCalls();
        _dbContextMock.VerifyNoOtherCalls();
        _storageMock.VerifyNoOtherCalls();
        _calculatorMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task ProceedWithStorageUpdate_ValidInput_Success()
    {
        var userId = 1;
        var initialCounter = 2;
        var expectedUser = new User { Id = userId, Counter = initialCounter };
        var sampleData = new StorageData { UserId = expectedUser.Id, UserData = "data" };

        _complexV3Mock
            .Setup(o => o.ProceedWithStorageUpdate(expectedUser))
            .CallBase();

        _complexV3Mock
            .Setup(o => o.GenerateNewStorageData(expectedUser, sampleData))
            .CallBase();

        _complexV3Mock
            .Setup(o => o.UpdateUserData(expectedUser, It.IsAny<StorageData>()))
            .Returns(Task.CompletedTask);

        _storageMock
            .Setup(o => o.DownloadDataForUser(expectedUser.Id))
            .ReturnsAsync(sampleData);

        _calculatorMock
            .Setup(o => o.Add(initialCounter, 1))
            .Returns(3);

        await _complexV3Mock.Object.ProceedWithStorageUpdate(expectedUser);

        _complexV3Mock
            .Verify(o => o.ProceedWithStorageUpdate(It.IsAny<User>()), Times.Once);
        _complexV3Mock
            .Verify(o => o.GenerateNewStorageData(It.IsAny<User>(), It.IsAny<StorageData>()), Times.Once);
        _complexV3Mock
            .Verify(o => o.UpdateUserData(It.IsAny<User>(), It.IsAny<StorageData>()), Times.Once);

        _storageMock
            .Verify(o => o.DownloadDataForUser(It.IsAny<int>()), Times.Once);

        _calculatorMock
            .Verify(o => o.Add(initialCounter, 1), Times.Once);

        _complexV3Mock.VerifyNoOtherCalls();
        _dbContextMock.VerifyNoOtherCalls();
        _storageMock.VerifyNoOtherCalls();
        _calculatorMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task UpdateUserData_ValidInput_Success()
    {
        var userId = 1;
        var initialCounter = 2;
        var expectedUser = new User { Id = userId, Counter = initialCounter };
        var sampleData = new StorageData { UserId = expectedUser.Id, UserData = "data" };

        _complexV3Mock
            .Setup(o => o.UpdateUserData(expectedUser, sampleData))
            .CallBase();

        _dbContextMock
            .Setup(o => o.UpdateItem(It.IsAny<User>()))
            .Returns(Task.CompletedTask);

        _storageMock
            .Setup(o => o.UploadDataForUser(It.IsAny<StorageData>()))
            .Returns(Task.CompletedTask);

        await _complexV3Mock.Object.UpdateUserData(expectedUser, sampleData);

        _complexV3Mock
            .Verify(o => o.UpdateUserData(It.IsAny<User>(), It.IsAny<StorageData>()), Times.Once);

        _dbContextMock
            .Verify(o => o.UpdateItem(It.IsAny<User>()), Times.Once);

        _storageMock
            .Verify(o => o.UploadDataForUser(It.IsAny<StorageData>()), Times.Once);

        _complexV3Mock.VerifyNoOtherCalls();
        _dbContextMock.VerifyNoOtherCalls();
        _storageMock.VerifyNoOtherCalls();
        _calculatorMock.VerifyNoOtherCalls();
    }

    [Fact]
    public void GenerateNewStorageData_ValidInput_Success()
    {
        var userId = 1;
        var initialCounter = 2;
        var expectedUser = new User { Id = userId, Counter = initialCounter };
        var sampleData = new StorageData { UserId = expectedUser.Id, UserData = "data" };
        var expectedUserData = sampleData.UserData + expectedUser.Counter.ToString();

        _complexV3Mock
            .Setup(o => o.GenerateNewStorageData(expectedUser, sampleData))
            .CallBase();

        var result = _complexV3Mock.Object.GenerateNewStorageData(expectedUser, sampleData);

        result.Should().NotBe(null);
        result.UserId.Should().Be(expectedUser.Id);
        result.UserData.Should().BeEquivalentTo(expectedUserData);
    }
}
