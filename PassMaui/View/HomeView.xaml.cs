using PassMaui.APIServices;
using PassMaui.ViewModel;

namespace PassMaui.View;

public partial class HomeView : ContentPage
{
    private readonly HomeViewModel _viewModel;
    public HomeView(IAccountApiService apiService)
    {
        InitializeComponent();
        _viewModel = new HomeViewModel(apiService);
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing(); 
    }
}