using QuestionBank.Core.Interfaces.Repositories.QuestionReminders;
using QuestionBank.Core.Interfaces.Repositories.subjects;
using QuestionBank.Core.Interfaces.Services.Subjects;
using QuestionBank.Core.Utilities;
using QuestionBank.Core.ViewModels.Request;
using QuestionBank.Core.ViewModels.Request.Subjects;
using QuestionBank.Core.ViewModels.Response;
using QuestionBank.Core.ViewModels.Response.QuestionReminder;
using QuestionBank.Core.ViewModels.Response.Subject;
using QuestionBank.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBank.Core.Implement.Subjects
{
	public class SubjectService : ISubjectService
	{
		private readonly ISubjectRepository _subjectRepository;

		public SubjectService(ISubjectRepository subjectRepository)
		{
			_subjectRepository = subjectRepository;
		}

		public async Task<bool> Create(SubjectRequest request)
		{
			var subject = new Subject()
			{
				Name = request.Name,
			};
			await _subjectRepository.Add(subject);
			return true;
		}

		public async Task<bool> DeleteRecord(int id)
		{
			var res = await _subjectRepository.Get(i => i.Id == id);

			if (res is null) { throw new Exception("Something wrong"); }

			await _subjectRepository.Delete(res);
			return true;
		}

		public async Task<bool> DeleteSoft(int id)
		{
			var res = await _subjectRepository.Get(i => i.Id == id);

			if (res is null) { throw new Exception("Something wrong"); }

			res.IsDeleted = true;

			await _subjectRepository.Delete(res);
			return true;
		}

		public async Task<SubjectResponse> Get(int id)
		{
			var res = await _subjectRepository.Get(i => i.Id == id);

			if (res is null) { throw new Exception("Something wrong"); }

			return new SubjectResponse()
			{
				Id = res.Id,
				Name = res.Name,
			};
		}

		public async Task<PagedResponse<SubjectResponse>> GetList(PagedRequest request)
		{
			Expression<Func<Subject, bool>> filter = i => !i.IsDeleted;

			if (request.SearchText != null)
			{
				Expression<Func<Subject, bool>> searchFilter = i => i.Name.Contains(request.SearchText);

				filter = PredicateBuilder.AndAlso(filter, searchFilter);
			}

			var query = await _subjectRepository
				.QueryAndSelectAsync(
				selector: q => new SubjectResponse()
				{
					Id = q.Id,
					Name = q.Name,
					IsDelete = q.IsDeleted,
				},
				filter,
				orderBy: q => Helper.ApplyOrder(q, request.SortColumn ?? "Name", request.SortOrder),
				pageSize: request.PageSize,
				page: request.CurrentPage
		   );

			int total = await _subjectRepository.Count(filter);

			return new PagedResponse<SubjectResponse>()
			{
				Items = query.ToList(),
				Limit = request.PageSize,
				TotalItems = total,
				TotalPages = (int)Math.Ceiling((double)total / request.PageSize),
				Page = request.CurrentPage
			};
		}

		public async Task<bool> Update(int id, SubjectRequest request)
		{
			var res = await _subjectRepository.Get(i => i.Id == id);

			if (res is null) { throw new Exception("Something wrong"); }

			res.Name = request.Name;

			await _subjectRepository.Update(res);
			return true;
		}
	}
}
