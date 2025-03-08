using CryptogetterBlazorApp.Components;
using CryptogetterBlazorApp.CryptoGetter;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;

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
	// Указываем страницу для неавторизованных пользователей
	options.FallbackPolicy = new AuthorizationPolicyBuilder()
		.RequireAuthenticatedUser() // Требуем аутентификацию
		.Build();
});

// Добавляем обработку ошибок авторизации
builder.Services.AddScoped<CustomAuthorizationMiddlewareResultHandler>();

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

// Кастомный middleware для перенаправления на страницу AccessDenied
public class CustomAuthorizationMiddleware
{
	private readonly RequestDelegate _next;

	public CustomAuthorizationMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		await _next(context);

		// Если статус 403 (Forbidden), перенаправляем на страницу AccessDenied
		if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
		{
			context.Response.Redirect("/access-denied");
		}
	}
}

// Кастомный обработчик результата авторизации
public class CustomAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
{
	public Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
	{
		if (authorizeResult.Forbidden)
		{
			context.Response.StatusCode = StatusCodes.Status403Forbidden;
			return Task.CompletedTask; // Middleware выше перенаправит на /access-denied
		}

		return next(context);
	}
}