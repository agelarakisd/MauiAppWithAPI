using PassMaui.ViewModel;

namespace PassMaui.View;

public partial class HomeView : ContentPage
{
	public HomeView(PasswordsViewModel model)
	{
		InitializeComponent();
		BindingContext = model;
	}
}