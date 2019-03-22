using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using GitHubWebUserFinder.Models;

namespace GitHubWebUserFinder.Services
{
	public interface IGitHubSearchService
	{
		Task<GitHubUser> FindUser(string name);
	}
	public class GitHubSearchService : IGitHubSearchService
	{
		public async Task<GitHubUser> FindUser(string name)
		{
			return  new GitHubUser {FullName = name };
		}
	}
}