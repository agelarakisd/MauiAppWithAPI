using PassMaui.APIServices;
using PassMaui.Domain;
using PassMaui.ViewModel;

namespace PassMaui.View;

public partial class EditAccountView : ContentPage
{
    private readonly EditAccountViewModel _viewModel;
    
    public EditAccountView(IAccountApiService apiService, Account acc)
    {
        InitializeComponent();
        _viewModel = new EditAccountViewModel(apiService,acc);
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}