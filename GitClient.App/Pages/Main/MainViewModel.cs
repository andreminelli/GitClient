using IO = System.IO;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using GitClient.App.Services;
using GitClient.App.Dialogs.Cloning;

namespace GitClient.App.Pages.Main;

public partial class MainViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(CloneCommand))]
    private string _url;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(CloneCommand))]
    private string _path;

    public MainViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;

        _url = "";
        _path = IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "repos");
    }

    [RelayCommand(CanExecute = nameof(CanClone))]
    public async Task Clone()
    {
        await _navigationService.NavigateToAsync(
            nameof(CloningViewModel),
            new Dictionary<string, object>
            {
                { nameof(Url), Url },
                { nameof(Path), Path }
            });
    }

    private bool CanClone() 
        => Uri.IsWellFormedUriString(Url, UriKind.Absolute) && !string.IsNullOrWhiteSpace(Path);
}
