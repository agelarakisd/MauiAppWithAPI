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

            //var peopleList = new List<PasswordInfo>();


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
                    var fourTextBoxesView = new ContentBoxesView();

                    if (fourTextBoxesView.FindByName<Entry>("SiteEntry") is Entry siteEntry)
                    {
                        siteEntry.Text = context.Passwords[j].Site;
                    }

                    if (fourTextBoxesView.FindByName<Entry>("DescriptionEntry") is Entry descriptionEntry)
                    {
                        descriptionEntry.Text = context.Passwords[j].Description;
                    }

                    if (fourTextBoxesView.FindByName<Entry>("UsernameEntry") is Entry usernameEntry)
                    {
                        usernameEntry.Text = context.Passwords[j].Username;
                    }

                    if (fourTextBoxesView.FindByName<Entry>("PasswordEntry") is Entry passwordEntry)
                    {
                        passwordEntry.Text = context.Passwords[j].Password;
                    }

                    fourTextBoxesView.HorizontalOptions = LayoutOptions.FillAndExpand;

                    rowStackLayout.Children.Add(fourTextBoxesView);
                }

                rowsContainer.Children.Add(rowStackLayout);
            }

            mainStackLayout.Children.Add(rowsContainer);
        }



    }
}