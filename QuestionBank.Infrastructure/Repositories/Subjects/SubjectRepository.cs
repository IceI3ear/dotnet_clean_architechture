using QuestionBank.Core.Interfaces.Repositories.subjects;
using QuestionBank.Domain.Entities;
using QuestionBank.Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBank.Infrastructure.Repositories.Subjects
{
	public class SubjectRepository : RepositoryBase<Subject>, ISubjectRepository
	{
		public SubjectRepository(ApplicationDbContext context) : base(context)
		{
		}
	}
}
