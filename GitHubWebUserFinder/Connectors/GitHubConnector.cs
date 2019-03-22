using GitHubWebUserFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GitHubWebUserFinder.Connectors
{
	public interface IGitHubConnector
	{
		Task<GitHubUser> FindUser(string criteria);
	}
	public class GitHubConnector : IGitHubConnector
	{
		public async Task<GitHubUser> FindUser(string criteria)
		{
			return new GitHubUser
			{
				Alias = criteria,
				AvatarUrl = "test",
				CurrentLocation = "Newcastle Upon Tyne",
				FullName = "Juan Antonio"
			};
		}
	}
}