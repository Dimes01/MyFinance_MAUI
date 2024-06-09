using Microsoft.Extensions.Logging;
using MyFinance.Pages;
using MyFinance.Services;
using MyFinance.ViewModels;

namespace MyFinance;

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

#if DEBUG
		builder.Logging.AddDebug();
#endif

        builder.RegisterPages();
        builder.RegisterViewModels();
        builder.RegisterOtherServices();

        return builder.Build();
    }

    private static MauiAppBuilder RegisterPages(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<LoginPage>();
        mauiAppBuilder.Services.AddSingleton<MainPage>();
        mauiAppBuilder.Services.AddSingleton<IncomesPage>();
        mauiAppBuilder.Services.AddSingleton<ExpendituresPage>();
        mauiAppBuilder.Services.AddTransient<ActionPage>();
        return mauiAppBuilder;
    }

    private static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<LoginPageViewModel>();
        mauiAppBuilder.Services.AddSingleton<MainPageViewModel>();
        mauiAppBuilder.Services.AddTransient<TransactionsViewModel>();
        return mauiAppBuilder;
    }

    private static MauiAppBuilder RegisterOtherServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<AuthService>();
        mauiAppBuilder.Services.AddSingleton<ShareData>();
        return mauiAppBuilder;
    }
}
