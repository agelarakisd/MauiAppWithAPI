using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PassMaui.View;
using PassMaui.Domain;
using PassMaui.APIServices;

namespace PassMaui.ViewModel
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly IAccountApiService _apiService;

        [ObservableProperty]
        List<Account> passwords;

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
            LoadPasswordsAsync();
        }

        public async void LoadPasswordsAsync()
        {
            try
            {
                var passwords = await _apiService.GetAllAccounts();
                Passwords = passwords;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [RelayCommand]
        private async Task NavigateToEditAccount(int siteId)
        {
            try 
            {
                await Application.Current.MainPage.Navigation.PushAsync(new EditAccountView(_apiService,siteId));
            }
            catch (Exception ex) 
            {
                if (Application.Current != null)
                    if (Application.Current.MainPage != null)
                        await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }



        [RelayCommand]
        private async Task NavigateToAddAccountPage()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new CreateAccountView(_apiService));
            }
            catch (Exception ex)
            {
                if (Application.Current != null)
                    if (Application.Current.MainPage != null)
                        await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

    }
}