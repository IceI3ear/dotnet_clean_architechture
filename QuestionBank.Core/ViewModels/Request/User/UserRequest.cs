using Microsoft.AspNetCore.Http;
using QuestionBank.Core.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBank.Core.ViewModels.Request.User
{
	public class UserRequest
	{
        public string UserName { get; set; }
        public string Email { get; set; }
		public string? PhoneNumber { get; set; }
        public IList<int> GroupIds { get; set; }
        public string[] Roles { get; set; }
    }
}
