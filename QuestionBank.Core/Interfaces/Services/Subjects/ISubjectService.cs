using QuestionBank.Core.ViewModels.Request;
using QuestionBank.Core.ViewModels.Request.Subjects;
using QuestionBank.Core.ViewModels.Response;
using QuestionBank.Core.ViewModels.Response.Subject;
using QuestionBank.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBank.Core.Interfaces.Services.Subjects
{
	public interface ISubjectService
	{
		Task<bool> Create(SubjectRequest request);
		Task<bool> Update(int id, SubjectRequest request);
		Task<bool> DeleteSoft(int id);
		Task<bool> DeleteRecord(int id);
		Task<SubjectResponse> Get(int id);
		Task<PagedResponse<SubjectResponse>> GetList(PagedRequest request);
	}
}
