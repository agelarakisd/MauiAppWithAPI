﻿using Microsoft.Extensions.Logging;
using PassMaui.ApiClients;
using PassMaui.APIServices;
using PassMaui.View;
using PassMaui.ViewModel;

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
            builder.Services.AddHttpClient();
            builder.Services.AddTransient<HomeView>();
            builder.Services.AddScoped<HomeViewModel>();
            builder.Services.AddSingleton<CreateAccountViewModel>();
            builder.Services.AddTransient<EditAccountViewModel>();
            builder.Services.AddTransient<PassMauiApiClient>();
            builder.Services.AddScoped<IAccountApiService>(sp =>
            { 
                var apiClient =  sp.GetService<PassMauiApiClient>(); 
                return apiClient.CreateApiService();
            });
            

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}