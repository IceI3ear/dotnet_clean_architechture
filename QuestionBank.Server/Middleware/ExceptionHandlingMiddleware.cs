using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using TouchStudy.Core.CustomExceptions;

namespace QuestionBank.Server.Middleware
{
	public static class ExceptionHandlingMiddleware
	{
		public static void ConfigureExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env,
			ILogger logger)
		{
			app.UseExceptionHandler(appError =>
			{
				appError.Run(async context =>
				{
					await HandleExceptionAsync(context, logger, true);
				});
			});
		}

		private static async Task HandleExceptionAsync(HttpContext context, ILogger logger, bool includeDetails)
		{
			var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerFeature>();
			if (exceptionHandlerPathFeature?.Error is null)
				return;

			// ProblemDetails has it's own content type
			context.Response.ContentType = "application/problem+json";

			var error = exceptionHandlerPathFeature.Error;
			var title = includeDetails ? error.Message : "An error occurred";
			var details = includeDetails ? error.ToString() : null;
			var status = GetCustomStatusCode(error);

			if (status == 500)
			{
				logger.LogError($"Warning: error code {status}. {title}{details}");
			}
			else
			{
				logger.LogWarning($"Warning: error code {status}. {title}{details}");
			}

			var problem = new ProblemDetails
			{
				Status = status,
				Title = title,
				Detail = details
			};

			// This is often very handy information for tracing the specific request
			var traceId = Activity.Current?.Id ?? context?.TraceIdentifier;
			problem.Extensions["traceId"] = traceId;

			//Serialize the problem details object to the Response as JSON (using System.Text.Json)
			if (context != null)
			{
				context.Response.StatusCode = status;
				var stream = context.Response.Body;
				await JsonSerializer.SerializeAsync(stream, problem, new JsonSerializerOptions
				{
					PropertyNamingPolicy = JsonNamingPolicy.CamelCase
				});
			}
		}

		private static int GetCustomStatusCode(Exception error)
		{
			if (error is CustomException)
				return CustomException.StatusCode;
			if (error is NotFoundException)
				return NotFoundException.StatusCode;
			if (error is AuthenticationException)
				return AuthenticationException.StatusCode;
			if (error is RegistrationException)
				return RegistrationException.StatusCode;
			if (error is WrongRoleException)
				return WrongRoleException.StatusCode;
			if (error is UnAuthorizeException)
				return UnAuthorizeException.StatusCode;
			if (error is ValidationException)
				return 400;

			return (int)HttpStatusCode.InternalServerError;
		}
	}
}
