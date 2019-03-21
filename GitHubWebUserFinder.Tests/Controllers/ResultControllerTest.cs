using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GitHubWebUserFinder;
using GitHubWebUserFinder.Controllers;
using GitHubWebUserFinder.Models;
using GitHubWebUserFinder.Services;
using System.Threading.Tasks;

namespace GitHubWebUserFinder.Tests.Controllers
{
	[TestClass]
	public class ResultControllerTest
	{
		[TestMethod]
		public async Task ShowResult_WhenCalled_WillReturn_ASearchResult()
		{
			ResultController controller = new ResultController(new GitHubSearchService());

			ViewResult result = (ViewResult)await controller.GetResult("test");

			Assert.IsInstanceOfType(result.Model, typeof(SearchResult));
		}

		[TestMethod]
		public async Task ShowResult_WhenCalled_WillContain_NameOfUser()
		{
			ResultController controller = new ResultController(new GitHubSearchService());

			ViewResult result = (ViewResult)await controller.GetResult("My Test");
			var user = (SearchResult) result.Model;

			Assert.AreEqual(user.User.Name,"My Test");
		}
	}
}
