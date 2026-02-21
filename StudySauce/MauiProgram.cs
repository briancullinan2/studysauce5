using Microsoft.Extensions.Logging;
using StudySauce.Services;
using StudySauce.Shared.Services;

namespace StudySauce
{
    public static class MauiProgram
    {
        //public static WebApplication? ServerInstance { get; set; }
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

            builder.Services.AddMauiBlazorWebView();
            // Inject the server instance into MAUI's DI
            //if (ServerInstance != null)
            //{
            //    builder.Services.AddSingleton<ILocalServer>(new LocalServer(ServerInstance));
            //}
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            //RegisterPageConstraints(builder);

            return builder.Build();
        }

        //public static void RegisterPageConstraints(IHostApplicationBuilder builder)
        //{
        //    builder.Services.Configure<RouteOptions>(opt => opt.ConstraintMap.Add("pack", typeof(EnumConstraint<PackMode>)));
        //}
    }


}
