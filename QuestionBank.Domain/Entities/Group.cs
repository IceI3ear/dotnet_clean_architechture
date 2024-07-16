using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBank.Domain.Entities
{
	public class Group : BaseModel
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public ICollection<UserGroup> UserGroups { get; set; }

	}
}
