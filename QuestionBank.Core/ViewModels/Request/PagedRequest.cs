using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBank.Core.ViewModels.Request
{
	public class PagedRequest
	{
		public string? SearchText { get; set; } = default!;
		public int CurrentPage { get; set; } = 0;
		public int PageSize { get; set; } = 10;
		public string SortColumn { get; set; } = "CreatedAt";
        public string SortOrder { get; set; } = "ASC";
    }
}
