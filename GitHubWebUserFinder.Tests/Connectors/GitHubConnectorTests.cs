using System.Threading.Tasks;
using GitHubWebUserFinder.Connectors;
using GitHubWebUserFinder.Models;
using NUnit.Framework;

namespace GitHubWebUserFinder.Tests.Services
{
	[TestFixture]
	public class GitHubConnectorTests
	{
		[Test]
		public async Task AGitHubConnector_WillReturnAGitHubUser_WhenSearching()
		{
			var connector = new GitHubConnector();

			var result = await connector.FindUser("test");

			Assert.AreEqual(result.GetType(), typeof(GitHubUser));
		}
	}
}
