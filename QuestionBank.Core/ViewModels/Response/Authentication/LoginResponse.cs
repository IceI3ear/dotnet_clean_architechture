using QuestionBank.Core.ViewModels.Response.User;
using QuestionBank.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBank.Core.ViewModels.Response.Authentication
{
    public class LoginResponse
    {
        public UserResponse User { get; set; }
        public string Token { get; set; }

        public LoginResponse() { }
        public LoginResponse(AppUser user, string token, IList<string> roles)
        {
            Token = token;
            User = new UserResponse(user, roles);
        }
    }
}
