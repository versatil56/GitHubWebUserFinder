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
		public async Task<HttpResponseMessage> Get(string url)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(url);
				client.DefaultRequestHeaders.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				return await client.GetAsync(url);
			}
		}
	}
}