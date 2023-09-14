using MyAPIIntegrationTests.Fixtures;

namespace MyAPIIntegrationTests.Collections;

[Collection("Logs collection")]
public class FirstItemTests
{
    private readonly TestLog _log;

    public FirstItemTests(TestLog log)
    {
        _log = log;
    }

    [Fact]
    public void SampleTest()
    {
        _log.AddLog("Hello from FirstItemTests SampleTest");
    }
}
