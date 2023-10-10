
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
            Passwords = new List<PasswordInfo>()
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
    }

}
