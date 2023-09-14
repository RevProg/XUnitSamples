namespace MyAPI.Services;

public class Calculator : ICalculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }

    public int Multiply(int a, int b)
    {
        return a * b;
    }

    public double Sin(double x, InputType type)
    {
        if (type == InputType.Degrees)
        {
            return Math.Sin(DegreesToRadians(x));
        }

        return Math.Sin(x);
    }

    private double DegreesToRadians(double x)
    {
        return x * Math.PI / 180;
    }
}
