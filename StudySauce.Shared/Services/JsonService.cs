using Microsoft.AspNetCore.Components;

namespace StudySauce.Shared.Services
{
    public interface IJsonService
    {
        Task SetState(IComponent? state);
        Task RestoreState(IComponent component);
        event Action<IComponent?>? OnStateChanged;
    }


    public class JsonService : IJsonService
    {
        public JsonService()
        {
        }

        public event Action<IComponent?>? OnStateChanged;

        public virtual async Task SetState(IComponent? state)
        {
            OnStateChanged?.Invoke(state);
        }

        public virtual async Task RestoreState(IComponent component)
        {
        }

    }
}
