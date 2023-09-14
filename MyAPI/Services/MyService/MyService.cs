namespace MyAPI.Services;

public class MyService : IMyService
{
    public int Total { get; set; } = 4;

    public virtual int CalculateValue()
    {
        return GetValue() * 2;
    }

    protected virtual int GetValue()
    {
        return 25;
    }

    internal int GetInternalValue()
    {
        return 20;
    }
}
