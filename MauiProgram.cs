using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskApp.Database;
using TaskApp.Services;
using TaskApp.ViewModels.Authentication;
using TaskApp.ViewModels.Categories;
using TaskApp.ViewModels.MainPage;
using TaskApp.ViewModels.Register;
using TaskApp.ViewModels.Tasks;
using TaskApp.Views.Categories;
using TaskApp.Views.Login;
using TaskApp.Views.Register;
using TaskApp.Views.Tasks;

namespace TaskApp
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
                });

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                string dbPath = Path.Combine(FileSystem.AppDataDirectory, "taskmanager.db");
                options.UseSqlite($"Filename={dbPath}");
            });
            builder.Services.AddScoped<DatabaseService>();

            //main
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainPageViewModel>();

            //tasks
            builder.Services.AddTransient<AddTaskViewModel>();
            builder.Services.AddTransient<AddTaskPage>();
            builder.Services.AddTransient<EditTaskViewModel>();
            builder.Services.AddTransient<EditTaskPage>();

            //categories
            builder.Services.AddTransient<AddCategoryViewModel>();
            builder.Services.AddTransient<AddCategoryPage>();
            builder.Services.AddTransient<EditCategoryViewModel>();
            builder.Services.AddTransient<EditCategoryPage>();

            //authentication
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddTransient<RegisterPage>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
