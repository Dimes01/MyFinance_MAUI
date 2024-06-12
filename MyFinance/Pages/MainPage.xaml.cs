using MyFinance.Services;
using MyFinance.ViewModels;

namespace MyFinance.Pages;

public partial class MainPage : ContentPage
{
    private MainPageViewModel viewModel;
    public MainPage(MainPageViewModel viewModel)
    {
        InitializeComponent();
        this.viewModel = viewModel;
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
        var parameters = new Dictionary<string, object>
        {
            { nameof(viewModel.Transactions), viewModel.Transactions }
        };
        try
        {
            await Shell.Current.GoToAsync($"../{nameof(ReportPage)}", parameters);
        }
        catch (Exception)
        {
            await DisplayAlert("Отчёт", "Не получилось перейти на страницу с отчётом", "Закрыть");
        }
    }
}
