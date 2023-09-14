namespace MyAPI.Services
{
    public interface ICalculator
    {
        int Add(int a, int b);

        int Multiply(int a, int b);

        double Sin(double x, InputType type);
    }

    public enum InputType
    {
        Radians = 0,
        Degrees = 1,
    }
}