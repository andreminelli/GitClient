using LibGit2Sharp;

namespace GitClient.Core;

public class RepositoryService : IRepositoryService
{
    public Task<string> GetStatus(string localRepoPath)
    {
        var status = string.Empty;
        try
        {
            using var repository = new Repository(localRepoPath);
            status = $"Atualmente no repositorio: {repository.Info.WorkingDirectory}";
        }
        catch (LibGit2SharpException)
        {
            throw;
        }
        return Task.FromResult(status);
    }

    public Task<string> Clone(CloneParameters options)
    {
        return Task.Run(() => Repository.Clone(
                        options.Url,
                        options.Path,
                        new CloneOptions(
                            new FetchOptions
                            {
                                OnProgress = progress => 
                                    { 
                                        options.ReportProgress(progress); 
                                        return true; 
                                    },
                                OnTransferProgress = progress => 
                                    { 
                                        options.ReportProgress($"Received: {progress.ReceivedObjects} / {progress.TotalObjects}; Indexed: {progress.IndexedObjects}");
                                        return true;
                                    }
                            })
                        {
                            OnCheckoutProgress = (_, _, _) => { }
                        }));
    }
}
