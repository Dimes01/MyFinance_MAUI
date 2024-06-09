using MyFinance.Services;
using MyFinance.ViewModels;

namespace MyFinance.Pages;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        //IncomesPieChart.Volumes = new() { 3000 };
        //IncomesPieChart.Volumes = new() { 4000, 3000 };
        //IncomesPieChart.Volumes = new() { 3000, 4000, 5000 };
        //IncomesPieChart.Volumes = new() { 3000, 4000, 5000, 1000 };
        //IncomesPieChart.Volumes = new() { 3000, 4000, 5000, 1000, 500 };
        //IncomesPieChart.Volumes = new() { 3000, 4000, 5000, 1000, 500, 1500 };

        //ExpendituresPieChart.Volumes = new() { 3000 };
        //ExpendituresPieChart.Volumes = new() { 4000, 3000 };
        //ExpendituresPieChart.Volumes = new() { 3000, 4000, 5000 };
        //ExpendituresPieChart.Volumes = new() { 3000, 4000, 5000, 1000 };
        //ExpendituresPieChart.Volumes = new() { 3000, 4000, 5000, 1000, 500 };
        //ExpendituresPieChart.Volumes = new() { 3000, 4000, 5000, 1000, 500, 1500 };
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ReportPage((BindingContext as MainPageViewModel).Transactions));
    }
}
