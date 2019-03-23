using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace GitHubWebUserFinder.Connectors
{
	public static class HttpClientFactory
	{
		public static Func<HttpClient> ClientFactory = () => {
			var client = new HttpClient
			{
				BaseAddress = new Uri("https://api.github.com/")
			};
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			return client;
		};

		private static Lazy<HttpClient> _client = new Lazy<HttpClient>(ClientFactory);

		public static HttpClient HttpClient
		{
			get { return _client.Value; }
		}
	}
}