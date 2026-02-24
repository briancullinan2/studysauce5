using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace DataLayer.Utilities.Extensions
{
    public static class IEntityExtensions
    {
        /// <summary>
        /// Rehydrates the entity by discarding local changes and fetching 
        /// the latest data from the database.
        /// </summary>
        public static void Refetch<T>(this IEntity<T> entity) where T : IEntity
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            using var scope = entity._service.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TranslationContext>();
            var entry = context.Entry(entity);

            // If the entity isn't being tracked, we need to attach it first
            if (entry.State == EntityState.Detached)
            {
                context.Attach(entity);
            }

            // This executes the SQL SELECT and updates the object's properties
            entry.Reload();
        }

        public static void Save<T>(this T ent, bool? recurse = false) where T : class, IEntity<T>
        {
            // Start the Transaction
            using var scope = ent._service.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TranslationContext>();
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    // 1. Perform relational checks here (e.g., does the linked Facility exist?)
                    // if (!context.Facilities.Any(f => f.Id == messageEntity.FacilityId)) 
                    //    throw new Exception("Invalid Facility Link");

                    // 2. Add the primary entity
                    context.Set<T>().Add(ent);

                    // 3. Commit the changes
                    context.SaveChanges();

                    // 4. Finalize the transaction
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Arizona Compliance: Roll back to prevent data corruption
                    transaction.Rollback();
                    //Log.Error($"Transaction Aborted: {ex.Message}");
                    throw; // Rethrow so the parent catch can handle the fallback
                }
            }
        }

        public static int Update<T>(this T entity, IDbConnection conn, string keyName = "Id") where T : IEntity<T>
        {
            var type = typeof(T);
            var props = type.GetProperties();
            var tableName = type.Name; // Assumes Table Name = Class Name

            // 1. Build the SET clause (skipping the Primary Key)
            var setClauses = props
                .Where(p => p.Name != keyName)
                .Select(p => $"[{p.Name}] = @{p.Name}");

            string sql = $"UPDATE [{tableName}] SET {string.Join(", ", setClauses)} WHERE [{keyName}] = @{keyName}";

            using var cmd = conn.CreateCommand();
            cmd.CommandText = sql;

            // 2. Map values to Parameters (Prevents SQL Injection)
            foreach (var prop in props)
            {
                var param = cmd.CreateParameter();
                param.ParameterName = "@" + prop.Name;
                param.Value = prop.GetValue(entity) ?? DBNull.Value;
                cmd.Parameters.Add(param);
            }

            if (conn.State != ConnectionState.Open) conn.Open();
            return cmd.ExecuteNonQuery();
        }


        public static T Wrap<T>(this T target) where T : class, IEntity<T>
        {
            return T.Wrap(target, target._service);
        }


        /*
        public static List<T> ToList<T>(this IQueryable<T> query) where T : IEntity, new()
        {
            // 1. Convert the IQueryable to a raw SQL string
            // If using EF Core: string sql = query.ToQueryString();
            // If using a custom metadata provider, call its specific SQL generator
            string sql = query.ToString();

            var results = new List<T>();

            // 2. Execute via ADO.NET for high-performance medical-grade streaming
            using (var conn = DataFactory.CreateConnection())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Use your reflection-based mapper to hydrate the entity
                            results.Add(DataMapper.Map<T>(reader));
                        }
                    }
                }
            }
            return results;
        }
        */

    }
}
