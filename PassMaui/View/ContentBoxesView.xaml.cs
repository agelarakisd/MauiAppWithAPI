namespace PassMaui.View;

public partial class ContentBoxesView : ContentView
{
	public ContentBoxesView()
	{
		InitializeComponent();
	}

    private void ActionButton_Clicked(object sender, EventArgs e)
    {
        PasswordEntry.Text = "MyPassword";
    }
}