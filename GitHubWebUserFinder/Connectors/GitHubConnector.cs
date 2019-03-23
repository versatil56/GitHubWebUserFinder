using GitHubWebUserFinder.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using System.Net.Http;

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
			string uri = $"https://api.github.com/users/{criteria}";

			HttpResponseMessage getUserCall = await _client.Get(uri);

			string rawUserResponse = await getUserCall.Content.ReadAsStringAsync();

			GitHubUser user = JsonConvert.DeserializeObject<GitHubUser>(rawUserResponse);

			List<GitHubRepository> repositories = await GetUserRepositories(criteria);

			user.Repositories = repositories;

			return user;
		}

		private async Task<List<GitHubRepository>> GetUserRepositories(string userName)
		{
			string uri = $"https://api.github.com/users/{userName}/repos";
			HttpResponseMessage getCall = await _client.Get(uri);

			string rawResponse = await getCall.Content.ReadAsStringAsync();

			return JsonConvert.DeserializeObject<List<GitHubRepository>>(rawResponse);
		}
	}
}