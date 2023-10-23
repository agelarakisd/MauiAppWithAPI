using CommunityToolkit.Mvvm.ComponentModel;
using PassMaui.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Security;
using System.Windows.Input;

namespace PassMaui.ViewModel
{

    public partial class PasswordsViewModel : ObservableObject
    {
        private bool isPasswordHidden;

        private string toggleButtonText;

        public string ToggleButtonText
        {
            get { return toggleButtonText; }
            set
            {
                toggleButtonText = value;
                OnPropertyChanged();
            }
        }

        public bool IsPasswordHidden
        {
            get { return isPasswordHidden; }
            set
            {
                isPasswordHidden = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ActionButtonText));
            }
        }
        public string ActionButtonText => IsPasswordHidden ? "Show Password" : "Hide Password";
        public ObservableCollection<PasswordInfo> Passwords { get; set; }
        public ICommand GeneratePasswordCommand { get; set; }
        public ICommand TogglePasswordVisibilityCommand { get; }

        private string _passwordEntryText;
        public string PasswordEntryText
        {
            get { return _passwordEntryText; }
            set { SetProperty(ref _passwordEntryText, value); }
        }


        public PasswordsViewModel()
        {
            LoadPasswords();
            GeneratePasswordCommand = new Command(async () => await GeneratePasswordAsync());
            TogglePasswordVisibilityCommand = new Command(TogglePasswordVisibility);
            ToggleButtonText = IsPasswordHidden ? "Show Password" : "Hide Password";

        }

        public void TogglePasswordVisibility()
        {
            IsPasswordHidden = !IsPasswordHidden;
            OnPropertyChanged(nameof(IsPasswordHidden));
            ToggleButtonText = IsPasswordHidden ? "Show Password" : "Hide Password";
        }



        private void LoadPasswords()
        {
            Passwords = new ObservableCollection<PasswordInfo>()
            {
                new PasswordInfo(
                    Site: "facebook",
                    Description: "my facebook password",
                    Username: "redrum",
                    Password: "duskolo1",
                    SiteId: Guid.NewGuid()
                ),
                new PasswordInfo(
                    Site: "instagram",
                    Description: "my instagram password",
                    Username: "redrum",
                    Password: "duskolo2",
                    SiteId: Guid.NewGuid()
                ),
                new PasswordInfo(
                    Site: "myspace",
                    Description: "my myspace password",
                    Username: "redrum",
                    Password: "duskolo3",
                    SiteId: Guid.NewGuid()
                ),
                new PasswordInfo(
                    Site: "twitter",
                    Description: "my twitter password",
                    Username: "redrum",
                    Password: "duskolo4",
                    SiteId: Guid.NewGuid()
                ),
                new PasswordInfo(
                    Site: "twitter",
                    Description: "my twitter password",
                    Username: "redrum",
                    Password: "duskolo5",
                    SiteId: Guid.NewGuid()
                )
            };
        }

        private async Task GeneratePasswordAsync()
        {
            bool answer = await Application.Current.MainPage.DisplayAlert("Warning!", "Would you like to generate a new password?", "Yes", "No");
            if (answer)
            {
                var result = await Application.Current.MainPage.DisplayPromptAsync("Question", "Give the length of the password");
                if (!string.IsNullOrEmpty(result))
                {
                    if (int.TryParse(result, out int passwordLength) && passwordLength > 0)
                    {
                        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                        var random = new Random();
                        var password = new string(Enumerable.Repeat(chars, passwordLength - 1)
                            .Select(s => s[random.Next(s.Length)]).ToArray());

                        int randomIndex = random.Next(password.Length);
                        password = password.Insert(randomIndex, "!");

                        PasswordEntryText = password;
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Warning", "Enter a valid number greater than 0", "OK");
                    }
                }
            }
        }

    }
}
