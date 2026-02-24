#if WINDOWS
#if DEBUG
using Microsoft.Extensions.Logging;
#endif
#endif
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using StudySauce.Services;
using StudySauce.Shared.Services;

namespace StudySauce
{
    public static class MauiProgram
    {
        private static SqliteConnection _keepAliveConnection;
#if WINDOWS
        public static Microsoft.AspNetCore.Builder.WebApplication? ServerInstance { get; set; }
#endif
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // Add device-specific services used by the StudySauce.Shared project
            builder.Services.AddSingleton<IFormFactor, FormFactor>();
            _keepAliveConnection = new Microsoft.Data.Sqlite.SqliteConnection("Data Source=:memory:");
            _keepAliveConnection.Open(); // The DB is born
            builder.Services.AddDbContext<DataLayer.TranslationContext>((serviceProvider, options) =>
            {
                options.UseSqlite(_keepAliveConnection);
            });

            //builder.Services.AddSingleton<DbContext>(new DataLayer.TranslationContext(c => c == null ? "Data Source=:memory:" : c.UseSqlite()));
            builder.Services.AddMauiBlazorWebView();
            // Inject the server instance into MAUI's DI
#if WINDOWS
            if (ServerInstance != null)
            {
                builder.Services.AddSingleton<ILocalServer>(new LocalServer(ServerInstance));
            }
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif
#endif

            //RegisterPageConstraints(builder);

            // 1. Build the app
            var mauiApp = builder.Build();


            // 2. Now you can create a scope from the built app
            using (var scope = mauiApp.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<DataLayer.TranslationContext>();
                var conn = db.Database.GetDbConnection();
                if (conn.State != System.Data.ConnectionState.Open) conn.Open();
                db.Database.Migrate();
            }

            // 3. Return the built app
            return mauiApp;
        }

        //public static void RegisterPageConstraints(IHostApplicationBuilder builder)
        //{
        //    builder.Services.Configure<RouteOptions>(opt => opt.ConstraintMap.Add("pack", typeof(EnumConstraint<PackMode>)));
        //}

    }


}
