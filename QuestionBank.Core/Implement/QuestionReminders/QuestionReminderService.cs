using QuestionBank.Core.Interfaces.Repositories.Groups;
using QuestionBank.Core.Interfaces.Repositories.QuestionReminders;
using QuestionBank.Core.Interfaces.Services.QuestionReminders;
using QuestionBank.Core.Utilities;
using QuestionBank.Core.ViewModels.Request;
using QuestionBank.Core.ViewModels.Request.QuestionReminders;
using QuestionBank.Core.ViewModels.Response;
using QuestionBank.Core.ViewModels.Response.Groups;
using QuestionBank.Core.ViewModels.Response.QuestionReminder;
using QuestionBank.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBank.Core.Implement.QuestionReminders
{
	public class QuestionReminderService : IQuestionReminderService
	{
		private readonly IQuestionReminderRepository _questionReminderRepository;

		public QuestionReminderService(IQuestionReminderRepository questionReminderRepository)
		{
			_questionReminderRepository = questionReminderRepository;
		}

		public async Task<bool> Create(QuestionReminderRequest request)
		{
			var questionReminder = new QuestionReminder()
			{
				Name = request.Name,
				Description = request.Description,
			};
			await _questionReminderRepository.Add(questionReminder);
			return true;
		}

		public async Task<bool> DeleteRecord(int id)
		{
			var res = await _questionReminderRepository.Get(i => i.Id == id);

			if (res is null) { throw new Exception("Something wrong"); }

			await _questionReminderRepository.Delete(res);
			return true;
		}

		public async Task<bool> DeleteSoft(int id)
		{
			var res = await _questionReminderRepository.Get(i => i.Id == id);

			if (res is null) { throw new Exception("Something wrong"); }

			res.IsDeleted = true;
			
			await _questionReminderRepository.Update(res);
			return true;
		}

		public async Task<QuestionReminderResponse> Get(int id)
		{
			var res = await _questionReminderRepository.Get(i => i.Id == id);

			if (res is null) { throw new Exception("Something wrong"); }

			return new QuestionReminderResponse()
			{
				Id = res.Id,
				Name = res.Name,
				Description = res.Description,
			};
		}

		public async Task<PagedResponse<QuestionReminderResponse>> GetList(PagedRequest request)
		{
			Expression<Func<QuestionReminder, bool>> filter = i => !i.IsDeleted;

			if (request.SearchText != null)
			{
				Expression<Func<QuestionReminder, bool>> searchFilter = i => i.Name.Contains(request.SearchText);

				filter = PredicateBuilder.AndAlso(filter, searchFilter);
			}

			var query = await _questionReminderRepository
				.QueryAndSelectAsync(
				selector: q => new QuestionReminderResponse()
				{
					Id = q.Id,
					Name = q.Name,
					Description	= q.Description,
					IsDelete = q.IsDeleted,
				},
				filter,
				orderBy: q => Helper.ApplyOrder(q, request.SortColumn ?? "Name", request.SortOrder),
				pageSize: request.PageSize,
				page: request.CurrentPage
		   );

			int total = await _questionReminderRepository.Count(filter);

			return new PagedResponse<QuestionReminderResponse>()
			{
				Items = query.ToList(),
				Limit = request.PageSize,
				TotalItems = total,
				TotalPages = (int)Math.Ceiling((double)total / request.PageSize),
				Page = request.CurrentPage
			};
		}

		public async Task<bool> Update(int id, QuestionReminderRequest request)
		{
			var res = await _questionReminderRepository.Get(i => i.Id == id);

			if (res is null) { throw new Exception("Something wrong"); }

			res.Name = request.Name;
			res.Description = request.Description;

			await _questionReminderRepository.Update(res);
			return true;
		}
	}
}
