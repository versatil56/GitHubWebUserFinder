using GitHubWebUserFinder.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using System.Net.Http;
using System;
using System.Net;

namespace GitHubWebUserFinder.Connectors
{
	public class GitHubConnector : IGitHubConnector
	{
		private readonly IAppHttpClient _client;

		public GitHubConnector(IAppHttpClient client)
		{
			_client = client;
		}

		public async Task<GitHubUser> FindUser(string criteria)
		{
			string uri = $"/users/{criteria}";

			HttpResponseMessage getUserCall = await _client.Get(uri);

			try
			{
				getUserCall.EnsureSuccessStatusCode();

				string rawUserResponse = await getUserCall.Content.ReadAsStringAsync();

				GitHubUser user = JsonConvert.DeserializeObject<GitHubUser>(rawUserResponse);

				List<GitHubRepository> repositories = await GetUserRepositories(criteria);

				user.Repositories = repositories;

				return user;
			}
			catch (Exception exception)
			{
				switch (getUserCall.StatusCode)
				{
					case HttpStatusCode.NotFound:
						throw new GitHubUserNotFoundException("GitHub user not found", exception);
					case HttpStatusCode.ServiceUnavailable:
						throw new SearchFunctionalityNotAvailableException("Find functionality is not available at the minute", exception);
					default:
						throw exception;
				}
			}
		}

		private async Task<List<GitHubRepository>> GetUserRepositories(string userName)
		{
			string uri = $"/users/{userName}/repos";

			HttpResponseMessage getUserRepositoriesCall = await _client.Get(uri);

			getUserRepositoriesCall.EnsureSuccessStatusCode();

			string rawResponse = await getUserRepositoriesCall.Content.ReadAsStringAsync();

			return JsonConvert.DeserializeObject<List<GitHubRepository>>(rawResponse);
		}
	}
}