
using PassMaui.APIServices;
using PassMaui.ViewModel;

namespace PassMaui.View;

public partial class CreateAccountView : ContentPage
{
    public CreateAccountView(IAccountApiService apiService)
    {
        InitializeComponent();
        BindingContext = new CreateAccountViewModel(apiService);
    }
}