
using PassMaui.ViewModel;
using SQLite;

namespace PassMaui.View;

public partial class CreateAccountView : ContentPage
{
    public CreateAccountView()
    {
        InitializeComponent();
        var database = new SQLiteConnection(@"C:\SQL2022\passmauidb.db");
        BindingContext = new CreateAccountViewModel(database);
    }
}