using Moq.Protected;
using MyAPI.Services;

namespace MyAPIUnitTestsMSTest.Services;

[TestClass]
public class NonPublicMembersTests
{
    [TestMethod]
    public void GetValue_NewMethod_Success()
    {
        var myService = new MyServiceTest();
        Assert.AreEqual(25, myService.GetValue());
    }

    [TestMethod]
    public void GetInternalValue_InternalVisibleTo_Success()
    {
        var myService = new MyService();
        Assert.AreEqual(20, myService.GetInternalValue());
    }

    [TestMethod]
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

        Assert.AreEqual(10, actualValue);
    }

    [TestMethod]
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

        Assert.AreEqual(10, actualValue);
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
