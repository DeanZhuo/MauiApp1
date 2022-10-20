namespace MauiApp1.Currency;

public partial class CurrencyPage : ContentPage
{
    private double UsdToIdr;
    private double UsdToJpy;

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

        var result = CurrencyService.UpdateValue(CurrencyService.Currencies.JPY, basenumber, UsdToIdr, UsdToJpy);
        UpdateEditor(result);
    }

    private void USDNum_Completed(object sender, EventArgs e)
    {
        double basenumber;

        if (sender == null || ((Entry)sender).Text.Equals(""))
            basenumber = 0;
        else
            basenumber = Convert.ToDouble(((Entry)sender).Text);
        var result = CurrencyService.UpdateValue(CurrencyService.Currencies.USD, basenumber, UsdToIdr, UsdToJpy);
        UpdateEditor(result);
    }

    private void IDRNum_Completed(object sender, EventArgs e)
    {
        double basenumber;

        if (sender == null || ((Entry)sender).Text.Equals(""))
            basenumber = 0;
        else
            basenumber = Convert.ToDouble(((Entry)sender).Text);
        var result = CurrencyService.UpdateValue(CurrencyService.Currencies.IDR, basenumber, UsdToIdr, UsdToJpy);
        UpdateEditor(result);
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
            DateTime dateTime = new(1970, 1, 1);
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
        UsdToIdr = IDR;
        UsdToJpy = JPY;

        UpdateEditor(new List<string> { "0.00", "0.00", "0.00" });

        IdrValue.Text = $"USD in IDR = {UsdToIdr}";
        JpyValue.Text = $"USD in JPY = {UsdToJpy}";
        BusyIndicator.IsRunning = false;
    }

    private void UpdateEditor(List<string> result)
    {
        IDRNum.Text = result[0];
        USDNum.Text = result[1];
        JPYNum.Text = result[2];
    }
}