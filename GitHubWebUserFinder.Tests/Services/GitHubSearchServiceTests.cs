using System.Threading.Tasks;
using GitHubWebUserFinder.Connectors;
using GitHubWebUserFinder.Models;
using GitHubWebUserFinder.Services;
using NUnit.Framework;

namespace GitHubWebUserFinder.Tests.Services
{
	[TestFixture]
	public class GitHubSearchServiceTests
	{
		[Test]
		public async Task AGitHubSearchService_WillReturnAGitHubUser_WhenSearching()
		{
			GitHubConnector connector = new GitHubConnector();
			GitHubSearchService service = new GitHubSearchService(connector);

			GitHubWebUserFinder.Models.GitHubUser result = await service.FindUser("test");

			Assert.AreEqual(result.GetType(), typeof(GitHubUser));
		}
	}
}
