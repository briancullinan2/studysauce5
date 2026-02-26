using Microsoft.AspNetCore.Components;

namespace StudySauce.Shared.Services
{
    public interface IMenuService
    {
        Task SetMenu(RenderFragment? menu);
        event Action<RenderFragment?>? OnMenuChanged;
    }

    public class MenuService : IMenuService
    {

        public event Action<RenderFragment?>? OnMenuChanged;

        public async Task SetMenu(RenderFragment? menu)
        {
            OnMenuChanged?.Invoke(menu);
        }
    }
}
