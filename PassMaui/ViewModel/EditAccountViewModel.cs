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

        [ObservableProperty]
        Account account;

        public IAsyncRelayCommand CopyPasswordCommand => new AsyncRelayCommand(CopyPassword);
        public IAsyncRelayCommand DeleteAccountCommand => new AsyncRelayCommand(DeleteAccount);
        public IAsyncRelayCommand GeneratePasswordCommand => new AsyncRelayCommand(GeneratePassword);
        public IAsyncRelayCommand NavigateBackCommand => new AsyncRelayCommand(NavigateBack);
        public IAsyncRelayCommand UpdateAccountCommand => new AsyncRelayCommand(UpdateAccount);


        public EditAccountViewModel(IAccountApiService apiService, Account acc)
        {
            _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
            Account = acc;
        }


        private async Task CopyPassword()
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

        private async Task UpdateAccount()
        {
            try
            {
                Account.ChangeAccountDetails(Account.Site, Account.Description , Account.Username , Account.Password );

                await _apiService.Update(Account);

                await Application.Current.MainPage?.DisplayAlert("", "Account updated successfully", "OK");
            }
            catch (Exception ex)
            {
                HandleException("An error occurred while updating the account", ex);
            }
        }


        private async Task DeleteAccount()
        {
            if (await ConfirmAccountDeletion())
            {
                try
                {
                    await _apiService.Delete(Account.Id);
                    await Shell.Current.GoToAsync(nameof(HomeView));
                }
                catch (Exception ex)
                {
                    HandleException("An error occurred while deleting the account", ex);
                }
            }
        }

        private async Task GeneratePassword()
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

                var acc = await _apiService.Update(itemToUpdate);
                Account = acc;
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
