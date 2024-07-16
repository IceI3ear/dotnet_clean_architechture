using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBank.Core.ViewModels.Request.User
{
	public class UpdateUserRequest
	{
		[Required]
		public int UserId { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
        public string UserName { get; set; }
        public string AvtUrl { get; set; }
    }
}
