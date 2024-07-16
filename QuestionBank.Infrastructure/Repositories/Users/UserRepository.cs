using QuestionBank.Core.Interfaces.Repositories.Users;
using QuestionBank.Domain.Entities;
using QuestionBank.Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBank.Infrastructure.Repositories.Users
{
	public class UserRepository : RepositoryBase<AppUser>, IUserRepository
	{
		public UserRepository(ApplicationDbContext context) : base(context)
		{
		}
	}
}
