using System.Diagnostics;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using GitClient.App.Services;
using GitClient.Core;

namespace GitClient.App.Dialogs.Cloning;

[QueryProperty(nameof(Url), nameof(Url))]
[QueryProperty(nameof(Path), nameof(Path))]
public partial class CloningViewModel : ObservableObject
{
    private readonly IRepositoryService _repositoryService;
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(CloseCommand))]
    private bool _finished;

    [ObservableProperty]
    private string _statusMessage = "";

    public string? Url { get; set; }

    public string? Path { get; set; }

    public CloningViewModel(IRepositoryService repositoryService, INavigationService navigationService)
    {
        _repositoryService = repositoryService;
        _navigationService = navigationService;
    }

    [RelayCommand]
    public async Task Start()
    {
        if (Url is null)
        {
            throw new ArgumentNullException(nameof(Url));
        }
        if (Path is null) 
        { 
            throw new ArgumentNullException(nameof(Path));
        }

        try
        {
            StatusMessage = "Starting to clone";
            var repo = await _repositoryService.Clone(
                new CloneParameters
                {
                    Url = Url,
                    Path = Path,
                    ReportProgress = progress => StatusMessage = progress
                });
            StatusMessage = "Cloned";
            Debug.WriteLine(repo);
        }
        catch(Exception e)
        {
            StatusMessage = e.Message;
            Debug.WriteLine(e);
        }
        finally
        {
            Finished = true;
        }
    }

    [RelayCommand(CanExecute = nameof(CanClose))]
    public Task Close()
    {
        return _navigationService.PopAsync();
    }

    public bool CanClose() => Finished;
}

