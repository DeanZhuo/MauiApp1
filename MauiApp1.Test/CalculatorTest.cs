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

        [Theory]
        [InlineData(1000.0, "1,000")]
        [InlineData(10.50, "10.5")]
        [InlineData(0.987654321, "0.9876543")]
        public void ConvertString_ShouldConvertAndTrim(double? input, string expected)
        {
            string actual;

            actual = Calculator.Calculator.ConvertString(input);

            Assert.Equal(expected, actual);
        }
    }
}