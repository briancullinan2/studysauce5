namespace StudySauce.Shared.Services
{
    public interface ITitleService
    {
        Task UpdateTitle(string? title);
        event Action<string?>? OnTitleChanged;
    }
}
