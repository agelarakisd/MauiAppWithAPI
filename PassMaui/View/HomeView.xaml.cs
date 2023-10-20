using PassMaui.ViewModel;

namespace PassMaui.View;

public partial class HomeView : ContentPage
{
    private HomeViewModel viewModel;
    public HomeView()
    {
        InitializeComponent();
        viewModel = new HomeViewModel();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        viewModel.OnAppearing();
        
    }
}