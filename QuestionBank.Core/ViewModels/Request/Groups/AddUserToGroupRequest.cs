using QuestionBank.Core.ViewModels.Request.User;
using QuestionBank.Core.ViewModels.Response.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBank.Core.ViewModels.Request.Groups
{
	public class AddUserToGroupRequest
	{
		public AddUserToGroupRequest() { }
		public IList<UserRequest> Users { get; set; }
	}
}
