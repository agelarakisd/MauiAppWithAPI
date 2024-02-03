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
            _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
            _id = id;
            _ = LoadAccountAsync();
        }

        public IAsyncRelayCommand CopyPasswordCommand => new AsyncRelayCommand<int>(CopyPassword);
        public IAsyncRelayCommand DeleteAccountCommand => new AsyncRelayCommand(DeleteAccount);
        public IAsyncRelayCommand GeneratePasswordCommand => new AsyncRelayCommand<int>(GeneratePassword);
        public IAsyncRelayCommand NavigateBackCommand => new AsyncRelayCommand(NavigateBack);

        public async Task LoadAccountAsync()
        {
            try
            {
                Account = await _apiService.GetAccount(_id);
                OnPropertyChanged(nameof(Account));
            }
            catch (Exception ex)
            {
                HandleException("An error occurred while loading account", ex);
            }
        }

        private async Task CopyPassword(int siteId)
        {
            try 
            {
                string password = Account.Password;
                if (!string.IsNullOrEmpty(password))
                {
                    await Clipboard.SetTextAsync(password);
                    await Application.Current.MainPage?.DisplayAlert("", "Copied to clipboard", "OK");
                }
            }
            catch (Exception ex)
            {
                HandleException("An error occurred while copying password", ex);
            }

        }

        private async Task DeleteAccount()
        {
            if (await EditAccountViewModel.ConfirmAccountDeletion())
            {
                try
                {
                    await _apiService.Delete(_id);
                    await Shell.Current.GoToAsync(nameof(HomeView));
                }
                catch (Exception ex)
                {
                    HandleException("An error occurred while deleting the account", ex);
                }
            }
        }

        private async Task GeneratePassword(int siteId)
        {
            try
            {
                var itemToUpdate = Account;

                if (itemToUpdate == null || Application.Current == null || Application.Current.MainPage == null)
                    return;

                var result = await Application.Current.MainPage.DisplayPromptAsync("Question", "Give the length of the password");

                if (string.IsNullOrEmpty(result) || !int.TryParse(result, out int passwordLength) || passwordLength <= 0)
                {
                    await Application.Current.MainPage.DisplayAlert("Warning", "Enter a valid number greater than 0", "OK");
                    return;
                }

                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                var random = new Random();
                var newPassword = new string(Enumerable.Repeat(chars, passwordLength - 1)
                    .Select(s => s[random.Next(s.Length)]).ToArray());

                var randomIndex = random.Next(newPassword.Length);
                newPassword = newPassword.Insert(randomIndex, "!");

                itemToUpdate.ChangePassword(newPassword);

                await _apiService.Update(itemToUpdate);

                await LoadAccountAsync();
            }
            catch (Exception ex)
            {
                HandleException("An error occurred while generating password", ex);
            }
        }

        private async Task NavigateBack()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(HomeView));
            }
            catch (Exception ex)
            {
                HandleException("Navigation Error", ex);
            }
        }

        private static async Task<bool> ConfirmAccountDeletion()
        {
            return await Application.Current.MainPage.DisplayAlert(
                "Confirm Deletion",
                "Are you sure you want to delete this account?",
                "Yes",
                "No"
            );
        }

        private static void HandleException(string title, Exception ex)
        {
            Application.Current.MainPage?.DisplayAlert(title, ex.Message, "OK");
        }
    }
}
