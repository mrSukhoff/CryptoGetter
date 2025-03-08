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

// ������������ ������� ��� ��������� DataMatrix
builder.Services.AddSingleton<ServerList>();
builder.Services.AddSingleton<CodeExtractor>();

// ��������� Windows Authentication
builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
	.AddNegotiate();

// ��������� �������� ����������� ��� ������ AD
builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("DmxGeneratorsPolicy", policy =>
		policy.RequireRole("PS\\dmx.generators"));
	// ��������� �������� ��� ���������������� �������������
	options.FallbackPolicy = new AuthorizationPolicyBuilder()
		.RequireAuthenticatedUser() // ������� ��������������
		.Build();
});

// ��������� ��������� ������ �����������
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

// ��������� middleware ��� ��������������� �� �������� AccessDenied
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

		// ���� ������ 403 (Forbidden), �������������� �� �������� AccessDenied
		if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
		{
			context.Response.Redirect("/access-denied");
		}
	}
}

// ��������� ���������� ���������� �����������
public class CustomAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
{
	public Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
	{
		if (authorizeResult.Forbidden)
		{
			context.Response.StatusCode = StatusCodes.Status403Forbidden;
			return Task.CompletedTask; // Middleware ���� ������������ �� /access-denied
		}

		return next(context);
	}
}