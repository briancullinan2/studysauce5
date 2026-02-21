using Microsoft.AspNetCore.Builder;
using StudySauce.Shared.Services;

namespace StudySauce.Win.Services
{

    public class LocalServer : ILocalServer
    {
        public string BaseUrl => app?.Urls.FirstOrDefault() ?? "http://localhost:5000";
        public async Task StopAsync()
        {
            if (app == null) return;
            await app.StopAsync();
        }

        WebApplication? app;

        public LocalServer()
        {

        }

        public LocalServer(WebApplication _app)
        {
            app = _app;
        }

        public void Initialize(WebApplication _app)
        {
            app = _app;
        }
    }
}
