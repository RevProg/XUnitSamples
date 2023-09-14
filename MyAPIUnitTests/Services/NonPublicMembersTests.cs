using Moq.Protected;
using MyAPI.Services;

namespace MyAPIUnitTests.Services;

public class NonPublicMembersTests
{
    [Fact]
    public void GetValue_NewMethod_Success()
    {
        var myService = new MyServiceTest();
        Assert.Equal(25, myService.GetValue());
    }

    [Fact]
    public void GetInternalValue_InternalVisibleTo_Success()
    {
        var myService = new MyService();
        Assert.Equal(20, myService.GetInternalValue());
    }

    [Fact]
    public void CalculateValue_ProtectedMockV1_Success()
    {
        var myServiceMock = new Mock<MyService>();

        myServiceMock
            .Setup(o => o.CalculateValue())
            .CallBase();

        myServiceMock
            .Protected()
            .Setup<int>("GetValue")
            .Returns(5);

        var actualValue = myServiceMock.Object.CalculateValue();

        Assert.Equal(10, actualValue);
    }

    [Fact]
    public void CalculateValue_ProtectedMockV2_Success()
    {
        var myServiceMock = new Mock<MyService>();

        myServiceMock
            .Setup(o => o.CalculateValue())
            .CallBase();

        myServiceMock
            .Protected()
            .As<IMyServiceTest>()
            .Setup(o => o.GetValue())
            .Returns(5)
            .Verifiable(Times.Once);

        var actualValue = myServiceMock.Object.CalculateValue();

        Assert.Equal(10, actualValue);
    }
}

internal class MyServiceTest : MyService
{
    // Override access modifer with new keyword
    public new int GetValue()
    {
        return base.GetValue();
    }
}

internal interface IMyServiceTest : IMyService
{
    int GetValue();
}
