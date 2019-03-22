using System.Net.Http;
using System.Threading.Tasks;
using GitHubWebUserFinder.Connectors;
using GitHubWebUserFinder.Models;
using GitHubWebUserFinder.Services;
using Moq;
using NUnit.Framework;

namespace GitHubWebUserFinder.Tests.Services
{
	[TestFixture]
	public class GitHubSearchServiceTests
	{
		[Test]
		public async Task AGitHubSearchService_WillReturnAGitHubUser_WhenSearching()
		{
			Mock<IGitHubConnector> connector = new Mock<IGitHubConnector>();
			connector.Setup(c => c.FindUser(It.Is<string>(criteria => criteria == "test")))
				.Returns(Task.FromResult(new GitHubUser()));

			GitHubSearchService service = new GitHubSearchService(connector.Object);

			GitHubUser result = await service.FindUser("test");

			Assert.AreEqual(result.GetType(), typeof(GitHubUser));
		}
	}
}
