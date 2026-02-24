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
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IQueryCompiler, RemoteQuery>();
var keepAliveConnection = new Microsoft.Data.Sqlite.SqliteConnection("Data Source=:memory:");
keepAliveConnection.Open(); // The DB is born
builder.Services.AddDbContext<TranslationContext>((serviceProvider, options) =>
{
    options.UseSqlite(keepAliveConnection);
    options.ReplaceService<IQueryCompiler, RemoteQuery>();
});

var app = builder.Build();
var runtime = app.Services.GetRequiredService<IJSRuntime>();
var navigation = app.Services.GetRequiredService<NavigationManager>();
var localServer = (LocalServer)app.Services.GetRequiredService<ILocalServer>();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TranslationContext>();
    var conn = db.Database.GetDbConnection();
    if (conn.State != System.Data.ConnectionState.Open) conn.Open();
    db.Database.EnsureCreated();
}

localServer.Initialize(app, runtime, navigation);

await app.RunAsync();
