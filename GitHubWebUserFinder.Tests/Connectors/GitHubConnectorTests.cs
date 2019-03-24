using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FsCheck;
using GitHubWebUserFinder.Connectors;
using GitHubWebUserFinder.Models;
using GitHubWebUserFinder.Tests.Models;
using Moq;
using NUnit.Framework;

namespace GitHubWebUserFinder.Tests.Services
{
	[TestFixture]
	public class GitHubConnectorTests
	{
		[Test]
		public async Task AGitHubConnector_WillReturnAGitHubUser_WhenSearching()
		{
			HttpResponseMessage expectedResponseFromGitHub = new HttpResponseMessage
			{
				Content = new StringContent(@"{login:'test'}")
			};

			HttpResponseMessage expectedRepositoriesResponse = new HttpResponseMessage
			{
				Content = new StringContent("[]")
			};

			Mock<IAppHttpClient> appHttpClient = new Mock<IAppHttpClient>();
			appHttpClient.SetupSequence(c => c.Get(It.IsAny<string>()))
				.ReturnsAsync(expectedResponseFromGitHub)
				.ReturnsAsync(expectedRepositoriesResponse);

			GitHubConnector connector = new GitHubConnector(appHttpClient.Object);
			GitHubUser result = await connector.FindUser("test");

			Assert.AreEqual(result.GetType(), typeof(GitHubUser));
		}

		[Test]
		public async Task AGitHubConnector_WillReturnAGitHubUser_WhenSearchingSuccessfully()
		{
			Newtonsoft.Json.Linq.JObject expectedUser = Generators.JsonGitHubUser.Sample(50, 1).First();
			HttpResponseMessage expectedResponseFromGitHub = new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.OK,
				Content = new StringContent(expectedUser.ToString())
			};

			HttpResponseMessage expectedRepositoriesResponse = new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.OK,
				Content = new StringContent("[]")
			};

			Mock<IAppHttpClient> appHttpClient = new Mock<IAppHttpClient>();
			appHttpClient.SetupSequence(c => c.Get(It.IsAny<string>()))
				.ReturnsAsync(expectedResponseFromGitHub)
				.ReturnsAsync(expectedRepositoriesResponse);

			GitHubConnector connector = new GitHubConnector(appHttpClient.Object);
			GitHubUser result = await connector.FindUser("test");

			Assert.AreEqual(result.FullName, expectedUser.GetValue("name").ToString());
		}

		[Test]
		public async Task AGitHubConnector_WillReturnAGitHubUsersRepositories_WhenSearchingSuccessfullyForUser_AndThereIsReposAvailable()
		{
			HttpResponseMessage expectedResponseFromGitHub = new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.OK,
				Content = new StringContent(@"{login:'test'}")
			};

			Newtonsoft.Json.Linq.JArray expectedRepositories = Generators.JsonGitHubRepositories.Sample(50,1).First();

			HttpResponseMessage expectedRepositoriesResponse = new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.OK,
				Content = new StringContent(expectedRepositories.ToString())
			};

			Mock<IAppHttpClient> appHttpClient = new Mock<IAppHttpClient>();
			appHttpClient.SetupSequence(c => c.Get(It.IsAny<string>()))
				.ReturnsAsync(expectedResponseFromGitHub)
				.ReturnsAsync(expectedRepositoriesResponse);

			GitHubConnector connector = new GitHubConnector(appHttpClient.Object);
			GitHubUser result = await connector.FindUser("test");

			Assert.AreEqual(result.Repositories.ToList().Count, expectedRepositories.Count);
		}

		[Test]
		public async Task AGitHubConnector_WillThrowA404Exception_IfUserDoesNotExist()
		{
			HttpResponseMessage expectedResponseFromGitHub = new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.NotFound,
				Content = new StringContent(@"User has not been found")
			};

			Mock<IAppHttpClient> appHttpClient = new Mock<IAppHttpClient>();
			appHttpClient.Setup(c => c.Get(It.IsAny<string>()))
				.ReturnsAsync(expectedResponseFromGitHub);


			GitHubConnector connector = new GitHubConnector(appHttpClient.Object);

			Assert.That(async() => await connector.FindUser("test"), Throws.Exception.TypeOf<GitHubUserNotFoundException>().And.Message.EqualTo("GitHub user not found"));
		}

		[Test]
		public async Task AGitHubConnector_WillThrowServiceUnavailableException_IfGitHubIsNotAvailable()
		{
			HttpResponseMessage expectedResponseFromGitHub = new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.ServiceUnavailable,
				Content = new StringContent(@"Git is not ready at the moment")
			};

			Mock<IAppHttpClient> appHttpClient = new Mock<IAppHttpClient>();
			appHttpClient.Setup(c => c.Get(It.IsAny<string>()))
				.ReturnsAsync(expectedResponseFromGitHub);


			GitHubConnector connector = new GitHubConnector(appHttpClient.Object);

			Assert.That(async () => await connector.FindUser("test"), Throws.Exception.TypeOf<SearchFunctionalityNotAvailableException>().And.Message.EqualTo("Find functionality is not available at the minute"));
		}

		[Test]
		public async Task AGitHubConnector_WhenThrowingUnrecognizedException_WillBubbleUpHttpRequestException()
		{
			HttpResponseMessage expectedResponseFromGitHub = new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.UpgradeRequired,
				Content = new StringContent(@"We have a problem")
			};

			Mock<IAppHttpClient> appHttpClient = new Mock<IAppHttpClient>();
			appHttpClient.Setup(c => c.Get(It.IsAny<string>()))
				.ReturnsAsync(expectedResponseFromGitHub);


			GitHubConnector connector = new GitHubConnector(appHttpClient.Object);

			Assert.That(async () => await connector.FindUser("test"), Throws.Exception.TypeOf<HttpRequestException>());
		}

		[Test]
		public async Task
			AGitHubConnector_WhenFetchingUsersRepositories_IfNotSuccess_ThenConsiderTheWholeTransactionAsFailed_AndThrowExceptionAccordingly()
		{
			HttpResponseMessage expectedResponseFromGitHub = new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.OK,
				Content = new StringContent(@"{login:'test'}")
			};

			Newtonsoft.Json.Linq.JArray expectedRepositories = Generators.JsonGitHubRepositories.Sample(50, 1).First();

			HttpResponseMessage expectedRepositoriesResponse = new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.UpgradeRequired,
				Content = new StringContent(@"We have a problem")
			};

			Mock<IAppHttpClient> appHttpClient = new Mock<IAppHttpClient>();
			appHttpClient.SetupSequence(c => c.Get(It.IsAny<string>()))
				.ReturnsAsync(expectedResponseFromGitHub)
				.ReturnsAsync(expectedRepositoriesResponse);

			GitHubConnector connector = new GitHubConnector(appHttpClient.Object);

			Assert.That(async () => await connector.FindUser("test"), Throws.Exception.TypeOf<HttpRequestException>());
		}
	}
}
