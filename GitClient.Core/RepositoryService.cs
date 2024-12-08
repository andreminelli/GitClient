using LibGit2Sharp;

namespace GitClient.Core;

public class RepositoryService
{
    public static string GetStatus(string localRepoPath)
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
        return status;
    }
}
