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

        public static string ConvertString(double? input)
        {
            if (input == null || input == 0)
                return "0";

            double entryDouble = (double)input;
            string entryString = entryDouble.ToString("N7");

            while (entryString.Contains('.') && entryString.EndsWith('0'))
                entryString = entryString.TrimEnd('0');
            if (entryString.EndsWith('.'))
                entryString = entryString.TrimEnd('.');

            return entryString;
        }
    }
}