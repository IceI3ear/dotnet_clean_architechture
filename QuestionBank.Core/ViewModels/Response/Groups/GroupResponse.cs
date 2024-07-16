using QuestionBank.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBank.Core.ViewModels.Response.Groups
{
	public class GroupResponse
	{
		public GroupResponse() { }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
		public string Description { get; set; }
        public int TotalUser { get; set; }
        public bool IsDelete { get; set; }

        public GroupResponse(Group group, int totalUser)
		{
			GroupId = group.Id;
			GroupName = group.Name;
			Description = group.Description;
			TotalUser = totalUser;
			IsDelete = group.IsDeleted;
		}
	}
}
