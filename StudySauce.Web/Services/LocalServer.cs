using StudySauce.Shared.Services;

namespace StudySauce.Services
{

    public class LocalServer : ILocalServer
    {
        public string BaseUrl => app.Urls.FirstOrDefault() ?? "http://localhost:5000";
        public Task StopAsync() => app.StopAsync();

        WebApplication app;

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
