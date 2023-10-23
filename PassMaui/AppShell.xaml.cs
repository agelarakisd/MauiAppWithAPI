using PassMaui.View;

namespace PassMaui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(CreateAccountView), typeof(CreateAccountView));
            Routing.RegisterRoute(nameof(HomeView), typeof(HomeView));
        }
    }
}