using Microsoft.Extensions.Logging;
using PassMaui.View;
using PassMaui.ViewModel;
using SQLite;

namespace PassMaui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddSingleton<HomeView>();
            builder.Services.AddSingleton<CreateAccountView>();
            builder.Services.AddSingleton<HomeViewModel>();
            builder.Services.AddSingleton<SQLiteConnection>(_ => new SQLiteConnection(@"C:\sqlite\passmauidb.db"));


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}