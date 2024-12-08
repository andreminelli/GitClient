using CommunityToolkit.Mvvm.ComponentModel;

using GitClient.Core;

namespace GitClient.App.Pages.Main;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private string? _text;

    public MainViewModel()
    {
        _text = RepositoryService.GetStatus(@"E:\source\GitClient");
    }
}
