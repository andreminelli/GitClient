using CommunityToolkit.Mvvm.ComponentModel;

namespace GitClient.App.Pages.Main;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private string? _text;

    public MainViewModel()
    {
        _text = "Bem vindo ao MVVM";
    }
}
