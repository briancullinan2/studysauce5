using DataLayer.Utilities.Extensions;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Linq.Expressions;
using System.Net.Http.Json;

namespace StudySauce.Web.Client.Services
{
    public class RemoteQuery : IQueryCompiler
    {
        private readonly HttpClient _httpClient;
        internal static IServiceProvider _service;

        public RemoteQuery()
        {
            _httpClient = _service.GetRequiredService<HttpClient>();
        }


        public TResult Execute<TResult>(Expression query)
        {
            Console.WriteLine("Executing: " + query.ToString());
            // This is exactly where you use your Expression Tree Converter
            var serialized = query.ToXDocument().ToString();
            Console.WriteLine("Converted: " + query.ToString());

            // Send to your remote endpoint
            var response = _httpClient.PostAsJsonAsync("api/query", serialized).Result;

            return response.Content.ReadFromJsonAsync<TResult>().Result;
        }

        // You must also implement ExecuteAsync for ToListAsync() support
        public TResult ExecuteAsync<TResult>(Expression query, CancellationToken cancellationToken = default)
        {
            Console.WriteLine("Executing: " + query.ToString());
            // Same logic, but returning a Task/ValueTask
            return (TResult)typeof(RemoteQuery)
                .GetMethod(nameof(ExecuteRemoteAsync))
                .MakeGenericMethod(typeof(TResult).GetGenericArguments())
                .Invoke(this, new object[] { query, cancellationToken });
        }

        public async Task<T> ExecuteRemoteAsync<T>(Expression query, CancellationToken cancellationToken = default)
        {
            // 1. Serialize the expression tree using your converter
            var serialized = query.ToXDocument().ToString();
            Console.WriteLine("Converted: " + query.ToString());
            try
            {
                // 2. Perform the async network call
                var response = await _httpClient.PostAsJsonAsync("api/query", serialized, cancellationToken);

                // Ensure the request was successful
                response.EnsureSuccessStatusCode();

                // 3. Deserialize and return the result
                // Note: If T is a collection, ensure your API returns the expected format
                return await response.Content.ReadFromJsonAsync<T>(cancellationToken: cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return default(T);
            }
        }

        public Func<QueryContext, TResult> CreateCompiledAsyncQuery<TResult>(Expression query)
        {
            return (queryContext) => this.ExecuteAsync<TResult>(query, queryContext.CancellationToken);
        }

        public Func<QueryContext, TResult> CreateCompiledQuery<TResult>(Expression query)
        {
            return (queryContext) => this.Execute<TResult>(query);
        }
        public Expression<Func<QueryContext, TResult>> PrecompileQuery<TResult>(Expression query, bool async)
        {
            if (async)
            {
                return (queryContext) => this.ExecuteAsync<TResult>(query, queryContext.CancellationToken);
            }

            return (queryContext) => this.Execute<TResult>(query);
        }
    }
}
