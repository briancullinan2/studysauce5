using Microsoft.AspNetCore.Components;

namespace StudySauce.Shared.Services
{
    public interface IMenuService
    {
        Task SetMenu(RenderFragment? menu);
        event Action<RenderFragment?>? OnMenuChanged;

        Task SetHeader(bool? show);
        event Action<bool?>? OnHeaderChanged;

        Task SetSidebar(string? theme);
        event Action<string?>? OnSidebarChanged;

        Task SetApplication(string? theme);
        event Action<string?>? OnApplicationChanged;

        Task SetBackground(string? theme);
        event Action<string?>? OnBackgroundChanged;
    }

    public class MenuService : IMenuService
    {

        public event Action<RenderFragment?>? OnMenuChanged;

        public async Task SetMenu(RenderFragment? menu)
        {
            OnMenuChanged?.Invoke(menu);
        }

        public event Action<bool?>? OnHeaderChanged;

        public async Task SetHeader(bool? show)
        {
            OnHeaderChanged?.Invoke(show);
        }

        public event Action<string?>? OnSidebarChanged;
        public async Task SetSidebar(string? theme)
        {
            OnSidebarChanged?.Invoke(theme);
        }

        public event Action<string?>? OnApplicationChanged;

        public async Task SetApplication(string? theme)
        {
            OnApplicationChanged?.Invoke(theme);
        }

        public event Action<string?>? OnBackgroundChanged;

        public async Task SetBackground(string? theme)
        {
            OnBackgroundChanged?.Invoke(theme);
        }
    }
}
