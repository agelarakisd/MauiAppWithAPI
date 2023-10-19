using PassMaui.ViewModel;

namespace PassMaui.View;

public partial class HomeView : ContentPage
{
    public HomeView()
    {
        InitializeComponent();
        BindingContext = new HomeViewModel();
    }
}