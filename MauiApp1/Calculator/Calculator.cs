namespace MauiApp1.Calculator
{
    public static class Calculator
    {
        public static double Calculate(double? firstNumber, double? secondNumber, string operation)
        {
            double result = 0;

            switch (operation)
            {
                case "+":
                    result = (double)(firstNumber + secondNumber);
                    break;

                case "-":
                    result = (double)(firstNumber - secondNumber);
                    break;

                case "x":
                    result = (double)(firstNumber * secondNumber);
                    break;

                case "/":
                    result = (double)(firstNumber / secondNumber);
                    break;

                default: return result;
            }
            return result;
        }
    }
}