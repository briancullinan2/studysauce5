using DataLayer;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.JSInterop;
using StudySauce.Shared.Services;
using StudySauce.Web.Client.Services;
var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add device-specific services used by the StudySauce.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();
builder.Services.AddSingleton<ILocalServer, LocalServer>();
builder.Services.AddSingleton<HttpClient>(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});
//builder.Services.AddSingleton<IQueryCompiler, RemoteQuery>();
//builder.Services.AddDbContext<TranslationContext>((serviceProvider, options) =>
//{
//    options.UseInMemoryDatabase("RemoteShell");
//});
builder.Services
           //.AddEntityFrameworkInMemoryDatabase()
           .AddDbContext<TranslationContext>((sp, options) =>
           {
               options.UseInMemoryDatabase("RemoteShell"); //.UseInternalServiceProvider(sp);
               options.ReplaceService<IQueryCompiler, RemoteQuery>();
           });

var app = builder.Build();
var runtime = app.Services.GetRequiredService<IJSRuntime>();
var navigation = app.Services.GetRequiredService<NavigationManager>();
var localServer = (LocalServer)app.Services.GetRequiredService<ILocalServer>();


localServer.Initialize(app, runtime, navigation);

await app.RunAsync();
