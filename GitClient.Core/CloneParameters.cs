namespace GitClient.Core;

public class CloneParameters
{
    static readonly Action<string> nullReporter = (string _) => { };

    public required string Url { get; set; }
    public required string Path { get; set; }
    public Action<string> ReportProgress { get; set; } = nullReporter;
}