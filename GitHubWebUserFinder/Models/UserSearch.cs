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
		[RegularExpression("^[a-z\\d](?:[a-z\\d]|-(?=[a-z\\d])){0,38}$", ErrorMessage = "Search may only contain alphanumeric characters or single hyphens, and cannot begin or end with a hyphen")]
		public string Criteria { get; set; }
	}
}