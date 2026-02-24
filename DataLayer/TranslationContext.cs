using Microsoft.EntityFrameworkCore;

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
        public DbSet<DataLayer.Entities.Pack> Packs { get; set; }
        public DbSet<DataLayer.Entities.Card> Cards { get; set; }
        public DbSet<DataLayer.Entities.Answer> Answers { get; set; }
        public DbSet<DataLayer.Entities.Group> Groups { get; set; }
        public DbSet<DataLayer.Entities.File> Files { get; set; }

        // Add other entities here...

        public TranslationContext(DbContextOptions<TranslationContext> ctx) : base(ctx)
        {

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
            _contexts.Add(this);

        }

        private static readonly List<TranslationContext> _contexts = new();

        // Access a specific database context by its App.config Name
        public static TranslationContext? Get(string opt) => _contexts.FirstOrDefault(ctx => ctx.Database.GetConnectionString()?.Contains(opt) == true);



        public TranslationContext? this[string name]
        {
            get
            {
                _currentConfigure = name;
                return _contexts.FirstOrDefault(ctx => ctx.Database.GetConnectionString()?.Contains(name) == true);
            }
        }


        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            // This makes the conversion "implicit" for the database layer globally.
            configurationBuilder.Properties<Customization.DisplayType>().HaveConversion<int>();
            configurationBuilder.Properties<Customization.ControlMode>().HaveConversion<int>();
            configurationBuilder.Properties<Customization.Gender>().HaveConversion<int>();
            configurationBuilder.Properties<Customization.PackMode>().HaveConversion<int>();
            configurationBuilder.Properties<Customization.PackStatus>().HaveConversion<int>();
            configurationBuilder.Properties<Customization.CardType>().HaveConversion<int>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<System.Text.RegularExpressions.Capture>();
            modelBuilder.Ignore<System.Text.RegularExpressions.Match>();
            modelBuilder.Ignore<System.Text.RegularExpressions.Group>();

            // Explicitly map the Message entity to the "Message" table
            modelBuilder.Entity<Entities.Message>().ToTable(EntityMetadata.Message.TableName);
            modelBuilder.Entity<Entities.Permission>().ToTable(EntityMetadata.Permission.TableName);
            modelBuilder.Entity<Entities.Role>().ToTable(EntityMetadata.Role.TableName);
            modelBuilder.Entity<Entities.Setting>().ToTable(EntityMetadata.Setting.TableName);
            modelBuilder.Entity<Entities.User>().ToTable(EntityMetadata.User.TableName);
            modelBuilder.Entity<Entities.Card>().ToTable(EntityMetadata.Card.TableName);
            modelBuilder.Entity<Entities.Pack>().ToTable(EntityMetadata.Pack.TableName);
            modelBuilder.Entity<Entities.Answer>().ToTable(EntityMetadata.Answer.TableName);
            modelBuilder.Entity<Entities.Group>().ToTable(EntityMetadata.Group.TableName);
            modelBuilder.Entity<Entities.File>().ToTable(EntityMetadata.File.TableName);

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
        private static string? _currentConfigure;
        private bool _databaseChecked;

        public static TranslationContext Current
        {
            get
            {
                if (_currentConfigure == null)
                {
                    throw new InvalidOperationException("Create a database with a configuration before using TranslationContext.Current");
                }
                return Get(_currentConfigure);
            }
        }
    }

    public partial class EntityMetadata
    {
        public static EntityMetadata<Entities.Answer> Answer => new EntityMetadata<Entities.Answer>();
        public static EntityMetadata<Entities.Pack> Pack => new EntityMetadata<Entities.Pack>();
        public static EntityMetadata<Entities.Card> Card => new EntityMetadata<Entities.Card>();
        public static EntityMetadata<Entities.Permission> Permission => new EntityMetadata<Entities.Permission>();
        public static EntityMetadata<Entities.User> User => new EntityMetadata<Entities.User>();
        public static EntityMetadata<Entities.Role> Role => new EntityMetadata<Entities.Role>();
        public static EntityMetadata<Entities.Setting> Setting => new EntityMetadata<Entities.Setting>();
        public static EntityMetadata<Entities.Message> Message => new EntityMetadata<Entities.Message>();
        public static EntityMetadata<Entities.File> File => new EntityMetadata<Entities.File>();
        public static EntityMetadata<Entities.Group> Group => new EntityMetadata<Entities.Group>();

    }
}
