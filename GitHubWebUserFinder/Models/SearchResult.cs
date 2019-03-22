using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GitHubWebUserFinder.Models
{
	public class SearchResult
	{
		public GitHubUser User { get; set; }
	}
}