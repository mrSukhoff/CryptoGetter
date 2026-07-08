using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;

namespace CryptogetterBlazorApp;

public class CustomAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
{
	private readonly AuthorizationMiddlewareResultHandler _defaultHandler = new();
	private readonly ILogger<CustomAuthorizationMiddlewareResultHandler> _logger;

	public CustomAuthorizationMiddlewareResultHandler(ILogger<CustomAuthorizationMiddlewareResultHandler> logger)
	{
		_logger = logger;
	}

	public async Task HandleAsync(
	RequestDelegate next,
	HttpContext context,
	AuthorizationPolicy policy,
	PolicyAuthorizationResult authorizeResult)
	{
		var user = context.User.Identity?.Name ?? "Anonymous";
		var ip = context.Connection.RemoteIpAddress?.ToString() ?? "Unknown";

		if (authorizeResult.Forbidden)
		{
			_logger.LogWarning(
				"Forbidden. User={User}, IP={IP}, Path={Path}",
				user,
				ip,
				context.Request.Path);
		}
		else if (authorizeResult.Challenged)
		{
			_logger.LogWarning(
				"Anonymous access. IP={IP}, Path={Path}",
				ip,
				context.Request.Path);
		}

		var defaultHandler = new AuthorizationMiddlewareResultHandler();
		await defaultHandler.HandleAsync(next, context, policy, authorizeResult);
	}
}