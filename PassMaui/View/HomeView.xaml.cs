using PassMaui.ViewModel;

namespace PassMaui.View;

public partial class HomeView : ContentPage
{
    private readonly HomeViewModel _viewModel;
    public HomeView()
    {
        InitializeComponent();
        _viewModel = new HomeViewModel();
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.OnAppearing();
        
    }
}