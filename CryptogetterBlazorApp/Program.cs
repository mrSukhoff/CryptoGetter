using CryptogetterBlazorApp.Components;
using CryptogetterBlazorApp.CryptoGetter;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();
builder.Services.AddServerSideBlazor();

// Регистрируем сервисы для генерации DataMatrix
builder.Services.AddSingleton<ServerList>();
builder.Services.AddSingleton<CodeExtractor>();

// Включение Windows Authentication
builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
	.AddNegotiate();

// Настройка политики авторизации для группы AD
builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("DmxGeneratorsPolicy", policy =>
		policy.RequireRole("PS\\dmx.generators"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.Run();