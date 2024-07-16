using QuestionBank.Core.ViewModels.Request;
using QuestionBank.Core.ViewModels.Request.Groups;
using QuestionBank.Core.ViewModels.Response;
using QuestionBank.Core.ViewModels.Response.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBank.Core.Interfaces.Services.Groups
{
    public interface IGroupService
    {
        Task<PagedResponse<GroupResponse>> GetList(PagedRequest request);

        Task<bool> Create(GroupRequest request);

        Task<bool> Update(int id, GroupRequest request);

        Task<GroupResponse> GetById(int id);

        Task<bool> DeleteRecord(int id);
        Task<bool> DeleteSoft(int id);
    }
}
