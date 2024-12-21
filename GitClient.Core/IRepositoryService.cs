namespace GitClient.Core;

public interface IRepositoryService
{
    Task<string> Clone(CloneParameters options);
    Task<string> GetStatus(string localRepoPath);
}