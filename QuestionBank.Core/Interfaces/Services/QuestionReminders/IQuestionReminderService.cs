using QuestionBank.Core.ViewModels.Request;
using QuestionBank.Core.ViewModels.Request.QuestionReminders;
using QuestionBank.Core.ViewModels.Response;
using QuestionBank.Core.ViewModels.Response.QuestionReminder;
using QuestionBank.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBank.Core.Interfaces.Services.QuestionReminders
{
	public interface IQuestionReminderService
	{
		Task<bool> Create(QuestionReminderRequest request);
		Task<bool> Update(int id, QuestionReminderRequest request);
		Task<bool> DeleteSoft(int id);
		Task<bool> DeleteRecord(int id);
		Task<QuestionReminderResponse> Get(int id);
		Task<PagedResponse<QuestionReminderResponse>> GetList(PagedRequest request);
	}
}
