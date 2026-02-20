using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using StudySauce.Shared.Services;
using StudySauce.Web.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add device-specific services used by the StudySauce.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();

await builder.Build().RunAsync();
