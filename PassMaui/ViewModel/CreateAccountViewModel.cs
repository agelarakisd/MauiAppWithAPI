using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PassMaui.APIServices;
using PassMaui.Domain;
using PassMaui.View;
using System.Text;

namespace PassMaui.ViewModel
{
    public partial class CreateAccountViewModel : ObservableObject
    {
        private readonly IAccountApiService _apiService;

        public IAsyncRelayCommand NavigateBackAsyncCommand => new AsyncRelayCommand(NavigateBackAsync);
        public IAsyncRelayCommand CreateAccountAsyncCommand => new AsyncRelayCommand(CreateAccountAsync);

        public CreateAccountViewModel(IAccountApiService apiService)
        {
            _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
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

        private async Task NavigateBackAsync()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(HomeView));
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private async Task CreateAccountAsync()
        {
            try
            {
                if (int.TryParse(PasswordLength, out int passwordLength) && passwordLength > 0)
                {
                    string password = GenerateRandomPassword(passwordLength);
                    var newAccount = Account.Create(Site, Description, Username, password);
                    await SaveAccountToDatabaseAsync(newAccount);
                    ClearFields();
                    await DisplayAlert("Success!", "Account Created.");
                    await Shell.Current.GoToAsync(nameof(HomeView));
                }
                else
                {
                    await DisplayAlert("Error", "Password length is not a valid number.");
                }
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private async Task SaveAccountToDatabaseAsync(Account account)
        {
            await _apiService.Create(account).ConfigureAwait(false);
        }

        private static async Task DisplayAlert(string title, string message)
        {
            await Application.Current?.MainPage?.DisplayAlert(title, message, "OK");
        }

        private static async Task HandleErrorAsync(Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex}");

            await DisplayAlert("Error", ex.Message);
        }

        private void ClearFields()
        {
            Site = Description = Username = PasswordLength = string.Empty;
        }

        private static string GenerateRandomPassword(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var newPassword = new StringBuilder(length);
            for (int i = 0; i < length - 1; i++) 
            {
                newPassword.Append(chars[random.Next(chars.Length)]);
            }

            var randomIndex = random.Next(newPassword.Length);
            newPassword.Insert(randomIndex, "!");

            return newPassword.ToString();
        }
    }
}
