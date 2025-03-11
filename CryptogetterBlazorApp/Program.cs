using CryptogetterBlazorApp.Components;
using CryptogetterBlazorApp.CryptoGetter;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();
builder.Services.AddServerSideBlazor();

builder.Services.AddSingleton<ServerList>();
builder.Services.AddSingleton<CodeExtractor>();

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
	.AddNegotiate();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("GeneratorAccessPolicy", policy =>
		policy.RequireRole("PS\\dmx.generators", "PS\\dmx.logs.read"));
	options.AddPolicy("LogsReadPolicy", policy =>
		policy.RequireRole("PS\\dmx.logs.read"));
	options.FallbackPolicy = new AuthorizationPolicyBuilder()
		.RequireAuthenticatedUser()
		.Build();
});

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
		else if (!context.User.Identity.IsAuthenticated)
		{
			context.Response.StatusCode = StatusCodes.Status401Unauthorized;
		}
	}
}