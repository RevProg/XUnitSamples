using MyAPIIntegrationTests.Fixtures;

namespace MyAPIIntegrationTests.Collections;

[Collection("Logs collection")]
public class SecondItemTests
{
    private readonly TestLog _log;

    public SecondItemTests(TestLog log)
    {
        _log = log;
    }

    [Fact]
    public void VerySampleTest()
    {
        _log.AddLog("Hello from SecondItemTests VerySampleTest");
    }
}
