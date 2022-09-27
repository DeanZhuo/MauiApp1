namespace MauiApp1;

public partial class AboutPage : ContentPage
{
    public AboutPage()
    {
        InitializeComponent();
    }

    private async void LearnMore_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is About about)
        {
            // Navigate to the specific URL in the system browser
            await Launcher.Default.OpenAsync(about.MoreInfoUrl);
        }
    }
}