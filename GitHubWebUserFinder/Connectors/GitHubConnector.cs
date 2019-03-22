using GitHubWebUserFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace GitHubWebUserFinder.Connectors
{
	public interface IAppHttpClient
	{
		Task<HttpResponseMessage> Get(string url);
	}

	public class AppHttpClient  : IAppHttpClient
	{
		private readonly HttpClient _httpClient;

		public AppHttpClient()
		{
			_httpClient = new HttpClient();
		}

		public async Task<HttpResponseMessage> Get(string url)
		{
			_httpClient.BaseAddress = new Uri(url);
			_httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
			_httpClient.DefaultRequestHeaders.Accept.Clear();
			_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			return await _httpClient.GetAsync(url);
		}
	}

	public interface IGitHubConnector
	{
		Task<GitHubUser> FindUser(string criteria);
	}
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