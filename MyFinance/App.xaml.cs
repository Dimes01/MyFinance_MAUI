using MyFinance.Pages;

namespace MyFinance
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            UserAppTheme = AppTheme.Light;
            MainPage = new NavigationPage(new IncomesPage());
        }
    }
}
