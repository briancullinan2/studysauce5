using Microsoft.Extensions.DependencyInjection;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace DataLayer.Entities
{

    public interface IEntity : INotifyPropertyChanged
    {
        abstract new public event PropertyChangedEventHandler? PropertyChanged;
        //abstract internal static IEntity Create(IEntity target);
        //abstract internal static IEntity Wrap(IEntity target);
    }

    public interface IEntity<T> : INotifyPropertyChanged, IEntity where T : IEntity
    {
        abstract new public event PropertyChangedEventHandler? PropertyChanged;
        abstract internal static T Create(T target, IServiceProvider service);
        internal IServiceProvider _service { get; }
        abstract internal static T Wrap(T target, IServiceProvider service);
    }

    abstract public class Entity : DispatchProxy
    {
        protected abstract override object? Invoke(MethodInfo? targetMethod, object?[]? args);

        public static IEntity Wrap(IEntity target, IServiceProvider service)
        {
            object generic = typeof(Entity<>).MakeGenericType(target.GetType());
            object proxy = System.Reflection.DispatchProxy.Create(generic.GetType(), target.GetType());

            var prop = proxy.GetType().GetField(nameof(Entity<>._target));
            prop.SetValue(proxy, target);
            var prop2 = proxy.GetType().GetField(nameof(Entity<>._service));
            prop2.SetValue(proxy, service);

            return proxy as IEntity;
        }

    }

    public class Entity<T> : Entity, IEntity<T>, INotifyPropertyChanged where T : IEntity
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected Entity() : base()
        {
            // observables don't like this because of invoke recursion bla bla bla, damn gemini is chatty
            // _target = this;
        }

        internal T _target = default!;
        internal IServiceProvider _service = default!;

        IServiceProvider IEntity<T>._service => this._service;

        // 1. You create a static helper to wrap the real object
        public static T Create(T target, IServiceProvider service)
        {
            // This calls the internal EF/WPF magic to create the proxy
            object proxy = DispatchProxy.Create<T, Entity<T>>();

            // This sets the _target field so Invoke can use it later
            ((Entity<T>)proxy)._target = target;
            ((Entity<T>)proxy)._service = service;

            return (T)proxy;
        }


        public override bool Equals(object? obj)
        {
            if (this == null || obj == null) return this == obj;

            var type = typeof(T);
            //var ignoreList = new List<string>(ignore);
            var foreignKeys = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Select(p => Attribute.GetCustomAttribute(p, typeof(ForeignKeyAttribute))?.TypeId);

            // Get properties that are NOT virtual (Nav properties) and NOT marked [NotMapped]
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => !foreignKeys.Contains(p.Name) // TODO: skip all IDs because they might not match on server
                    && p.GetGetMethod()?.IsVirtual != true
                    && !Attribute.IsDefined(p, typeof(KeyAttribute))  // TODO: don't match id fields
                    && !Attribute.IsDefined(p, typeof(NotMappedAttribute))
                    && !Attribute.IsDefined(p, typeof(ForeignKeyAttribute)) // comparing id is enough
                    && !typeof(IEnumerable).IsAssignableFrom(p.PropertyType)
                            //&& !ignoreList.Contains(p.Name)
                            );

            foreach (var prop in properties)
            {
                var selfValue = prop.GetValue(this, null);
                var toValue = prop.GetValue(obj, null);

                if (selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue)))
                {
                    return false;
                }
            }
            return true;
        }


        static T IEntity<T>.Wrap(T target, IServiceProvider service)
        {
            return (T)Wrap((IEntity)target, service);
        }

        protected void SetValue<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return;

            field = value;

            // "Looking for null or subs": 
            // If PropertyChanged is null (no WPF binding), nothing happens.
            // If it has subs, it notifies the UI.
            PropertyChanged?.Invoke(_target, new PropertyChangedEventArgs(propertyName));

            // Lead back to your static TranslationContext
            OnEntityChanged(propertyName);
        }

        private void OnEntityChanged(string prop)
        {
            // Add your logic to track "dirty" states for your Cloud/Disk swap
            Console.WriteLine($"Property {prop} changed. Ready for sync.");
        }

        protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
        {
            var result = targetMethod?.Invoke(_target, args);

            if (targetMethod?.Name.StartsWith("set_") == true)
            {
                var propName = targetMethod.Name.Substring(4);
                // Trigger your WPF logic here
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
                OnEntityChanged(propName);
            }
            if (targetMethod?.Name.StartsWith("get_") == true && result == null)
            {
                var prop = _target.GetType().GetProperty(targetMethod.Name.Substring(4));
                using var scope = _service.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<TranslationContext>();

                if (typeof(IEntity<>).IsAssignableFrom(prop.PropertyType))
                {
                    // 1. Force EF to hit the DB/Disk/Cloud
                    context.Entry(_target).Reference(prop.Name).Load();

                    // 2. Retrieve the now-populated value
                    var rawValue = prop.GetValue(_target) as IEntity;

                    // 3. Make it "Smart" before returning it to the UI
                    result = rawValue != null ? Entity.Wrap(rawValue, _service) : null;
                }
                else if (typeof(IEnumerable).IsAssignableFrom(prop.PropertyType))
                {
                    // 1. Force load the collection
                    context.Entry(_target).Collection(prop.Name).Load();

                    // 2. Collections are slightly different; we want the proxy to 
                    // track the collection itself or wrap the items.
                    result = (prop.GetValue(_target) as IEnumerable<IEntity>).Select(e => Entity.Wrap(e, _service));
                }
            }
            return result;
        }

    }
}
