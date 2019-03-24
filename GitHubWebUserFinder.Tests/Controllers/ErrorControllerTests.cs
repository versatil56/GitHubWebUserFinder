using System.Web.Mvc;
using GitHubWebUserFinder.Controllers;
using GitHubWebUserFinder.Models;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace GitHubWebUserFinder.Tests.Controllers
{
	[TestFixture]
	public class ErrorControllerTest
	{
		[Test]
		public void SomethingWentWrong_WhenCalled_WillRender()
		{
			ErrorController controller = new ErrorController();

			ViewResult result = controller.SomethingWentWrong() as ViewResult;

			Assert.NotNull(result);
		}

		[Test]
		public void NotFound_WhenCalled_WillRender()
		{
			ErrorController controller = new ErrorController();

			ViewResult result = controller.NotFound() as ViewResult;

			Assert.NotNull(result);
		}

		[Test]
		public void NotUnavailable_WhenCalled_WillRender()
		{
			ErrorController controller = new ErrorController();

			ViewResult result = controller.NotAvailable() as ViewResult;

			Assert.NotNull(result);
		}
	}
}
