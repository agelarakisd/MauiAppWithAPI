using PassMaui.ViewModel;

namespace PassMaui.View;

public partial class ContentBoxesView : ContentView
{
    public ContentBoxesView()
    {
        InitializeComponent();
    }

    private void DeleteButton_Clicked(object sender, EventArgs e)
    {
        
    }

    private async void GenPassButton_Clicked(object sender, EventArgs e)
    {
        bool answer = await Application.Current.MainPage.DisplayAlert("Warning!", "Would you like to generate a new password?", "Yes", "No");
        if (answer)
        {
            var result = await Application.Current.MainPage.DisplayPromptAsync("Question","Give length of the password");
            if (result != string.Empty)
            {
                string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!";
                var random = new Random();
                string password = new string(Enumerable.Repeat(chars, Int32.Parse(result))
                  .Select(s => s[random.Next(s.Length)]).ToArray());
                PasswordEntry.Text = password;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "Enter a valid number", "OK");
            }
        }
    }

    private void ActionButton_Clicked(object sender, EventArgs e)
    {
        PasswordEntry.IsPassword = !PasswordEntry.IsPassword;

        ActionButton.Text = PasswordEntry.IsPassword ? "Hide Password" : "Show Password";
    }
}