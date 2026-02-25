namespace StudySauce.Shared.Services
{
    public interface ITitleService
    {
        Task UpdateTitle(string? title);
        string? GetTitle();
        event Action<string?>? OnTitleChanged;
    }
}
