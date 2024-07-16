using QuestionBank.Core.Interfaces.Repositories.QuestionReminders;
using QuestionBank.Domain.Entities;
using QuestionBank.Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBank.Infrastructure.Repositories.QuestionReminders
{
	public class QuestionReminderRepository : RepositoryBase<QuestionReminder>, IQuestionReminderRepository
	{
		public QuestionReminderRepository(ApplicationDbContext context) : base(context)
		{
		}
	}
}
