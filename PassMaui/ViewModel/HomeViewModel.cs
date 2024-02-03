using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PassMaui.View;
using PassMaui.Domain;
using PassMaui.APIServices;
using System.Collections.ObjectModel;

namespace PassMaui.ViewModel
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly IAccountApiService _apiService;
        public IAsyncRelayCommand NavigateToEditAccountCommand => new AsyncRelayCommand<int>(NavigateToEditAccount);
        public IAsyncRelayCommand NavigateToAddAccountPageCommand => new AsyncRelayCommand(NavigateToAddAccountPage);


        private ObservableCollection<Account> _passwords;
        public ObservableCollection<Account> Passwords
        {
            get { return _passwords; }
            set { SetProperty(ref _passwords, value); }
        }

        private int _hiddenId;
        public int HiddenId
        {
            get { return _hiddenId; }
            set
            {
                if (_hiddenId != value)
                {
                    _hiddenId = value;
                    OnPropertyChanged(nameof(HiddenId));
                }
            }
        }

        public HomeViewModel(IAccountApiService apiService) 
        {
            _apiService = apiService;
            _ = LoadAccountsAsync();
        }

        public async Task LoadAccountsAsync()
        {
            try
            {
                var accounts = await _apiService.GetAllAccounts();
                Passwords = new ObservableCollection<Account>(accounts);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task NavigateToEditAccount(int siteId)
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new EditAccountView(_apiService, siteId));
            }
            catch (Exception ex)
            {
                await HandleNavigationException(ex);
            }
        }

        private async Task NavigateToAddAccountPage()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new CreateAccountView(_apiService));
            }
            catch (Exception ex)
            {
                await HandleNavigationException(ex);
            }
        }

        private static async Task HandleNavigationException(Exception ex)
        {
            if (Application.Current != null && Application.Current.MainPage != null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

    }
}