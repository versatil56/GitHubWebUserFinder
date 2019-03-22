using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Diagnostics.CodeAnalysis;

namespace GitHubWebUserFinder.Connectors
{
	[ExcludeFromCodeCoverage]
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
}