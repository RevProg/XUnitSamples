namespace MyAPIUnitTests;

public static class MyMockRepository
{
    private static readonly MockRepository _repository = new(MockBehavior.Strict);

    public static Mock<T> Create<T>() where T : class
    {
        return _repository.Create<T>();
    }

    public static Mock<T> Create<T>(MockBehavior mockBehavior) where T : class
    {
        return _repository.Create<T>(mockBehavior);
    }

    public static Mock<T> Create<T>(params object[] args) where T : class
    {
        return _repository.Create<T>(args);
    }

    public static Mock<T> Create<T>(MockBehavior mockBehavior, params object[] args) where T : class
    {
        return _repository.Create<T>(mockBehavior, args);
    }
}
