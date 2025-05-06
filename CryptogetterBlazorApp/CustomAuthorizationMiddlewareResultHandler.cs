using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;

namespace CryptogetterBlazorApp;

public class CustomAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
{
	private readonly ILogger<CustomAuthorizationMiddlewareResultHandler> _logger;
	private static readonly Dictionary<AuthorizationPolicy, string> PolicyNames = new Dictionary<AuthorizationPolicy, string>();

	public CustomAuthorizationMiddlewareResultHandler(ILogger<CustomAuthorizationMiddlewareResultHandler> logger)
	{
		_logger = logger;
	}

	// Метод для регистрации политик (вызывать при конфигурации в Program.cs)
	public static void RegisterPolicy(AuthorizationPolicy policy, string name)
	{
		PolicyNames[policy] = name;
	}

	public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
	{
		var userName = context.User.Identity?.IsAuthenticated == true ? context.User.Identity.Name : "Anonymous";
		var policyName = PolicyNames.TryGetValue(policy, out var name) ? name : "Unknown";

		if (authorizeResult.Succeeded)
		{
			await next(context);
		}
		else if (authorizeResult.Forbidden)
		{
			_logger.LogWarning("Access denied for user {User} on policy {Policy}. Path: {Path}", userName, policyName, context.Request.Path);
			context.Response.StatusCode = StatusCodes.Status403Forbidden;
			await context.Response.WriteAsync("Access denied. Insufficient permissions.");
		}
		else if (context.User.Identity?.IsAuthenticated != true)
		{
			_logger.LogWarning("Unauthorized access attempt by {User}. Path: {Path}", userName, context.Request.Path);
			context.Response.StatusCode = StatusCodes.Status401Unauthorized;
			await context.Response.WriteAsync("Unauthorized. Please log in.");
		}
	}
}