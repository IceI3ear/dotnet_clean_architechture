using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QuestionBank.Domain.Entities
{
	public class AppUser : IdentityUser<int>, IBaseModel
	{
		public bool IsDeleted { get; set; }
		public DateTime DeletedAt { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public string? Avatar { get; set; }
        public ICollection<UserGroup> UserGroups { get; set; }
        public void UpdateTimeStamps()
		{
			if (!IsDeleted && Id > 0)
			{
				UpdatedAt = DateTime.UtcNow;
			}
			else if (!IsDeleted && Id == 0)
			{
				CreatedAt = DateTime.UtcNow;
			}
			else if (IsDeleted && Id > 0)
			{
				DeletedAt = DateTime.UtcNow;
			}
			else if (IsDeleted && Id == 0)
			{
				throw new NotSupportedException("Cannot delete a new entity");
			}
		}
	}
}
