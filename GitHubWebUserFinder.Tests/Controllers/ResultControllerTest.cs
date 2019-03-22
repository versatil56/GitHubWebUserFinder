using System.Web.Mvc;
using GitHubWebUserFinder.Controllers;
using GitHubWebUserFinder.Models;
using GitHubWebUserFinder.Services;
using System.Threading.Tasks;
using NUnit.Framework;

namespace GitHubWebUserFinder.Tests.Controllers
{
	[TestFixture]
	public class ResultControllerTest
	{
		[Test]
		public async Task ShowResult_WhenCalled_WillReturn_ASearchResult()
		{
			ResultController controller = new ResultController(new GitHubSearchService());

			ViewResult result = (ViewResult)await controller.GetResult("test");

			Assert.AreEqual(result.Model.GetType(), typeof(SearchResult));
		}

		[Test]
		public async Task ShowResult_WhenCalled_WillContain_NameOfUser()
		{
			ResultController controller = new ResultController(new GitHubSearchService());

			ViewResult result = (ViewResult)await controller.GetResult("My Test");
			var user = (SearchResult) result.Model;

			Assert.AreEqual(user.User.FullName, "My Test");
		}
	}
}
