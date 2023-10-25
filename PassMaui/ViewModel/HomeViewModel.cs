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

        private readonly SQLiteConnection _database;

        public HomeViewModel() 
        {
            var dbPath = @"C:\sqlite\passmauidb.db";
            _database = new SQLiteConnection(dbPath);

            if (!_database.TableMappings.Any(m => m.MappedType == typeof(PasswordInfo)))
            {
                _database.CreateTable<PasswordInfo>();
            }

            LoadPasswords();
        }

        public void AddPassword(PasswordInfo password)
        {
            _database.Insert(password);
            LoadPasswords();
        }

        private void UpdatePassword(PasswordInfo password)
        {
            _database.Update(password);
            LoadPasswords(); 
        }

        private void DeletePassword(PasswordInfo password)
        {
            _database.Delete(password);
            LoadPasswords();
        }


        public void OnAppearing()
        {
            var previousEntryCount = Passwords.Count; 

            LoadPasswords();

            var currentEntryCount = Passwords.Count;

            if (currentEntryCount <= previousEntryCount) return;
            LoadPasswords();
            OnPropertyChanged(nameof(Passwords));
        }

        private void LoadPasswords()
        {
            Passwords = new ObservableCollection<PasswordInfo>(_database.Table<PasswordInfo>());
        }

        [RelayCommand]
        private async Task CopyPassword(int siteId)
        {
            var passwordInfo = Passwords.FirstOrDefault(item => item.SiteId == siteId);
            if (passwordInfo != null)
            {
                string password = passwordInfo.Password;
                if (!string.IsNullOrEmpty(password))
                {
                    await Clipboard.SetTextAsync(password);
                    if (Application.Current != null)
                        if (Application.Current.MainPage != null)
                            await Application.Current.MainPage.DisplayAlert("", "Copied to clipboard", "OK");
                }
            }
        }

        [RelayCommand]
        private void DeleteAccount(int siteId)
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
        private async Task NavigateToAddAccountPage()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(CreateAccountView));
            }
            catch (Exception ex)
            {
                if (Application.Current != null)
                    if (Application.Current.MainPage != null)
                        await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }


        [RelayCommand]
        private async Task GeneratePassword(int siteId)
        {
            var itemToUpdate = Passwords.FirstOrDefault(item => item.SiteId == siteId);

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
                            var newPassword = new string(Enumerable.Repeat(chars, passwordLength)
                                .Select(s => s[random.Next(s.Length)]).ToArray());

                            var randomIndex = random.Next(newPassword.Length);
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

    }
}