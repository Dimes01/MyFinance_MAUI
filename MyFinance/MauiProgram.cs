using Microsoft.Extensions.Logging;
using MyFinance.Pages;
using MyFinance.Services;
using MyFinance.ViewModels;

namespace MyFinance
{
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

            builder.Services.AddTransient<AuthService>();
            builder.Services.AddSingleton<ShareData>();
            builder.Services.AddSingleton<MainPageViewModel>();

            return builder.Build();
        }
    }
}
