using StudySauce.Shared.Services;
using System.Reflection;

namespace StudySauce.Services
{
    internal class TitleService : ITitleService
    {
        internal static Action<string?> _setTitle = s => { };
        internal static string? _title;
        private string? _appName;

        public event Action<string?>? OnTitleChanged;

        internal TitleService()
        {
            _appName = Assembly.GetEntryAssembly()?
                             .GetCustomAttribute<AssemblyProductAttribute>()?
                             .Product;
        }


        public async Task UpdateTitle(string? title)
        {
            if (title == null)
            {
                _setTitle(_appName);
            }
            else
            {
                _setTitle(title + " - " + _appName);
            }
            _title = title;
        }

        public string? GetTitle()
        {
            return _title;
        }
    }
}
