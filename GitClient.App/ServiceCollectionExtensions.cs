using System.ComponentModel;

using CommunityToolkit.Maui;

namespace GitClient.App;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSingletonViewModelWithRoute<TViewModel, TView>(this IServiceCollection services)
        where TViewModel : class, INotifyPropertyChanged
        where TView : BindableObject
    {
        services.AddSingleton<TView, TViewModel>();
        Routing.RegisterRoute(typeof(TViewModel).Name, typeof(TView));
        return services;
    }

    public static IServiceCollection AddTransientViewModelWithRoute<TViewModel, TView>(this IServiceCollection services)
        where TViewModel : class, INotifyPropertyChanged
        where TView : BindableObject
    {
        services.AddTransient<TView, TViewModel>();
        Routing.RegisterRoute(typeof(TViewModel).Name, typeof(TView));
        return services;
    }
}

