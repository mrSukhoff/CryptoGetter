using CryptogetterBlazorApp.Components;
using CryptogetterBlazorApp.CryptoGetter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

// Регистрируем сервисы для генерации DataMatrix
builder.Services.AddSingleton<ServerList>();
builder.Services.AddSingleton<CodeExtractor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();