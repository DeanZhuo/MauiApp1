namespace MauiApp1;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        Barrel.ApplicationId = AppInfo.Name;
        MainPage = new AppShell();
    }
}