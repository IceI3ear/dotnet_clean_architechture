using QuestionBank.Core.Interfaces.Repositories.Groups;
using QuestionBank.Domain.Entities;
using QuestionBank.Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBank.Infrastructure.Repositories.Groups
{
	public class GroupRepository : RepositoryBase<Group>, IGroupRepository
	{
		public GroupRepository(ApplicationDbContext context) : base(context)
		{
		}
	}
}
