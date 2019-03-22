using GitHubWebUserFinder.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

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
			var uri = $"https://api.github.com/users/{criteria}";
			var getCall = await _client.Get(uri);

			string rawResponse = await getCall.Content.ReadAsStringAsync();

			return JsonConvert.DeserializeObject<GitHubUser>(rawResponse);
		}
	}
}