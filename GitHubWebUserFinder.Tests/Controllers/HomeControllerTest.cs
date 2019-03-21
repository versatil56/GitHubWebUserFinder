using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GitHubWebUserFinder;
using GitHubWebUserFinder.Controllers;
using GitHubWebUserFinder.Models;

namespace GitHubWebUserFinder.Tests.Controllers
{
	[TestClass]
	public class HomeControllerTest
	{
		[TestMethod]
		public void Index_WhenCalled_WillRender()
		{
			HomeController controller = new HomeController();

			ViewResult result = controller.Index() as ViewResult;

			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void When_Submitting_InvalidSearch_WillReturnViewAgain()
		{
			HomeController controller = new HomeController();

			controller.ModelState.AddModelError("SessionName", "Required");
			ViewResult result = controller.Search(new UserSearch()) as ViewResult;

			Assert.AreEqual(result.ViewName,"Index");
		}

		[TestMethod]
		public void When_Submitting_ValidSearch_ThenWeAreRedirected_ToResult()
		{
			HomeController controller = new HomeController();

			RedirectToRouteResult result = controller.Search(new UserSearch()) as RedirectToRouteResult;

			Assert.AreEqual(result.RouteValues["controller"],"Result");
			Assert.AreEqual(result.RouteValues["action"], "ShowResult");
		}

		[TestMethod]
		public void When_Submitting_ValidSearch_WeSendSearchCriteria_ToResult()
		{
			HomeController controller = new HomeController();

			RedirectToRouteResult result = controller.Search(new UserSearch{Criteria = "Test"}) as RedirectToRouteResult;

			Assert.AreEqual(result.RouteValues["searchCriteria"], "Test");
		}
	}
}
