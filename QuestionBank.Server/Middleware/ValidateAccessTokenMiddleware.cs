using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using QuestionBank.Core.Constants;
using QuestionBank.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace QuestionBank.Server.Middleware
{
	public class ValidateAccessTokenMiddleware
	{
		private readonly RequestDelegate _next;

		public ValidateAccessTokenMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context, UserManager<AppUser> userManager)
		{
			var authorizationHeaders = context.Request.Headers.Authorization.ToString();
			var requestPath = context.Request.Path.Value ?? string.Empty;
			var endpoint = context.GetEndpoint();
			var isPass = true;

			if (!string.IsNullOrWhiteSpace(authorizationHeaders) && !(endpoint?.Metadata.GetMetadata<IAllowAnonymous>() is object))
			{
				var accessToken = authorizationHeaders.Split(" ")[1];
				var handler = new JwtSecurityTokenHandler();
				var decodedToken = handler.ReadJwtToken(accessToken);

				var userId = decodedToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
				var email = decodedToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.GivenName)?.Value;
				var roles = decodedToken.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();

				if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(email))
				{
					isPass = false;
					context.Response.StatusCode = 401;
					await context.Response.WriteAsync("Unauthorized");
				}

				var currentUser = await userManager.FindByIdAsync(userId!);
				var currentRoles = await userManager.GetRolesAsync(currentUser!);

				if (currentRoles.Count == 0 || currentRoles.Intersect(roles).Count() != currentRoles.Count)
				{
					isPass = false;
					context.Response.StatusCode = 401;
					await context.Response.WriteAsync("Unauthorized");
				}
			}

			if (isPass)
			{
				await _next(context);
			}
		}
	}
}
