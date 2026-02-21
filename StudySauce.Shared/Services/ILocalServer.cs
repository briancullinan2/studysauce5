namespace StudySauce.Shared.Services
{
    public interface ILocalServer
    {
        string BaseUrl { get; }
        Task StopAsync();
    }

}
