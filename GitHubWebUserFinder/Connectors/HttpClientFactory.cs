using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace GitHubWebUserFinder.Connectors
{
	public static class HttpClientFactory
	{
		public static Func<HttpClient> GitHubClientFactory = () => {
			var client = new HttpClient
			{
				BaseAddress = new Uri("https://api.github.com/")
			};
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			return client;
		};

		private static Lazy<HttpClient> _gitHubClient = new Lazy<HttpClient>(GitHubClientFactory);

		public static HttpClient GitHubHttpClient
		{
			get { return _gitHubClient.Value; }
		}
	}
}