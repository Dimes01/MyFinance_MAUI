namespace MyFinance.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            //IncomesPieChart.Volumes = new() { 3000 };
            //IncomesPieChart.Volumes = new() { 4000, 3000 };
            //IncomesPieChart.Volumes = new() { 3000, 4000, 5000 };
            //IncomesPieChart.Volumes = new() { 3000, 4000, 5000, 1000 };
            //IncomesPieChart.Volumes = new() { 3000, 4000, 5000, 1000, 500 };
            IncomesPieChart.Volumes = new() { 3000, 4000, 5000, 1000, 500, 1500 };

            //ExpendituresPieChart.Volumes = new() { 3000 };
            //ExpendituresPieChart.Volumes = new() { 4000, 3000 };
            //ExpendituresPieChart.Volumes = new() { 3000, 4000, 5000 };
            //ExpendituresPieChart.Volumes = new() { 3000, 4000, 5000, 1000 };
            ExpendituresPieChart.Volumes = new() { 3000, 4000, 5000, 1000, 500 };
            //ExpendituresPieChart.Volumes = new() { 3000, 4000, 5000, 1000, 500, 1500 };
        }
    }

}
