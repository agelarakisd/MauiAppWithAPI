
using CommunityToolkit.Mvvm.ComponentModel;
using PassMaui.Models;
using System.Security;

namespace PassMaui.ViewModel
{

    public partial class PasswordsViewModel : ObservableObject
    {
        [ObservableProperty]
        List<PasswordInfo> passwords;

        public PasswordsViewModel()
        {
            LoadPasswords();
        }

        private void LoadPasswords()
        {
            var userId = Guid.NewGuid();
            Passwords = new List<PasswordInfo>()
            {
                new PasswordInfo(
                    Site: "facebook",
                    Description: "my facebook password",
                    Username: "redrum",
                    Password: "duskolo123",
                    UserId: userId
                ),                
                new PasswordInfo(
                    Site: "instagram",
                    Description: "my instagram password",
                    Username: "redrum",
                    Password: "duskolo123",
                    UserId: userId
                ),                
                new PasswordInfo(
                    Site: "myspace",
                    Description: "my myspace password",
                    Username: "redrum",
                    Password: "duskolo123",
                    UserId: userId
                ),                
                new PasswordInfo(
                    Site: "twitter",
                    Description: "my twitter password",
                    Username: "redrum",
                    Password: "duskolo123",
                    UserId: userId
                ),                
                new PasswordInfo(
                    Site: "twitter",
                    Description: "my twitter password",
                    Username: "redrum",
                    Password: "duskolo123",
                    UserId: userId
                )
            };
        }

        //private SecureString GetSecureString(string pwd)
        //{
        //    using var secureString = new SecureString();
        //    foreach (char c in pwd)
        //    {
        //        secureString.AppendChar(c);
        //    }
        //    return secureString;
        //}
    }

}
