using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PassMaui.APIServices;
using PassMaui.Domain;
using PassMaui.View;

namespace PassMaui.ViewModel
{
    public partial class EditAccountViewModel : ObservableObject
    {
        private readonly IAccountApiService _apiService;
        private readonly int _id;

        [ObservableProperty]
        Account account;

        public EditAccountViewModel(IAccountApiService apiService, int id)
        {
            _apiService = apiService;
            _id = id;
            LoadPasswordsAsync();
        }

        public async void LoadPasswordsAsync()
        {
            try
            {
                var password = await _apiService.GetAccount(_id);
                Account = password;
                OnPropertyChanged(nameof(Account));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [RelayCommand]
        private async Task CopyPassword(int siteId)
        {
            try 
            {
                string password = Account.Password;
                if (!string.IsNullOrEmpty(password))
                {
                    await Clipboard.SetTextAsync(password);
                    if (Application.Current != null)
                        if (Application.Current.MainPage != null)
                            await Application.Current.MainPage.DisplayAlert("", "Copied to clipboard", "OK");
                }
            }
            catch (Exception ex)
            {
                if (Application.Current != null)
                    if (Application.Current.MainPage != null)
                        await Application.Current.MainPage.DisplayAlert("An error occurred while copying password", ex.Message, "OK");
            }

        }

        [RelayCommand]
        private async Task DeleteAccount()
        {
            try
            {
                await _apiService.Delete(_id);
                await Shell.Current.GoToAsync(nameof(HomeView));
            }
            catch (Exception ex)
            {
                if (Application.Current != null && Application.Current.MainPage != null)
                {
                    await Application.Current.MainPage.DisplayAlert("An error occurred while deleting the account", ex.Message, "OK");
                }
            }
        }

        [RelayCommand]
        private async Task GeneratePassword(int siteId)
        {
            var itemToUpdate = Account;

            if (itemToUpdate != null)
            {
                if (Application.Current != null)
                {
                    if (Application.Current.MainPage != null)
                    {
                        var result = await Application.Current.MainPage.DisplayPromptAsync("Question", "Give the length of the password");

                        if (!string.IsNullOrEmpty(result) && int.TryParse(result, out int passwordLength) && passwordLength > 0)
                        {
                            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                            var random = new Random();
                            var newPassword = new string(Enumerable.Repeat(chars, passwordLength - 1)
                                .Select(s => s[random.Next(s.Length)]).ToArray());

                            var randomIndex = random.Next(newPassword.Length);
                            newPassword = newPassword.Insert(randomIndex, "!");

                            itemToUpdate.ChangePassword(newPassword);

                            await _apiService.Update(itemToUpdate);

                            LoadPasswordsAsync();
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Warning", "Enter a valid number greater than 0", "OK");
                        }
                    }
                }
            }
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
    }
}
