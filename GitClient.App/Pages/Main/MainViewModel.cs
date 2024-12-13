using System.Diagnostics;
using IO = System.IO;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using LibGit2Sharp;

namespace GitClient.App.Pages.Main;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private string _url;

    [ObservableProperty]
    private string _path;

    public MainViewModel()
    {
        _url = "";
        _path = IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "repos");
    }

    [RelayCommand]
    public void Clone()
    {
        var repo = Repository.Clone(Url, Path, new CloneOptions(
            new FetchOptions 
            { 
                OnProgress = OnProgress,
                OnTransferProgress = OnTransferProgress 
            })
            {
                OnCheckoutProgress = OnCheckoutProgress
            }
        );
        Debug.WriteLine(repo);
    }

    private void OnCheckoutProgress(string path, int completedSteps, int totalSteps)
    {
        Debug.WriteLine($"{completedSteps} {totalSteps}");
    }

    private bool OnProgress(string serverProgressOutput)
    {
        Debug.WriteLine(serverProgressOutput);
        return true;
    }

    private bool OnTransferProgress(TransferProgress progress)
    {
        Debug.WriteLine($"Received: {progress.ReceivedObjects} / {progress.TotalObjects}; Indexed: {progress.IndexedObjects}");
        return true;
    }
}
