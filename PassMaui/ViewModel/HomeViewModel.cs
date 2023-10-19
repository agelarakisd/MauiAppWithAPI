using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PassMaui.Models;
using System.Collections.ObjectModel;

namespace PassMaui.ViewModel
{
    public partial class HomeViewModel : ObservableObject
    {
        public ObservableCollection<PasswordInfo> Passwords { get; set; }

        public HomeViewModel()
        {
            LoadPasswords();
        }

        private void LoadPasswords()
        {
            Passwords = new ObservableCollection<PasswordInfo>()
            {
                new PasswordInfo(
                    Site: "facebook",
                    Description: "my facebook password",
                    Username: "facebookuser",
                    Password: "password1",
                    SiteId: 1
                ),
                new PasswordInfo(
                    Site: "instagram",
                    Description: "my instagram password",
                    Username: "instagramuser",
                    Password: "password2",
                    SiteId: 2
                ),
                new PasswordInfo(
                    Site: "myspace",
                    Description: "my myspace password",
                    Username: "myspaceuser",
                    Password: "password3",
                    SiteId: 3
                ),
                new PasswordInfo(
                    Site: "twitter",
                    Description: "my twitter password",
                    Username: "twitteruser",
                    Password: "password4",
                    SiteId: 4
                ),
                new PasswordInfo(
                    Site: "twitter",
                    Description: "my twitter password",
                    Username: "twitteruser",
                    Password: "password5",
                    SiteId: 5
                )
            };
        }

        [RelayCommand]
        public async Task CopyPassword(int siteId)
        {
            var passwordInfo = Passwords.FirstOrDefault(item => item.SiteId == siteId);
            if (passwordInfo != null)
            {
                string password = passwordInfo.Password;
                if (!string.IsNullOrEmpty(password))
                {
                    await Clipboard.SetTextAsync(password);
                }
            }
        }

        [RelayCommand]
        public void DeletePassword(int siteId)
        {
            var itemToDelete = Passwords.FirstOrDefault(item => item.SiteId == siteId);

            if (itemToDelete != null)
            {
                Passwords.Remove(itemToDelete);
            }
        }

        [RelayCommand]
        public async Task NavigateToAddAccountPage()
        {
            // to be fixed
        }


        [RelayCommand]
        public async Task GeneratePassword(int siteId)
        {
            {
                var result = await Application.Current.MainPage.DisplayPromptAsync("Question", "Give the length of the password");
                if (!string.IsNullOrEmpty(result))
                {
                    if (int.TryParse(result, out int passwordLength) && passwordLength > 0)
                    {
                        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                        var random = new Random();
                        var newPassword = new string(Enumerable.Repeat(chars, passwordLength - 1)
                            .Select(s => s[random.Next(s.Length)]).ToArray());

                        int randomIndex = random.Next(newPassword.Length);
                        newPassword = newPassword.Insert(randomIndex, "!");

                        var itemToUpdate = Passwords.FirstOrDefault(item => item.SiteId == siteId);

                        if (itemToUpdate != null)
                        {
                            var updatedItem = itemToUpdate with { Password = newPassword };
                            int index = Passwords.IndexOf(itemToUpdate);

                            if (index >= 0)
                            {
                                Passwords[index] = updatedItem;
                                OnPropertyChanged(nameof(Passwords));
                            }
                        }
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