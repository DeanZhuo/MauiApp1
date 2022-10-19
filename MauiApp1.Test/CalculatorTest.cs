namespace MauiApp1.Test
{
    public class CalculatorTest
    {
        [Theory]
        [InlineData(2, 3, "+", 5)]
        [InlineData(2, 1.2, "-", 0.8)]
        [InlineData(6, 2, "x", 12)]
        [InlineData(100, 12.5, "/", 8)]
        public void Calculate_ShouldWork(double x, double y, string operation, double expected)
        {
            double actual;

            actual = Calculator.Calculator.Calculate(x, y, operation);

            Assert.Equal(expected, actual);
        }
    }
}