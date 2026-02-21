using StudySauce.Services;
using StudySauce.Shared.Services;
using StudySauce.Web.Components;
using StudySauce.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Add device-specific services used by the StudySauce.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();
builder.Services.AddSingleton<ILocalServer, LocalServer>();

var app = builder.Build();

var localServer = (LocalServer)app.Services.GetRequiredService<ILocalServer>();
localServer.Initialize(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseExceptionHandler("/Error", createScopeForErrors: true);
app.MapFallbackToFile("index.html");
//app.UseStatusCodePagesWithReExecute("/home", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(
        typeof(StudySauce.Shared._Imports).Assembly,
        typeof(StudySauce.Web.Client._Imports).Assembly);

app.Run();
