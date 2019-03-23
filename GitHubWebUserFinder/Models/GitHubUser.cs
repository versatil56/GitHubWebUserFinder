using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace GitHubWebUserFinder.Models
{
	public class GitHubUser
	{
		[JsonProperty("name")]
		public string FullName { get; set; }
		[JsonProperty("location")]
		public string CurrentLocation { get; set; }
		[JsonProperty("login")]
		public string Alias { get; set; }
		[JsonProperty("avatar_url")]
		public string AvatarUrl { get; set; }
		public List<GitHubRepository> Repositories { get; set; }
	}
}