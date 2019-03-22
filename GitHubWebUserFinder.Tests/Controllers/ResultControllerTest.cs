using System.Web.Mvc;
using GitHubWebUserFinder.Controllers;
using GitHubWebUserFinder.Models;
using GitHubWebUserFinder.Services;
using System.Threading.Tasks;
using GitHubWebUserFinder.Connectors;
using Moq;
using NUnit.Framework;

namespace GitHubWebUserFinder.Tests.Controllers
{
	[TestFixture]
	public class ResultControllerTest
	{
		[Test]
		public async Task ShowResult_WhenCalled_WillReturn_ASearchResult()
		{
			Mock<IGitHubConnector> connector = new Mock<IGitHubConnector>();
			connector.Setup(c => c.FindUser(It.IsAny<string>())).Returns(Task.FromResult(new GitHubUser()));

			ResultController controller = new ResultController(new GitHubSearchService(connector.Object));
			ViewResult result = (ViewResult)await controller.GetResult("test");

			Assert.AreEqual(result.Model.GetType(), typeof(SearchResult));
		}

		[Test]
		public async Task ShowResult_WhenCalled_WillContain_NameOfUser()
		{
			Mock<IGitHubConnector> connector = new Mock<IGitHubConnector>();
			GitHubUser expectedResult = new GitHubUser
			{
				Alias = "My Test",
				FullName = "Juan Antonio"
			};

			connector.Setup(c => c.FindUser(It.IsAny<string>())).Returns(Task.FromResult(expectedResult));

			ResultController controller = new ResultController(new GitHubSearchService(connector.Object));

			ViewResult result = (ViewResult)await controller.GetResult(expectedResult.Alias);
			SearchResult user = (SearchResult) result.Model;

			Assert.AreEqual(user.User.FullName, expectedResult.FullName);
		}
	}
}
