﻿using MyAPI.Models;
using MyAPI.Services;

namespace MyAPIUnitTestsMSTest.Services.Complex;

[TestClass]
public class ComplexV1Tests
{
    private ComplexV1 _complexV1;
    private Mock<IDBContext> _dbContextMock;
    private Mock<IStorage> _storageMock;
    private Mock<ICalculator> _calculatorMock;

    [TestInitialize]
    public void Setup()
    {
        _dbContextMock = new Mock<IDBContext>();
        _storageMock = new Mock<IStorage>();
        _calculatorMock = new Mock<ICalculator>();
        _complexV1 = new ComplexV1(_dbContextMock.Object, _storageMock.Object, _calculatorMock.Object);
    }

    [TestMethod]
    public async Task ProcessUser_UserDoesNotExist_ThrowsInvalidOperationException()
    {
        _dbContextMock
            .Setup(o => o.GetItemById<User>(It.IsAny<int>()))
            .Returns(Task.FromResult((User?)null));

        await Awaiting(async () => await _complexV1.ProcessUser(0))
            .Should().ThrowAsync<InvalidOperationException>();

        _dbContextMock
            .Verify(o => o.GetItemById<User>(0), Times.Once);
        _dbContextMock
            .Verify(o => o.UpdateItem(It.IsAny<User>()), Times.Never);

        _storageMock.VerifyNoOtherCalls();
        _calculatorMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task ProcessUser_ValidInput_Success()
    {
        var userId = 1;
        var initialCounter = 2;
        var expectedUser = new User { Id = userId, Counter = initialCounter };
        var sampleData = new StorageData { UserId = expectedUser.Id, UserData = "data" };

        _dbContextMock
            .Setup(o => o.GetItemById<User>(It.IsAny<int>()))
            .ReturnsAsync(expectedUser);

        _dbContextMock
            .Setup(o => o.UpdateItem(It.IsAny<User>()))
            .Returns(Task.CompletedTask);

        _storageMock
            .Setup(o => o.DownloadDataForUser(expectedUser.Id))
            .ReturnsAsync(sampleData);

        _storageMock
            .Setup(o => o.UploadDataForUser(It.IsAny<StorageData>()))
            .Returns(Task.CompletedTask);

        _calculatorMock
            .Setup(o => o.Add(initialCounter, 1))
            .Returns(3);

        await _complexV1.ProcessUser(expectedUser.Id);

        _dbContextMock
            .Verify(o => o.GetItemById<User>(expectedUser.Id), Times.Once);
        _dbContextMock
            .Verify(o => o.UpdateItem(It.IsAny<User>()), Times.Once);

        _storageMock
            .Verify(o => o.DownloadDataForUser(expectedUser.Id), Times.Once);
        _storageMock
            .Verify(o => o.UploadDataForUser(It.IsAny<StorageData>()), Times.Once);

        _calculatorMock
            .Verify(o => o.Add(initialCounter, 1), Times.Once);

        _dbContextMock.VerifyNoOtherCalls();
        _storageMock.VerifyNoOtherCalls();
        _calculatorMock.VerifyNoOtherCalls();
    }
}
