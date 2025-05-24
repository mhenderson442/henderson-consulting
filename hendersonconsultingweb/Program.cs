var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Configuration.RegisterAzureKeyVault();

builder.Services.AddRazorPages();
builder.Services.RegisterScopedServices();
builder.Services.RegisterSingletonServices();
builder.Services.RegisterApplicationSettings(builder.Configuration);
builder.Services.RegisterHttpClients(builder.Configuration);

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapRazorPages();
app.Run();