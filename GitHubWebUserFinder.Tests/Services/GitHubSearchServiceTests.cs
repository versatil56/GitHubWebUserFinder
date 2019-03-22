using System.Threading.Tasks;
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
			var service = new GitHubSearchService();

			var result = await service.FindUser("test");

			Assert.AreEqual(result.FullName, "test");
		}
	}
}
