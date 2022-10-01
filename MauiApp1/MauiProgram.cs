namespace MauiApp1;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton<AllNotesPage>();
        builder.Services.AddTransient<NotePage>();

        builder.Services.AddSingleton<CalculatorPage>();

        builder.Services.AddSingleton<CurrencyPage>();

        return builder.Build();
    }
}