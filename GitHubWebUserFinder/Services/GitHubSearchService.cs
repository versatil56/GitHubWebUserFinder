using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using GitHubWebUserFinder.Connectors;
using GitHubWebUserFinder.Models;

namespace GitHubWebUserFinder.Services
{
	public class GitHubSearchService : IGitHubSearchService
	{
		private readonly IGitHubConnector _connector;

		public GitHubSearchService(IGitHubConnector connector)
		{
			_connector = connector;
		}

		public async Task<GitHubUser> FindUser(string name)
		{
			var searchResult = await _connector.FindUser(name);
			searchResult.Repositories = searchResult.Repositories.OrderByDescending(_ => _.NumberOfStarGazers).Take(5);

			return searchResult;
		}
	}
}