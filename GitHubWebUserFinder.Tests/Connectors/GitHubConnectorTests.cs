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
			var expectedResponseFromGitHub = new HttpResponseMessage
			{
				Content = new StringContent(@"{login:'test'}")
			};

			var expectedRepositoriesResponse = new HttpResponseMessage
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
			var expectedUser = Generators.GitHubUser.Sample(50, 1).First();
			var expectedResponseFromGitHub = new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.OK,
				Content = new StringContent(expectedUser.ToString())
			};

			var expectedRepositoriesResponse = new HttpResponseMessage
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
			var expectedResponseFromGitHub = new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.OK,
				Content = new StringContent(@"{login:'test'}")
			};

			var expectedRepositories = Generators.GitHubRepositories.Sample(50,1).First();

			var expectedRepositoriesResponse = new HttpResponseMessage
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

			Assert.AreEqual(result.Repositories.Count, expectedRepositories.Count);
		}
	}
}
