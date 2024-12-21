using CommunityToolkit.Maui;

using GitClient.App.Dialogs.Cloning;
using GitClient.App.Pages.Main;
using GitClient.App.Services;
using GitClient.Core;

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
        RegisterServices(builder.Services);

        return builder.Build();
    }

    private static void RegisterViews(IServiceCollection services)
    {
        services
            .AddSingletonViewModelWithRoute<MainViewModel, MainPage>()
            .AddTransientViewModelWithRoute<CloningViewModel, CloningDialog>();
    }

    private static void RegisterServices(IServiceCollection services)
    {
        services
            .AddSingleton<INavigationService, MauiNavigationService>()
            .AddSingleton<IRepositoryService, RepositoryService>();
    }
}
