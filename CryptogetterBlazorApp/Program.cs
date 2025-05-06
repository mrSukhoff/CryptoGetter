using CryptogetterBlazorApp;
using CryptogetterBlazorApp.Components;
using CryptogetterBlazorApp.CryptoGetter;
using CryptogetterBlazorApp.LogDb;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

// Настройка Serilog
Log.Logger = new LoggerConfiguration()
	.MinimumLevel.Information()
	.MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
	.Enrich.FromLogContext()
	.WriteTo.File(
		path: "logs/app-.log",
		rollingInterval: RollingInterval.Day,
		outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
	.CreateLogger();

builder.Host.UseSerilog();

// Razor и Blazor
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();
builder.Services.AddServerSideBlazor();

// Сервисы
builder.Services.AddSingleton<ServerList>();
builder.Services.AddSingleton<CodeExtractor>();


//Авторизация
builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
	.AddNegotiate();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthorizationBuilder()
	.AddPolicy("GeneratorAccessPolicy", policy =>
		policy.RequireRole("PS\\dmx.generators"))
	.AddPolicy("LogsReadPolicy", policy =>
		policy.RequireRole("PS\\dmx.logs.read"))
	.SetFallbackPolicy(new AuthorizationPolicyBuilder()
		.RequireAuthenticatedUser()
		.Build());

builder.Services.AddScoped<IAuthorizationMiddlewareResultHandler, CustomAuthorizationMiddlewareResultHandler>();

// Настройка SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlite("Data Source=app.db")); // Файл базы данных будет в корне проекта

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	app.UseHsts();
}

app.UseStaticFiles();

app.UseStatusCodePages(async context =>
{
	if (context.HttpContext.Response.StatusCode == StatusCodes.Status403Forbidden)
	{
		context.HttpContext.Response.StatusCode = StatusCodes.Status307TemporaryRedirect;
		context.HttpContext.Response.Headers.Location = "/access-denied";
		await context.HttpContext.Response.StartAsync();
	}
});

app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

// Инициализация базы данных
using (var scope = app.Services.CreateScope())
{
	var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
	dbContext.Database.EnsureCreated(); // Создаёт базу данных, если её нет
}

app.Run();