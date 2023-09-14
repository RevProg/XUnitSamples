namespace MyAPIIntegrationTests.Fixtures;

public class TestLog : IDisposable
{
    private readonly List<string> _logs = new List<string>();

    public TestLog() { }

    public void AddLog(string log) => _logs.Add(log);

    public void Dispose()
    {
        _logs.Clear();
    }
}
