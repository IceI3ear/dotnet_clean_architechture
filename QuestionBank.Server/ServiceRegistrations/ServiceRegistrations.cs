using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using QuestionBank.Core.Implement.Authentication;
using Microsoft.AspNetCore.Identity;
using QuestionBank.Domain.Entities;
using System;
using QuestionBank.Infrastructure.EntityFramework;
using QuestionBank.Core.Interfaces.Services.Authentication;
using QuestionBank.Core.Interfaces.Repositories.Groups;
using QuestionBank.Infrastructure.Repositories.Groups;
using QuestionBank.Core.Interfaces.Services.Groups;
using QuestionBank.Core.Implement.Groups;
using QuestionBank.Core.Interfaces.Services.Users;
using QuestionBank.Core.Implement.Users;
using QuestionBank.Core.Interfaces.Repositories.Users;
using QuestionBank.Infrastructure.Repositories.Users;
using QuestionBank.Core.Interfaces.Repositories.subjects;
using QuestionBank.Infrastructure.Repositories.Subjects;
using QuestionBank.Core.Interfaces.Services.Subjects;
using QuestionBank.Core.Implement.Subjects;
using QuestionBank.Core.Interfaces.Repositories.QuestionReminders;
using QuestionBank.Infrastructure.Repositories.QuestionReminders;
using QuestionBank.Core.Interfaces.Services.QuestionReminders;
using QuestionBank.Core.Implement.QuestionReminders;

namespace QuestionBank.Server.ServiceRegistrations
{
    public static class ServiceRegistrations
	{
		public static void ConfigureServices(this IServiceCollection services, ConfigurationManager configuration, bool isProduction)
		{

			if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Stage")
			{
				services.AddDbContext<ApplicationDbContext>(options =>
						options.UseSqlServer(Environment.GetEnvironmentVariable("CONNECTION_STRING"), x => x.MigrationsAssembly("QuestionBank.Database")));
			}
			else
			{
				services.AddDbContext<ApplicationDbContext>(options =>
					options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
			}

			services.AddCors(options =>
			{
				options.AddPolicy("_myAllowSpecificOrigins",
					builder =>
					{
						builder.AllowAnyOrigin()
							.AllowAnyMethod()
							.AllowAnyHeader();
					});
			});
			services.AddCors(option =>
			{
				option.AddPolicy(name: "_myAllowSpecificDevOrigins",
					builder =>
					{
						builder.AllowAnyOrigin()
							.AllowAnyMethod()
							.AllowAnyHeader();
					});
			});

			services.AddIdentity<AppUser, AppRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();
			services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
			services.AddScoped<IAccountService, AccountService>();
			services.AddScoped<IGroupRepository, GroupRepository>();
			services.AddScoped<IGroupService, GroupService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<ISubjectRepository, SubjectRepository>();
			services.AddScoped<ISubjectService, SubjectService>();
			services.AddScoped<IQuestionReminderRepository, QuestionReminderRepository>();
			services.AddScoped<IQuestionReminderService, QuestionReminderService>();
		}
	}
}
