namespace GitClient.App.Dialogs.Cloning;

public partial class CloningDialog : ContentPage
{
	public CloningDialog(CloningViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
	}
}