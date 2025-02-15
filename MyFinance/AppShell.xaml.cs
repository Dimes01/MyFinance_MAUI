﻿using MyFinance.Pages;

namespace MyFinance
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(ExpendituresPage), typeof(ExpendituresPage));
            Routing.RegisterRoute(nameof(IncomesPage), typeof(IncomesPage));
            Routing.RegisterRoute(nameof(ReportPage), typeof(ReportPage));
        }
    }
}
