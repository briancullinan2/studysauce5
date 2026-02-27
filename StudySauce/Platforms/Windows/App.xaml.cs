// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using StudySauce.Services;
using StudySauce.Shared.Services;

namespace StudySauce.WinUI
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : MauiWinUIApplication
    {
        private static KeepAlive? _keepAlive;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        [STAThread]
        public static void Main(string[] args)
        {
            // 1. Start your Web Server in a background thread
            var webBuilder = WebApplication.CreateBuilder(new WebApplicationOptions
            {
                Args = args,
                // Ensure the server looks in the actual folder where the assets live
                ContentRootPath = AppContext.BaseDirectory,
                ApplicationName = "StudySauce"
            });
#if DEBUG
            webBuilder.Environment.EnvironmentName = Environments.Development;
#else
            webBuilder.Environment.EnvironmentName = Environments.Production;
#endif

            webBuilder.Services.AddDirectoryBrowser();
            webBuilder.Services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddInteractiveWebAssemblyComponents();

            webBuilder.Services.AddServerSideBlazor(options =>
            {
                options.DetailedErrors = true;
            });

            // Add device-specific services used by the StudySauce.Shared project
            webBuilder.Services.AddSingleton<IFormFactor, FormFactor>();
            webBuilder.Services.AddSingleton<ITitleService, TitleTrackerService>();
            webBuilder.Services.AddSingleton<IMenuService, MenuService>();
            webBuilder.Services.AddSingleton<IStudyService, StudyService>();
            webBuilder.Services.AddSingleton<ICourseService, CourseService>();
            webBuilder.Services.AddSingleton<IJsonService, JsonService>();

            // FUCK DI
            TitleService._setTitle = SetTitle;
            webBuilder.Services.AddSingleton<ILocalServer, LocalServer>();
            _keepAlive = new KeepAlive("Data Source=:memory:");
            _keepAlive.Open();
            webBuilder.Services.AddDbContext<DataLayer.TranslationContext>((serviceProvider, options) =>
            {
                options.UseSqlite(_keepAlive);
            });


            webBuilder.Environment.WebRootPath = Path.Combine(AppContext.BaseDirectory, "wwwroot");
            webBuilder.WebHost.ConfigureKestrel(options =>
            {
                options.ListenAnyIP(8080); // Open for business on port 8080
            });

            webBuilder.WebHost.UseStaticWebAssets();
            Microsoft.AspNetCore.Hosting.StaticWebAssets.StaticWebAssetsLoader.UseStaticWebAssets(
                webBuilder.Environment,
                webBuilder.Configuration);

            var webApp = webBuilder.Build();
            using (var scope = webApp.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<DataLayer.TranslationContext>();
                var conn = db.Database.GetDbConnection();
                if (conn.State != System.Data.ConnectionState.Open) conn.Open();
                db.Database.EnsureCreated();
                var _pack = db.Packs.FirstOrDefault(p => p.Title == "something");
            }

            var localServer = (LocalServer)webApp.Services.GetRequiredService<ILocalServer>();
            localServer.Initialize(webApp);

            MauiProgram.ServerInstance = webApp;

            //webApp.MapGet("/api/status", () => new { Status = "Online", Machine = Environment.MachineName });
            webApp.Use((context, next) =>
            {
                context.Response.Headers.Append("Cache-Control", "no-store, no-cache, must-revalidate, max-age=0");
                context.Response.Headers.Append("Pragma", "no-cache");
                context.Response.Headers.Append("Expires", "0");
                return next();
            });

            if (webApp.Environment.IsDevelopment())
            {
                webApp.UseWebAssemblyDebugging();
            }
            else
            {
                webApp.UseHsts();
            }

            //webApp.UseHttpsRedirection();
            //webApp.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            //webApp.UseAntiforgery();

            //webApp.UseDirectoryBrowser(new DirectoryBrowserOptions
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot")),
            //    RequestPath = "/wwwroot" // Or just "" if you want it at the root
            //});
            //webApp.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot", "_framework")),
            //    RequestPath = "/_framework"
            //});

            webApp.UsePathBase("/");
            webApp.UseStaticFiles(); // Move this UP
            webApp.UseBlazorFrameworkFiles();
            webApp.UseAntiforgery();
            webApp.UseRouting();     // Move this UP

            // 2. Mapping happens AFTER routing is configured
            //webApp.MapBlazorHub();
            webApp.MapPost("/api/query", QueryService.RespondQuery);



            webApp.MapRazorComponents<Components.App>()
                .AddInteractiveServerRenderMode()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(
                typeof(StudySauce.Shared._Imports).Assembly,
                typeof(StudySauce.Web.Client._Imports).Assembly)
                .DisableAntiforgery();


            // Run the Web Server in the background
            Task.Run(async () =>
            {
                try
                {
                    await webApp.RunAsync();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"!!! Kestrel Crash: {ex.Message}");
                }
            });

            // 2. Start the WinUI/MAUI Application
            WinRT.ComWrappersSupport.InitializeComWrappers();

            Microsoft.UI.Xaml.Application.Start((p) =>
            {
                var context = new Microsoft.UI.Dispatching.DispatcherQueueSynchronizationContext(
                    Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread());
                SynchronizationContext.SetSynchronizationContext(context);

                new App();
            });
        }
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            base.OnLaunched(args);

            // Get the handle from the first window in the MAUI application
            var mauiWindow = Microsoft.Maui.Controls.Application.Current?.Windows[0];
            var nativeWindow = mauiWindow?.Handler.PlatformView as Microsoft.UI.Xaml.Window;

            if (nativeWindow != null)
            {
                nativeWindow.ExtendsContentIntoTitleBar = true;

                var handle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);
                var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(handle);
                var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);

                appWindow.TitleBar.ExtendsContentIntoTitleBar = true;
                appWindow.TitleBar.ButtonBackgroundColor = Microsoft.UI.Colors.Transparent;
                appWindow.TitleBar.ButtonInactiveBackgroundColor = Microsoft.UI.Colors.Transparent;
                // This path looks in your bin output folder for the icon
                // Ensure "appicon.ico" is actually being copied there by our MSBuild target
                appWindow.SetIcon("teardrop.ico");
            }
        }

        internal static void SetTitle(string? title)
        {
            StudySauce.App.Current?.Windows.FirstOrDefault()?.Title = title;
        }
    }
}
