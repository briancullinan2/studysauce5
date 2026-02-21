using Microsoft.AspNetCore.Builder;
using StudySauce.Shared.Services;

namespace StudySauce.Services
{

    public class LocalServer : ILocalServer
    {
        private WebApplication app;

        public string BaseUrl => app.Urls.FirstOrDefault() ?? "http://localhost:5000";
        public Task StopAsync() => app.StopAsync();

        internal void Initialize(WebApplication _app)
        {
            app = _app;
        }

        public LocalServer()
        {

        }
        public LocalServer(WebApplication _app)
        {
            app = _app;
        }

    }
}
