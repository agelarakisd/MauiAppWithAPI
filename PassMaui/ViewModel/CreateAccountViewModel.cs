﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PassMaui.Models;
using PassMaui.View;
using SQLite;

namespace PassMaui.ViewModel
{
    public partial class CreateAccountViewModel : ObservableObject
    {
        private readonly SQLiteConnection database;

        public CreateAccountViewModel(SQLiteConnection db)
        {
            database = db;
        }

        private string site;
        public string Site
        {
            get => site;
            set => SetProperty(ref site, value);
        }

        private string description;
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        private string username;
        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }

        private string passwordLength;
        public string PasswordLength
        {
            get => passwordLength;
            set => SetProperty(ref passwordLength, value);
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
                await Application.Current.MainPage.DisplayAlert("Navigation Error", ex.Message, "OK");
            }
        }

        [RelayCommand]
        public async Task CreateAccount()
        {
            if (int.TryParse(PasswordLength, out int passwordLength) && passwordLength > 0)
            {
                string password = GenerateRandomPassword(passwordLength);

                var newAccount = new PasswordInfo
                {
                    Site = Site,
                    Description = Description,
                    Username = Username,
                    Password = password
                };

                SaveAccountToDatabase(newAccount);

                Site = string.Empty;
                Description = string.Empty;
                Username = string.Empty;
                PasswordLength = string.Empty;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Password length is not a valid number.", "OK");
            }
        }



        private static string GenerateRandomPassword(int length)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var newPassword = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            int randomIndex = random.Next(newPassword.Length);
            newPassword = newPassword.Insert(randomIndex, "!");

            return newPassword;
        }

        private void SaveAccountToDatabase(PasswordInfo account)
        {
            database.Insert(account);

            Site = string.Empty;
            Description = string.Empty;
            Username = string.Empty;
            PasswordLength = string.Empty;
        }
    }
}