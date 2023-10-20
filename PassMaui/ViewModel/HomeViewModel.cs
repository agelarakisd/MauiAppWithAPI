using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PassMaui.Models;
using PassMaui.View;
using System.Collections.ObjectModel;
using SQLite;

namespace PassMaui.ViewModel
{
    public partial class HomeViewModel : ObservableObject
    {
        public ObservableCollection<PasswordInfo> Passwords { get; set; }

        private SQLiteConnection database;

        public HomeViewModel()
        {
            string dbPath = @"C:\sqlite\passmauidb.db";
            database = new SQLiteConnection(dbPath);

            if (!database.TableMappings.Any(m => m.MappedType == typeof(PasswordInfo)))
            {
                database.CreateTable<PasswordInfo>();
            }

            LoadPasswords();
        }

        public void AddPassword(PasswordInfo password)
        {
            database.Insert(password);
            LoadPasswords();
        }
        public void UpdatePassword(PasswordInfo password)
        {
            database.Update(password);
            LoadPasswords(); 
        }
        public void DeletePassword(PasswordInfo password)
        {
            database.Delete(password);
            LoadPasswords();
        }

        public void OnAppearing()
        {
            LoadPasswords();
        }

        private void LoadPasswords()
        {
            Passwords = new ObservableCollection<PasswordInfo>(database.Table<PasswordInfo>());
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
        public void DeleteAccount(int siteId)
        {
            var itemToDelete = Passwords.FirstOrDefault(item => item.SiteId == siteId);

            if (itemToDelete != null)
            {
                DeletePassword(itemToDelete);

                Passwords.Remove(itemToDelete);

                OnPropertyChanged(nameof(Passwords));
            }
        }



        [RelayCommand]
        public async Task NavigateToAddAccountPage()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(CreateAccountView));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }


        [RelayCommand]
        public async Task GeneratePassword(int siteId)
        {
            var itemToUpdate = Passwords.FirstOrDefault(item => item.SiteId == siteId);

            if (itemToUpdate != null)
            {
                var result = await Application.Current.MainPage.DisplayPromptAsync("Question", "Give the length of the password");

                if (!string.IsNullOrEmpty(result) && int.TryParse(result, out int passwordLength) && passwordLength > 0)
                {
                    string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                    var random = new Random();
                    var newPassword = new string(Enumerable.Repeat(chars, passwordLength)
                        .Select(s => s[random.Next(s.Length)]).ToArray());

                    int randomIndex = random.Next(newPassword.Length);
                    newPassword = newPassword.Insert(randomIndex, "!");

                    itemToUpdate.Password = newPassword;

                    UpdatePassword(itemToUpdate);

                    LoadPasswords();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Warning", "Enter a valid number greater than 0", "OK");
                }
            }
        }

    }
}