using PassMaui.Models;
using PassMaui.ViewModel;
using SQLite;

namespace PassMaui.View;

public partial class CreateAccountView : ContentPage
{
    public CreateAccountView(SQLiteConnection database)
    {
        InitializeComponent();
        BindingContext = new CreateAccountViewModel(database);
    }
}