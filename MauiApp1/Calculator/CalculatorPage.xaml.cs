namespace MauiApp1.Calculator;

public partial class CalculatorPage : ContentPage
{
    public CalculatorPage()
    {
        InitializeComponent();
        ClearClicked(this, EventArgs.Empty);
    }

    private readonly string NEUTRAL = "NEUTRAL";
    private readonly string ENTER_ONE = "ENTER_ONE";
    private readonly string ENTER_TWO = "ENTER_TWO";

    private double? firstNumber;
    private double? secondNumber;
    private string operation;
    private string state;
    private bool insertdot;

    private void ClearClicked(object sender, EventArgs e)
    {
        EquationText.Text = "0";
        InsertText.Text = "0";
        insertdot = false;
        ClearState();
    }

    private void ClearState()
    {
        firstNumber = null;
        secondNumber = null;
        operation = string.Empty;
        state = NEUTRAL;
    }

    private void SelectNumber(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        string entry;
        if (insertdot)
        {
            entry = "." + button.Text;
            insertdot = false;
        }
        else
            entry = button.Text;

        if (state.Equals(NEUTRAL))
        {
            state = ENTER_ONE;
            firstNumber = Convert.ToDouble(entry);
            InsertText.Text = Convert.ToString(firstNumber);
            EquationText.Text = "0";
        }
        else if (state.Equals(ENTER_ONE))
        {
            entry = Convert.ToString(firstNumber) + entry;
            firstNumber = Convert.ToDouble(entry);
            InsertText.Text = Convert.ToString(firstNumber);
        }
        else if (state.Equals(ENTER_TWO))
        {
            entry = Convert.ToString(secondNumber) + entry;
            secondNumber = Convert.ToDouble(entry);
            InsertText.Text = Convert.ToString(secondNumber);
        }
    }

    private void SelectOperator(object sender, EventArgs e)
    {
        Button button = (Button)sender;

        if (state.Equals(NEUTRAL) || state.Equals(ENTER_ONE))
        {
            state = ENTER_TWO;
            firstNumber ??= 0;
        }
        else if (state.Equals(ENTER_TWO) && secondNumber != null)
        {
            Calculate(button, EventArgs.Empty);
        }
        operation = button.Text;
        EquationText.Text = $"{firstNumber} {operation}";
    }

    private void Calculate(object sender, EventArgs e)
    {
        EquationText.Text = $"{firstNumber} {operation} {secondNumber}";
        firstNumber = Calculator.Calculate(firstNumber, secondNumber, operation);
        InsertText.Text = Convert.ToString(firstNumber);
        secondNumber = null;

        Button button = (Button)sender;
        if (button.Text.Equals("="))
        {
            ClearState();
        }
    }

    private void SelectDot(object sender, EventArgs e)
    {
        insertdot = true;
        InsertText.Text += ".";
    }

    private void ChangeValue(object sender, EventArgs e)
    {
        if (state.Equals(ENTER_ONE) && firstNumber != null)
        {
            firstNumber *= -1;
            InsertText.Text = Convert.ToString(firstNumber);
        }
        if (state.Equals(ENTER_TWO) && secondNumber != null)
        {
            secondNumber *= -1;
            InsertText.Text = Convert.ToString(secondNumber);
        }
    }

    private void SelectPercent(object sender, EventArgs e)
    {
        if (state.Equals(ENTER_TWO) && secondNumber != null)
        {
            secondNumber = (double)(firstNumber * secondNumber / 100);
            Calculate(sender, EventArgs.Empty);
        }
    }
}