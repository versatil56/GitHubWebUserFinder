using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FsCheck;
using GitHubWebUserFinder.Connectors;
using GitHubWebUserFinder.Models;
using GitHubWebUserFinder.Services;
using GitHubWebUserFinder.Tests.Models;
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
				.Returns(Task.FromResult(new GitHubUser { Repositories = new List<GitHubRepository>()}));

			GitHubSearchService service = new GitHubSearchService(connector.Object);

			GitHubUser result = await service.FindUser("test");

			Assert.AreEqual(result.GetType(), typeof(GitHubUser));
		}

		[Test]
		public async Task AGitHubSearchService_WithSuccessfulSearch_WillReturnOnlyFiveOrLessRepositories()
		{
			var user = Generators.GitHubUser.Sample(50, 1).First();
			var repos = Generators.GitHubRepositories.Sample(50, 1).First();
			user.Repositories = repos;

			Mock<IGitHubConnector> connector = new Mock<IGitHubConnector>();
			connector.Setup(c => c.FindUser(It.IsAny<string>()))
				.Returns(Task.FromResult(user));

			GitHubSearchService service = new GitHubSearchService(connector.Object);

			GitHubUser result = await service.FindUser("test");

			Assert.LessOrEqual(result.Repositories.ToList().Count,5);
		}

		[Test]
		public async Task GitHubSearchService_WithSuccessfulSearch_WillReturnRepositoriesSortedByStarGazeCount()
		{
			var user = Generators.GitHubUser.Sample(50, 1).First();
			var repos = Generators.GitHubRepositories.Sample(50, 1).Single(c => c.Count > 5);
			user.Repositories = repos;

			var highestStarGaze = repos.OrderByDescending(c => c.NumberOfStarGazers).First();
			var lowestStarGaze = repos.OrderByDescending(c => c.NumberOfStarGazers).Take(5).Last();

			Mock<IGitHubConnector> connector = new Mock<IGitHubConnector>();
			connector.Setup(c => c.FindUser(It.IsAny<string>()))
				.Returns(Task.FromResult(user));

			GitHubSearchService service = new GitHubSearchService(connector.Object);

			GitHubUser result = await service.FindUser("test");

			Assert.AreEqual(highestStarGaze.NumberOfStarGazers, result.Repositories.First().NumberOfStarGazers);
			Assert.AreEqual(lowestStarGaze.NumberOfStarGazers, result.Repositories.Last().NumberOfStarGazers);
		}
	}
}
