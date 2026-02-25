using StudySauce.Shared.Services;

namespace StudySauce.Services
{
    internal class TitleTrackerService : ITitleService
    {
        internal static Action<string?> _setTitle = s => { };
        internal static string? _title;

        public event Action<string?>? OnTitleChanged;

        public async Task UpdateTitle(string? title)
        {
            _title = title;
        }

        public string? GetTitle()
        {
            return _title;
        }
    }
}
