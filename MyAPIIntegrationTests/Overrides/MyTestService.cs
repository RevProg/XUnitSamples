using MyAPI.Services;

namespace MyAPIIntegrationTests.Overrides;

public class MyTestService : IMyService
{
    public int Total { get; set; } = 22;
}
