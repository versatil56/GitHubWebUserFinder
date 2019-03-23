using Newtonsoft.Json;

namespace GitHubWebUserFinder.Models
{
	public class GitHubRepository
	{
		public string Name { get; set; }
		[JsonProperty("html_url")]
		public string Url { get; set; }
		[JsonProperty("stargazers_count")]
		public int NumberOfStarGazers { get; set; }
	}
}