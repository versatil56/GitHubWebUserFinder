using System.Web.Mvc;
using GitHubWebUserFinder.Controllers;
using GitHubWebUserFinder.Models;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace GitHubWebUserFinder.Tests.Controllers
{
	[TestFixture]
	public class HomeControllerTest
	{
		[Test]
		public void Index_WhenCalled_WillRender()
		{
			HomeController controller = new HomeController();

			ViewResult result = controller.Index() as ViewResult;

			Assert.NotNull(result);
		}

		[Test]
		public void When_Submitting_InvalidSearch_WillReturnViewAgain()
		{
			HomeController controller = new HomeController();

			controller.ModelState.AddModelError("SessionName", "Required");
			ViewResult result = controller.Search(new UserSearch()) as ViewResult;

			Assert.AreEqual(result.ViewName,"Index");
		}

		[Test]
		public void When_Submitting_ValidSearch_ThenWeAreRedirected_ToResult()
		{
			HomeController controller = new HomeController();

			RedirectToRouteResult result = controller.Search(new UserSearch()) as RedirectToRouteResult;

			Assert.AreEqual(result.RouteValues["controller"],"Result");
			Assert.AreEqual(result.RouteValues["action"], "GetResult");
		}

		[Test]
		public void When_Submitting_ValidSearch_WeSendSearchCriteria_ToResult()
		{
			HomeController controller = new HomeController();

			RedirectToRouteResult result = controller.Search(new UserSearch{Criteria = "Test"}) as RedirectToRouteResult;

			Assert.AreEqual(result.RouteValues["searchCriteria"], "Test");
		}
	}
}
