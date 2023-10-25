using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PassMaui.Models;
using PassMaui.View;
using SQLite;

namespace PassMaui.ViewModel
{
    public partial class CreateAccountViewModel : ObservableObject
    {
        private readonly SQLiteConnection _database;

        public CreateAccountViewModel(SQLiteConnection db)
        {
            _database = db;
        }

        private string _site;
        public string Site
        {
            get => _site;
            set => SetProperty(ref _site, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private string _username;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private string _passwordLength;
        public string PasswordLength
        {
            get => _passwordLength;
            set => SetProperty(ref _passwordLength, value);
        }

        [RelayCommand]
        private static async Task NavigateBack()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(HomeView));
            }
            catch (Exception ex)
            {
                if (Application.Current != null)
                    if (Application.Current.MainPage != null)
                        await Application.Current.MainPage.DisplayAlert("Navigation Error", ex.Message, "OK");
            }
        }

        [RelayCommand]
        private async Task CreateAccount()
        {
            if (int.TryParse(PasswordLength, out int passwordLength) && passwordLength > 0)
            {
                string password = GenerateRandomPassword(passwordLength);

                var newAccount = new PasswordInfo
                {
                    Site = Site,
                    Description = Description,
                    Username = Username,
                    Password = password
                };

                SaveAccountToDatabase(newAccount);

                Site = string.Empty;
                Description = string.Empty;
                Username = string.Empty;
                PasswordLength = string.Empty;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Password length is not a valid number.", "OK");
            }
        }



        private static string GenerateRandomPassword(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var newPassword = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            var randomIndex = random.Next(newPassword.Length);
            newPassword = newPassword.Insert(randomIndex, "!");

            return newPassword;
        }

        private void SaveAccountToDatabase(PasswordInfo account)
        {
            _database.Insert(account);

            Site = string.Empty;
            Description = string.Empty;
            Username = string.Empty;
            PasswordLength = string.Empty;
        }
    }
}
