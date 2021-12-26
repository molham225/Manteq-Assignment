using Api.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Model.Common
{
	public class PaginationSortAndSearchInfo
	{
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public Dictionary<string, string> ColumnSearch { get; set; }
		public Dictionary<string, SortingDirectionEnum> SortBy { get; set; }

		public int Skip
		{
			get
			{
				return PageSize * (PageNumber - 1);
			}
		}

		public PaginationSortAndSearchInfo()
		{
			PageNumber = 1;
			PageSize = 10;
		}
	}
}
