using QuestionBank.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBank.Core.ViewModels.Response.User
{
	public class UserResponse
	{
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Avatar { get; set; }
        public IList<string> Roles { get; set; } = new List<string>();
        public bool IsDelete { get; set; }

        public UserResponse() { }
        public UserResponse(AppUser user, IList<string> roles)
        {
            Id = user.Id;
            UserName = user.UserName;
            Email = user.Email;
            PhoneNumber = user.PhoneNumber;
            Avatar = user.Avatar;
            Roles = roles;
            IsDelete = user.IsDeleted;
        }

		public UserResponse(AppUser user)
		{
			Id = user.Id;
			UserName = user.UserName;
			Email = user.Email;
			PhoneNumber = user.PhoneNumber;
			Avatar = user.Avatar;
			IsDelete = user.IsDeleted;
		}
	}
}
