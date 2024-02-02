using PassMaui.APIServices;
using PassMaui.ViewModel;

namespace PassMaui.View;

public partial class EditAccountView : ContentPage
{
    private readonly EditAccountViewModel _viewModel;
    
    public EditAccountView(IAccountApiService apiService, int id)
    {
        InitializeComponent();
        _viewModel = new EditAccountViewModel(apiService,id);
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}