using MyAPI.Services;

namespace MyAPIUnitTests.Services;

public class CalculatorTests
{
    private readonly Calculator _calculator;

    public CalculatorTests()
    {
        _calculator = new Calculator();
    }

    [Fact]
    public void Add_ValidNumbersFact_Success()
    {
        var actualResult = _calculator.Add(3, 5);

        Assert.Equal(8, actualResult);
    }

    [Theory]
    [InlineData(1, 2, 3)]
    [InlineData(-5, 2, -3)]
    [InlineData(1555, 232, 1787)]
    public void Add_ValidNumbersTheory_Success(int first, int second, int expectedResult)
    {
        var actualResult = _calculator.Add(first, second);

        //actualResult.Should().Be(expectedResult);
        Assert.Equal(expectedResult, actualResult);
    }

    [Theory]
    [InlineData(1, 3, 3)]
    [InlineData(5, 8, 40)]
    public void Multiply_ValidNumbers_Success(int first, int second, int expectedResult)
    {
        var actualResult = _calculator.Multiply(first, second);

        //actualResult.Should().Be(expectedResult);
        Assert.Equal(expectedResult, actualResult);
    }

    [Theory]
    [InlineData(30, InputType.Degrees, 0.5)]
    [InlineData(Math.PI / 2, InputType.Radians, 1)]
    public void Sin_ValidInput_CorrectResult(double x, InputType type, double expectedResult)
    {
        var actualResult = _calculator.Sin(x, type);

        Assert.Equal(expectedResult, actualResult, 5);
    }
}
