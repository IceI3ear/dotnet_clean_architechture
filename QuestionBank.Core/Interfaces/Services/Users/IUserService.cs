using QuestionBank.Core.ViewModels.Request;
using QuestionBank.Core.ViewModels.Request.User;
using QuestionBank.Core.ViewModels.Response;
using QuestionBank.Core.ViewModels.Response.User;
using QuestionBank.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBank.Core.Interfaces.Services.Users
{
    public interface IUserService
    {
        Task<UserResponse> AutoGet(int id);
        Task<UserResponse> Get(int id);
        Task<bool> CreateUser(UserRequest model);
        Task<bool> UpdateUser(int id, UserRequest model);
        Task<bool> DeleteSoftUser(int id);
        Task<bool> DeleteRecordUser(int id);
        Task<PagedResponse<UserResponse>> GetListUsers(PagedRequest request);
    }
}
