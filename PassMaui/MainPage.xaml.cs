using PassMaui.Models;
using PassMaui.View;
using PassMaui.ViewModel;

namespace PassMaui
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new PasswordsViewModel();

            var rowsContainer = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Spacing = 10
            };
            var context = BindingContext as PasswordsViewModel;

            for (int i = 0; i < context.Passwords.Count; i += 3)
            {
                var rowStackLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal
                };

                for (int j = i; j < Math.Min(i + 3, context.Passwords.Count); j++)
                {
                    var contentBoxes = new ContentBoxesView();

                    contentBoxes.BindingContext = context.Passwords[j];

                    contentBoxes.HorizontalOptions = LayoutOptions.FillAndExpand;

                    var siteEntry = contentBoxes.FindByName<Entry>("SiteEntry") as Entry;
                    if (siteEntry != null)
                    {
                        siteEntry.Text = context.Passwords[j].Site;
                    }

                    var descriptionEntry = contentBoxes.FindByName<Entry>("DescriptionEntry") as Entry;
                    if (descriptionEntry != null)
                    {
                        descriptionEntry.Text = context.Passwords[j].Description;
                    }

                    var usernameEntry = contentBoxes.FindByName<Entry>("UsernameEntry") as Entry;
                    if (usernameEntry != null)
                    {
                        usernameEntry.Text = context.Passwords[j].Username;
                    }

                    var passwordEntry = contentBoxes.FindByName<Entry>("PasswordEntry") as Entry;
                    if (passwordEntry != null)
                    {
                        passwordEntry.Text = context.Passwords[j].Password;
                    }

                    rowStackLayout.Children.Add(contentBoxes);
                }

                rowsContainer.Children.Add(rowStackLayout);
            }

            mainStackLayout.Children.Add(rowsContainer);
        }
    }
}
