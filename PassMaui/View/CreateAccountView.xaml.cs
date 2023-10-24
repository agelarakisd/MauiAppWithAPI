
using PassMaui.ViewModel;
using SQLite;

namespace PassMaui.View;

public partial class CreateAccountView : ContentPage
{
    public CreateAccountView()
    {
        InitializeComponent();
        var database = new SQLiteConnection(@"C:\sqlite\passmauidb.db");
        BindingContext = new CreateAccountViewModel(database);
    }
}