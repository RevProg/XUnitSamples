using AutoFixture;
using MyAPI.Models;

namespace MyAPIUnitTestsMSTest;

[TestClass]
public class AutoFixtureSamples
{
    [TestMethod]
    public void SampleTest()
    {
        var fixture = new Fixture();
        var model = fixture.Create<SomeData>();

        model.Should().NotBeNull();
        model.Guid.Should().NotBeEmpty();
        model.Name.Should().NotBeEmpty();
    }
}
