using MyFinance.Services;

namespace MyFinance.Pages;

public partial class LoadingPage : ContentPage
{
    private readonly AuthService authService;

    public LoadingPage(AuthService authService)
	{
		InitializeComponent();
        this.authService = authService;
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        // https://youtu.be/97G-XkuENYE?t=1130 
        if (await authService.Login("", ""))
        {
            await Shell.Current.GoToAsync($"//Tabs/{nameof(MainPage)}");
        }
        else
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}