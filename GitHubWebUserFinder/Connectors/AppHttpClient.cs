using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace GitHubWebUserFinder.Connectors
{
	[ExcludeFromCodeCoverage]
	public class AppHttpClient  : IAppHttpClient
	{
		private readonly HttpClient _httpClient;

		public AppHttpClient()
		{
			_httpClient = HttpClientFactory.GitHubHttpClient;
		}

		public async Task<HttpResponseMessage> Get(string url)
		{
	    	return await _httpClient.GetAsync(url);
		}
	}
}