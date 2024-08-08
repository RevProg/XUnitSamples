using MyAPI.Services;

namespace MyAPIUnitTestsMSTest.Services;

[TestClass]
public class CalculatorTests
{
    private readonly Calculator _calculator;

    public CalculatorTests()
    {
        _calculator = new Calculator();
    }

    [TestMethod]
    public void Add_ValidNumbersFact_Success()
    {
        var actualResult = _calculator.Add(3, 5);

        Assert.AreEqual(8, actualResult);
    }

    [TestMethod]
    [DataRow(1, 2, 3)]
    [DataRow(-5, 2, -3)]
    [DataRow(1555, 232, 1787)]
    public void Add_ValidNumbersTheory_Success(int first, int second, int expectedResult)
    {
        var actualResult = _calculator.Add(first, second);

        //actualResult.Should().Be(expectedResult);
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    [DataRow(1, 3, 3)]
    [DataRow(5, 8, 40)]
    public void Multiply_ValidNumbers_Success(int first, int second, int expectedResult)
    {
        var actualResult = _calculator.Multiply(first, second);

        //actualResult.Should().Be(expectedResult);
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    [DataRow(30, InputType.Degrees, 0.5)]
    [DataRow(Math.PI / 2, InputType.Radians, 1)]
    public void Sin_ValidInput_CorrectResult(double x, InputType type, double expectedResult)
    {
        var actualResult = _calculator.Sin(x, type);

        Assert.AreEqual(expectedResult, actualResult, 5);
    }
}
