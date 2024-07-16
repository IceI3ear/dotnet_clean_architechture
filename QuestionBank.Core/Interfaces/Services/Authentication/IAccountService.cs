using QuestionBank.Core.ViewModels.Request.Authentication;
using QuestionBank.Core.ViewModels.Response.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBank.Core.Interfaces.Services.Authentication
{
    public interface IAccountService
    {
        Task<LoginResponse> Login(LoginRequest request);
    }
}
