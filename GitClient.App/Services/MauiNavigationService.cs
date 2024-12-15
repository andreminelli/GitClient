namespace GitClient.App.Services;

public class MauiNavigationService : INavigationService
{
    public Task NavigateToAsync(string route, IDictionary<string, object>? routeParameters = null)
        => routeParameters != null
            ? Shell.Current.GoToAsync(route, routeParameters)
            : Shell.Current.GoToAsync(route);

    public Task PopAsync() => Shell.Current.GoToAsync("..");
}

