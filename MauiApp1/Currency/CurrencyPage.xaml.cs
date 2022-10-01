namespace MauiApp1.Currency;

public partial class CurrencyPage : ContentPage
{
    private double usdidr;
    private double usdjpy;

    public CurrencyPage()
    {
        InitializeComponent();
        BindingContext = this;

        UpdateCurrency(this, EventArgs.Empty);

        IDRNum.Completed += IDRNum_Completed;
        USDNum.Completed += USDNum_Completed;
        JPYNum.Completed += JPYNum_Completed;
    }

    private void JPYNum_Completed(object sender, EventArgs e)
    {
        double basenumber;

        if (sender == null || ((Entry)sender).Text.Equals(""))
            basenumber = 0;
        else
            basenumber = Convert.ToDouble(((Entry)sender).Text);
        double toUsd = basenumber / usdjpy;
        double toIdr = toUsd * usdidr;

        JPYNum.Text = basenumber.ToString("N");
        USDNum.Text = toUsd.ToString("N");
        IDRNum.Text = toIdr.ToString("N");
    }

    private void USDNum_Completed(object sender, EventArgs e)
    {
        double basenumber;

        if (sender == null || ((Entry)sender).Text.Equals(""))
            basenumber = 0;
        else
            basenumber = Convert.ToDouble(((Entry)sender).Text);
        double toIdr = basenumber * usdidr;
        double toJpy = basenumber * usdjpy;

        USDNum.Text = basenumber.ToString("N");
        IDRNum.Text = toIdr.ToString("N");
        JPYNum.Text = toJpy.ToString("N");
    }

    private void IDRNum_Completed(object sender, EventArgs e)
    {
        double basenumber;

        if (sender == null || ((Entry)sender).Text.Equals(""))
            basenumber = 0;
        else
            basenumber = Convert.ToDouble(((Entry)sender).Text);
        double toUsd = basenumber / usdidr;
        double toJpy = toUsd * usdjpy;

        IDRNum.Text = basenumber.ToString("N");
        USDNum.Text = toUsd.ToString("N");
        JPYNum.Text = toJpy.ToString("N");
    }

    private async void UpdateCurrency(object sender, EventArgs e)
    {
        try
        {
            bool update = false;
            if (sender is Button)
                update = true;
            BusyIndicator.IsRunning = true;
            CurrencyResponse result = await CurrencyService.GetCurrency(update);
            TimeZoneInfo zoneInfo = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            DateTime dateTime = new DateTime(1970, 1, 1);
            DateTime addseconds = dateTime.AddSeconds(result.timestamp);
            DateTime localtime = TimeZoneInfo.ConvertTimeFromUtc(addseconds, zoneInfo);
            Timestamp.Text = $"Last Updated: {localtime:F}";
            UpdateText(result.rates.IDR, result.rates.JPY);
        }
        catch (Exception ex)
        {
            BusyIndicator.IsRunning = false;
            Debug.WriteLine($"Cannot update currency: {ex.Message}");
            await Shell.Current.DisplayAlert("Error while updating value", ex.Message + "\nPlease try again later", "Ok");
        }
    }

    private void UpdateText(double IDR, double JPY)
    {
        usdidr = IDR;
        usdjpy = JPY;

        IDRNum.Text = "0";
        USDNum.Text = "0";
        JPYNum.Text = "0";

        IdrValue.Text = $"USD in IDR = {usdidr}";
        JpyValue.Text = $"USD in JPY = {usdjpy}";
        BusyIndicator.IsRunning = false;
    }
}