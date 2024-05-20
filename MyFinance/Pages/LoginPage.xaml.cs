using MyFinance.Services;
using MyFinance.ViewModels;

namespace MyFinance.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
		BindingContext = new LoginPageViewModel(new AuthService());
	}


}