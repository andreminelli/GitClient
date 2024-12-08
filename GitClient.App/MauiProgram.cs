using CommunityToolkit.Maui;

using GitClient.App.Pages.Main;

using Microsoft.Extensions.Logging;

namespace GitClient.App;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        RegisterViews(builder.Services);
        RegisterViewModels(builder.Services);

        return builder.Build();
    }

    private static void RegisterViews(IServiceCollection services) => services.AddSingleton<MainPage>();

    private static void RegisterViewModels(IServiceCollection services) => services.AddSingleton<MainViewModel>();
}
