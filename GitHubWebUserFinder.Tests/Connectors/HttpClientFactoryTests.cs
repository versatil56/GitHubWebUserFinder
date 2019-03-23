using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GitHubWebUserFinder.Connectors;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace GitHubWebUserFinder.Tests.Connectors
{
	[TestFixture]
	class HttpClientFactoryTests
	{
		[Test]
		public void AHttpClientFactory_CanGenerateAHttpClient()
		{
			var result = HttpClientFactory.HttpClient;

			Assert.AreEqual(typeof(HttpClient),result.GetType());
		}

		[Test]
		public void AHttpClientFactory_WillGenerateAClient_WithTheRightUri()
		{
			var result = HttpClientFactory.HttpClient;

			Assert.AreEqual(result.BaseAddress.ToString(), "https://api.github.com/");
		}

		[Test]
		public void AHttpClientFactory_WillGenerateAClient_WithUserAgentHeaders()
		{
			var client = HttpClientFactory.HttpClient;
			var result = client.DefaultRequestHeaders.GetValues("user-agent").ToArray();

			Assert.AreEqual(result[0], "Mozilla/4.0");
			Assert.AreEqual(result[1], "(compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
		}

		[Test]
		public void AHttpClientFactory_WillGenerateAClient_WithRightContentType()
		{
			var client = HttpClientFactory.HttpClient;
			var result = client.DefaultRequestHeaders.Accept.ToString();

			Assert.AreEqual(result, "application/json");
		}
	}
}
