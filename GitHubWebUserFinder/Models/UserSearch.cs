using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GitHubWebUserFinder.Models
{
	public class UserSearch
	{
		[Required]
		public string Criteria { get; set; }
	}
}