using System;
using System.Collections.Generic;
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
			connector.Setup(c => c.FindUser(It.IsAny<string>())).Returns(Task.FromResult(new GitHubUser{Repositories = new List<GitHubRepository>()}));

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
				FullName = "Juan Antonio",
				Repositories = new List<GitHubRepository>()
			};

			connector.Setup(c => c.FindUser(It.IsAny<string>())).Returns(Task.FromResult(expectedResult));

			ResultController controller = new ResultController(new GitHubSearchService(connector.Object));

			ViewResult result = (ViewResult)await controller.GetResult(expectedResult.Alias);
			SearchResult user = (SearchResult) result.Model;

			Assert.AreEqual(user.User.FullName, expectedResult.FullName);
		}

		[Test]
		public async Task ShowResult_WhenCalled_IfNoLocation_WillInitializeitToNotApplicable()
		{
			Mock<IGitHubConnector> connector = new Mock<IGitHubConnector>();
			GitHubUser expectedResult = new GitHubUser
			{
				Alias = "My Test",
				FullName = "Juan Antonio",
				Repositories = new List<GitHubRepository>()
			};

			connector.Setup(c => c.FindUser(It.IsAny<string>())).Returns(Task.FromResult(expectedResult));

			ResultController controller = new ResultController(new GitHubSearchService(connector.Object));

			ViewResult result = (ViewResult)await controller.GetResult(expectedResult.Alias);
			SearchResult user = (SearchResult)result.Model;

			Assert.AreEqual(user.User.CurrentLocation, "N/A");
		}

		[Test]
		public async Task ShowResult_WhenCalled_IfNoRepositories_ThenReturnViewWillMessageUsageSayingNoRepositories()
		{
			Mock<IGitHubConnector> connector = new Mock<IGitHubConnector>();
			GitHubUser expectedResult = new GitHubUser
			{
				Alias = "My Test",
				FullName = "Juan Antonio",
				Repositories = new List<GitHubRepository>()
			};

			connector.Setup(c => c.FindUser(It.IsAny<string>())).Returns(Task.FromResult(expectedResult));

			ResultController controller = new ResultController(new GitHubSearchService(connector.Object));

			ViewResult result = (ViewResult)await controller.GetResult(expectedResult.Alias);
			SearchResult user = (SearchResult)result.Model;

			Assert.AreEqual(user.User.NoRepositoriesMessage, "It looks like this user has no repositories");
		}

		[Test]
		public async Task ShowNotFound_WhenNotFoundExceptionIsRaised()
		{
			Mock<IGitHubConnector> connector = new Mock<IGitHubConnector>();

			ResultController controller = new ResultController(new GitHubSearchService(connector.Object));

			ExceptionContext exceptionContext = new ExceptionContext
			{
				Exception = new GitHubUserNotFoundException("error", new Exception())
			};

			((IExceptionFilter)controller).OnException(exceptionContext);

			RedirectToRouteResult result = (RedirectToRouteResult) exceptionContext.Result;

			Assert.That(result.RouteValues.ContainsValue("NotFound"));
			Assert.That(result.RouteValues.ContainsValue("Error"));
		}

		[Test]
		public async Task ShowNotAvailable_WhenSearchFunctionalityNotAvailableException()
		{
			Mock<IGitHubConnector> connector = new Mock<IGitHubConnector>();

			ResultController controller = new ResultController(new GitHubSearchService(connector.Object));

			ExceptionContext exceptionContext = new ExceptionContext
			{
				Exception = new SearchFunctionalityNotAvailableException("error", new Exception())
			};

			((IExceptionFilter)controller).OnException(exceptionContext);

			RedirectToRouteResult result = (RedirectToRouteResult)exceptionContext.Result;

			Assert.That(result.RouteValues.ContainsValue("NotAvailable"));
			Assert.That(result.RouteValues.ContainsValue("Error"));
		}
	}
}
