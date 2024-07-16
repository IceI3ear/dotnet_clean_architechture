using QuestionBank.Infrastructure.EntityFramework;

namespace QuestionBank.Server.Middleware
{
	public class TenancyMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<TenancyMiddleware> _logger;
		private readonly List<string> _migrationDoneTenants = [];
		private readonly IConfiguration _configuration;

		private IServiceProvider Services { get; }

		public TenancyMiddleware(
			RequestDelegate next,
			IConfiguration configuration,
			IServiceProvider services,
			ILogger<TenancyMiddleware> logger
		)
		{
			_next = next;
			_configuration = configuration;
			Services = services;
			_logger = logger;
		}

		public async Task InvokeAsync(
			HttpContext context,
			ApplicationDbContext applicationContext)
		{
			string host = context.Request.Host.Host;
			_logger.LogInformation(host);
			var headerReferers = context.Request.Headers.Referer.ToString();

			await _next.Invoke(context);
		}
	}
}
