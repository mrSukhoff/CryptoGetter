using CryptogetterBlazorApp.Components;
using CryptogetterBlazorApp.CryptoGetter;
using CryptogetterBlazorApp.Data;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();
builder.Services.AddServerSideBlazor();

builder.Services.AddSingleton<ServerList>();
builder.Services.AddSingleton<CodeExtractor>();

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
	.AddNegotiate();

builder.Services.AddHttpContextAccessor();

// ��������� SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlite("Data Source=app.db")); // ���� ���� ������ ����� � ����� �������

builder.Services.AddAuthorizationBuilder()
	.AddPolicy("GeneratorAccessPolicy", policy =>
		policy.RequireRole("PS\\dmx.generators", "PS\\dmx.logs.read"))
	.AddPolicy("LogsReadPolicy", policy =>
		policy.RequireRole("PS\\dmx.logs.read"))
	.SetFallbackPolicy(new AuthorizationPolicyBuilder()
		.RequireAuthenticatedUser()
		.Build());

builder.Services.AddScoped<IAuthorizationMiddlewareResultHandler, CustomAuthorizationMiddlewareResultHandler>();

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

// ������������� ���� ������
using (var scope = app.Services.CreateScope())
{
	var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
	dbContext.Database.EnsureCreated(); // ������ ���� ������, ���� � ���
}

app.Run();

public class CustomAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
{
	public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
	{
		if (authorizeResult.Succeeded)
		{
			await next(context);
		}
		else if (authorizeResult.Forbidden)
		{
			context.Response.StatusCode = StatusCodes.Status403Forbidden;
		}
		else if (context.User.Identity?.IsAuthenticated != true) // ���������� ��������
		{
			context.Response.StatusCode = StatusCodes.Status401Unauthorized;
		}
	}
}