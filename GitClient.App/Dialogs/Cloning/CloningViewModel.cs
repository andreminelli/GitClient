using System.Diagnostics;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using GitClient.App.Services;

using LibGit2Sharp;

namespace GitClient.App.Dialogs.Cloning;

[QueryProperty(nameof(Url), nameof(Url))]
[QueryProperty(nameof(Path), nameof(Path))]
public partial class CloningViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(CloseCommand))]
    private bool _finished;

    [ObservableProperty]
    private string _statusMessage = "";

    public string? Url { get; set; }

    public string? Path { get; set; }

    public CloningViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    [RelayCommand]
    public async Task Start()
    {
        try
        {
            await Task.Run(() =>
            {
                var repo = Repository.Clone(
                    Url,
                    Path,
                    new CloneOptions(
                        new FetchOptions
                        {
                            OnProgress = OnProgress,
                            OnTransferProgress = OnTransferProgress
                        })
                    {
                        OnCheckoutProgress = OnCheckoutProgress
                    });
                StatusMessage = "Cloned";
                Debug.WriteLine(repo);
            });
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

    private void OnCheckoutProgress(string path, int completedSteps, int totalSteps)
    {
        Debug.WriteLine($"{completedSteps} {totalSteps}");
    }

    private bool OnProgress(string serverProgressOutput)
    {
        StatusMessage = serverProgressOutput.TrimEnd().Split("\r").Last();
        //Thread.Sleep(1000);
        return true;
    }

    private bool OnTransferProgress(TransferProgress progress)
    {
        Debug.WriteLine($"Received: {progress.ReceivedObjects} / {progress.TotalObjects}; Indexed: {progress.IndexedObjects}");
        return true;
    }

}

