using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuestionBank.Core.Constants;
using QuestionBank.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBank.Infrastructure.EntityFramework
{
	public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, int>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

		public DbSet<AppUser> AppUsers { get; set; }
		public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<QuestionReminder> QuestionReminders { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			var stringArrayConverter = new ValueConverter<string[], string>(
			  v => string.Join(";", v),
			  v => v.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries));

			builder.Entity<UserGroup>().HasKey(sc => new { sc.UserId, sc.GroupId });

			builder.Entity<UserGroup>()
				.HasOne<AppUser>(sc => sc.Users)
				.WithMany(s => s.UserGroups)
				.HasForeignKey(sc => sc.UserId);


			builder.Entity<UserGroup>()
				.HasOne<Group>(sc => sc.Groups)
				.WithMany(s => s.UserGroups)
				.HasForeignKey(sc => sc.GroupId);

			SeedData(builder);
		}

		private static void SeedData(ModelBuilder builder)
		{
			#region seed data for identity role
			builder.Entity<AppRole>().HasData(new AppRole { Id = 1, Name = AuthenticationConstant.RoleAdmin, NormalizedName = AuthenticationConstant.RoleAdmin.ToUpper() });
			builder.Entity<AppRole>().HasData(new AppRole { Id = 2, Name = AuthenticationConstant.RoleProofReader, NormalizedName = AuthenticationConstant.RoleProofReader.ToUpper() });
			builder.Entity<AppRole>().HasData(new AppRole { Id = 3, Name = AuthenticationConstant.RoleQuestionCreator, NormalizedName = AuthenticationConstant.RoleQuestionCreator.ToUpper() });
			#endregion

			#region seed data for identity user
			var appUser = new AppUser
			{
				Id = 1,
				UserName = "admin",
				Email = "admin@gmail.com",
				NormalizedUserName = "ADMIN",
				NormalizedEmail = "ADMIN@GMAIL.COM",
				PasswordHash = "AQAAAAIAAYagAAAAEKRbeVHvo7lzK3A3uQiOAP7/Z9JTP3hjzteDyoTmLszccSQ23a5RZgQTvxZHXEQ6gg==",
				ConcurrencyStamp = "f1690e2b-2cb3-4f08-af4e-9da85e0bddbd",
				SecurityStamp = "a1068ef5-2f00-4d52-b6bc-87d255729e04",
			};
			builder.Entity<AppUser>().HasData(appUser);

			var proofreader = new AppUser
			{
				Id = 2,
				UserName = "proofreader",
				Email = "proofreader@gmail.com",
				NormalizedUserName = "PROOFREADER",
				NormalizedEmail = "PROOFREADER@GMAIL.COM",
				PasswordHash = "AQAAAAIAAYagAAAAEI/XnmLatlCDxrs6P/oUH89A0XpUOUvvTBJ9ETxdwc5HuRjoiR5NDDb3FIf4iE0nuA==",
				ConcurrencyStamp = "f1690e2b-2cb3-4f08-af4e-9da85e0bddbd",
				SecurityStamp = "a1068ef5-2f00-4d52-b6bc-87d255729e04",
			};
			builder.Entity<AppUser>().HasData(proofreader);

			var creator = new AppUser
			{
				Id = 3,
				UserName = "creator",
				Email = "creator@gmail.com",
				NormalizedUserName = "CREATOR",
				NormalizedEmail = "CREATOR@GMAIL.COM",
				PasswordHash = "AQAAAAIAAYagAAAAEI/XnmLatlCDxrs6P/oUH89A0XpUOUvvTBJ9ETxdwc5HuRjoiR5NDDb3FIf4iE0nuA==",
				ConcurrencyStamp = "f1690e2b-2cb3-4f08-af4e-9da85e0bddbd",
				SecurityStamp = "a1068ef5-2f00-4d52-b6bc-87d255729e04",
			};
			builder.Entity<AppUser>().HasData(creator);
			#endregion

			#region seed data for identity userrole
			builder.Entity<IdentityUserRole<int>>().HasData(
				new IdentityUserRole<int>
				{
					RoleId = 1,
					UserId = 1
				});

			builder.Entity<IdentityUserRole<int>>().HasData(
				new IdentityUserRole<int>
				{
					RoleId = 2,
					UserId = 1
				});
			builder.Entity<IdentityUserRole<int>>().HasData(
				new IdentityUserRole<int>
				{
					RoleId = 3,
					UserId = 1
				});
			builder.Entity<IdentityUserRole<int>>().HasData(
				new IdentityUserRole<int>
				{
					RoleId = 2,
					UserId = 2
				});
			builder.Entity<IdentityUserRole<int>>().HasData(
				new IdentityUserRole<int>
				{
					RoleId = 3,
					UserId = 3
				});
			#endregion
		}

		private static void AddSoftDeleteFilters(ModelBuilder builder)
		{
			foreach (var entityType in builder.Model.GetEntityTypes())
			{
				//other automated configurations left out
				if (typeof(BaseModel).IsAssignableFrom(entityType.ClrType))
				{
					entityType.AddSoftDeleteQueryFilter();
				}
			}
		}

		public override int SaveChanges()
		{
			UpdateEntityState();
			return base.SaveChanges();
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			UpdateEntityState();
			return base.SaveChangesAsync(cancellationToken);
		}

		private void UpdateEntityState()
		{
			var now = DateTime.UtcNow;

			foreach (var changedEntity in ChangeTracker.Entries())
			{
				if (changedEntity.Entity is BaseModel entity)
				{
					switch (changedEntity.State)
					{
						case EntityState.Added:
							entity.CreatedAt = now;
							entity.UpdatedAt = now;
							break;
						case EntityState.Modified:
							Entry(entity).Property(x => x.CreatedAt).IsModified = false;
							entity.UpdatedAt = now;
							break;
						case EntityState.Deleted:
							entity.IsDeleted = true;
							entity.DeletedAt = now;
							changedEntity.State = EntityState.Modified;
							break;
					}
				}
			}
		}
	}

	public static class SoftDeleteQueryExtension
	{
		public static void AddSoftDeleteQueryFilter(
			this IMutableEntityType entityData)
		{
			var methodToCall = typeof(SoftDeleteQueryExtension)
				.GetMethod(nameof(GetSoftDeleteFilter),
					BindingFlags.NonPublic | BindingFlags.Static)
				?.MakeGenericMethod(entityData.ClrType);
			var filter = methodToCall?.Invoke(null, new object[] { });
			if (filter != null)
			{
				entityData.SetQueryFilter((LambdaExpression)filter);
			}
		}

		private static LambdaExpression GetSoftDeleteFilter<TEntity>()
			where TEntity : BaseModel
		{
			Expression<Func<TEntity, bool>> filter = x => !x.IsDeleted;
			return filter;
		}
	}
}