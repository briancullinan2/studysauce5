using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.Data;

namespace DataLayer
{
    // This context never connects to a DB; it just holds your Entity mappings
    public class TranslationContext : DbContext
    {
        public DbSet<DataLayer.Entities.Permission> Permissions { get; set; }
        public DbSet<DataLayer.Entities.Role> Roles { get; set; }
        public DbSet<DataLayer.Entities.User> Users { get; set; }
        public DbSet<DataLayer.Entities.Setting> Settings { get; set; }
        public DbSet<DataLayer.Entities.Message> Messages { get; set; }
        // Add other entities here...

        public TranslationContext(Func<DbContextOptionsBuilder, string>? configure) : base()
        {
            if (configure == null)
            {
                throw new NotSupportedException();
            }
            _currentConfigure = _configure = configure;

            var conn = this.Database.GetDbConnection();
            if (conn.State != ConnectionState.Open) conn.Open();

            this.Database.EnsureCreated();
            //this.Database.Migrate();
        }

        public string ConnectString
        {
            get
            {
                return Database.GetDbConnection().ConnectionString;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (_configure == null)
            {
                throw new NotSupportedException();
            }
            var connection = _configure.Invoke(options);
            _contexts.TryAdd(connection, this);
        }

        private static readonly ConcurrentDictionary<string, TranslationContext> _contexts = new ConcurrentDictionary<string, TranslationContext>();

        // Access a specific database context by its App.config Name
        public static TranslationContext Get(Func<DbContextOptionsBuilder?, string> opt) => _contexts.GetOrAdd(opt(null), (key) => new TranslationContext(opt));

        public TranslationContext this[Func<DbContextOptionsBuilder?, string> opt]
        {
            get
            {
                _currentConfigure = opt;
                return _contexts.GetOrAdd(opt(null), (key) => new TranslationContext(opt));
            }
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            // This makes the conversion "implicit" for the database layer globally.
            configurationBuilder.Properties<Customization.Voltage>().HaveConversion<int>();
            configurationBuilder.Properties<Customization.Gender>().HaveConversion<int>();
            configurationBuilder.Properties<Customization.OrganComponent>().HaveConversion<int>();
            configurationBuilder.Properties<Customization.OrganSystem>().HaveConversion<int>();
            configurationBuilder.Properties<Customization.PulseDuration>().HaveConversion<int>();
            configurationBuilder.Properties<Customization.PulseWidth>().HaveConversion<int>();
            configurationBuilder.Properties<Customization.PWM0Frequency>().HaveConversion<int>();
            configurationBuilder.Properties<Customization.Voltage>().HaveConversion<int>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Explicitly map the Message entity to the "Message" table
            modelBuilder.Entity<Entities.Message>().ToTable("Message");
            modelBuilder.Entity<Entities.Permission>().ToTable("Permission");
            modelBuilder.Entity<Entities.Role>().ToTable("Role");
            modelBuilder.Entity<Entities.Setting>().ToTable("User");
            modelBuilder.Entity<Entities.User>().ToTable("User");

            /*
            modelBuilder.Entity<User>()
                .Property(u => u.FirstName)
                .HasMaxLength(50); // This is the "Gold Standard" for EF
            */

            /*
            modelBuilder.Entity<MedicalMessage>()
                .Property(e => e.VoltageSetting)
                .HasConversion<int>();
            */

        }

        //private static string _currentString = "Data Source=:memory:";
        private static Func<DbContextOptionsBuilder, string>? _currentConfigure;
        private Func<DbContextOptionsBuilder, string> _configure;
        public static TranslationContext Current => Get(_currentConfigure);
    }

    public partial class EntityMetadata
    {
        public static EntityMetadata<Entities.Permission> Permission => new EntityMetadata<Entities.Permission>();
        public static EntityMetadata<Entities.User> User => new EntityMetadata<Entities.User>();
        public static EntityMetadata<Entities.Role> Patient => new EntityMetadata<Entities.Role>();
        public static EntityMetadata<Entities.Setting> Setting => new EntityMetadata<Entities.Setting>();
        public static EntityMetadata<Entities.Message> Message => new EntityMetadata<Entities.Message>();

    }
}
