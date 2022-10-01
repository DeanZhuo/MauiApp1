namespace MauiApp1;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        CurrencyService.InitCurrencyService();
        Routing.RegisterRoute(nameof(NotePage), typeof(NotePage));
    }
}