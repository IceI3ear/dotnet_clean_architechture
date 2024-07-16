using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBank.Core.ViewModels.Response
{
	public class PagedResponse<T>
	{
		public int Page { get; set; }
		public int Limit { get; set; }
		public int TotalItems { get; set; }
		public int TotalPages { get; set; }
		public IList<T> Items { get; set; }
		public PagedResponse()
		{
			Items = new List<T>();
		}
	}
}
