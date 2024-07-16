using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBank.Domain.Entities
{
	public class UserGroup
	{
		public int UserId { get; set; }
        public int GroupId { get; set; }
        public AppUser Users { get; set; }
        public Group Groups { get; set; }
    }
}
