// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using StudySauce.Shared.Services;
using StudySauce.Win.Services;


namespace StudySauce.Win
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : MauiWinUIApplication
    {
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
                ApplicationName = "StudySauce.Web.Client"
            });
            webBuilder.Environment.WebRootPath = System.IO.Path.Combine(AppContext.BaseDirectory, "wwwroot");
            //webBuilder.WebHost.UseStaticWebAssets();
            webBuilder.WebHost.ConfigureKestrel(options =>
            {
                options.ListenAnyIP(8080); // Open for business on port 8080
            });

            webBuilder.Services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddInteractiveWebAssemblyComponents();

            // Add device-specific services used by the StudySauce.Shared project
            webBuilder.Services.AddSingleton<IFormFactor, FormFactor>();
            webBuilder.Services.AddSingleton<ILocalServer, LocalServer>();

            var webApp = webBuilder.Build();

            webApp.UsePathBase("/");

            var localServer = (LocalServer)webApp.Services.GetRequiredService<ILocalServer>();
            localServer.Initialize(webApp);

            MauiProgram.ServerInstance = webApp;

            webApp.MapGet("/api/status", () => new { Status = "Online", Machine = Environment.MachineName });

            Microsoft.AspNetCore.Hosting.StaticWebAssets.StaticWebAssetsLoader.UseStaticWebAssets(
                webApp.Environment,
                webApp.Configuration);

            //webApp.UseHttpsRedirection();

            //webApp.UseBlazorFrameworkFiles();
            //webApp.UseStaticFiles();

            webApp.UseAntiforgery();

            webApp.MapStaticAssets();

            webApp.MapRazorComponents<Win.Components.App>()
                .AddInteractiveServerRenderMode()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(
                    typeof(StudySauce.Shared._Imports).Assembly,
                    typeof(StudySauce.Web.Client._Imports).Assembly);

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

    }
}
