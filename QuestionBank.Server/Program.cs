using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QuestionBank.Core.Implement.Authentication;
using QuestionBank.Server.Middleware;
using QuestionBank.Server.ServiceRegistrations;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices(builder.Configuration, builder.Environment.IsProduction());

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();

var logger = new LoggerConfiguration()
	.ReadFrom.Configuration(builder.Configuration)
	.Enrich.FromLogContext()
	.CreateLogger();

builder.Logging.ClearProviders();

builder.Logging.AddSerilog(logger);

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(JwtSettings.SectionsName));
string issuer = builder.Configuration.GetValue<string>("JwtSettings:Issuer") ?? string.Empty;
string audience = builder.Configuration.GetValue<string>("JwtSettings:Audience") ?? string.Empty;
string signingKey = builder.Configuration.GetValue<string>("JwtSettings:Secret") ?? string.Empty;
byte[] signingKeyBytes = System.Text.Encoding.UTF8.GetBytes(signingKey);

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
	options.RequireHttpsMetadata = false;
	options.SaveToken = true;
	options.TokenValidationParameters = new TokenValidationParameters()
	{
		ValidateIssuer = true,
		ValidIssuer = issuer,
		ValidateAudience = true,
		ValidAudience = audience,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ClockSkew = System.TimeSpan.Zero,
		IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
	};
});

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen(option =>
{
	option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
	{
		Type = SecuritySchemeType.Http,
		Scheme = "bearer",
		Description = "JWT Authorization header using the Bearer scheme.",
	});
	option.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			Array.Empty<string>()
		}
	});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	app.UseCors("_myAllowSpecificDevOrigins");
}
else
{
	app.UseHsts();
	app.UseCors("_myAllowSpecificOrigins");
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.UseDefaultFiles();

app.UseStaticFiles();

app.MapControllerRoute(
	name: "default",
	pattern: "api/{controller}/{action}/{id?}");

app.MapControllers();

app.Run();
