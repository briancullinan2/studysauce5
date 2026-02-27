using DataLayer;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StudySauce.Services
{
    internal static class QueryService
    {
        public async static void RespondQuery(HttpContext context, IServiceProvider _service)
        {
            // 1. Read the serialized query from the request body
            using var scope = _service.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TranslationContext>();
            using var reader = new StreamReader(context.Request.Body);
            var jsonQuery = await reader.ReadToEndAsync();
            string rawXml = JsonSerializer.Deserialize<string>(jsonQuery);

            // TODO: add marshalling rules here
            var results = DataLayer.Utilities.Extensions.LinqExtensions.ToQueryable(rawXml, _service);

            context.Response.ContentType = "application/json";
            var json = JsonSerializer.Serialize(results, new JsonSerializerOptions
            {
                WriteIndented = true,
                ReferenceHandler = ReferenceHandler.IgnoreCycles // Important for EF Entities
            });

            await context.Response.WriteAsync(json);
        }
    }
}
