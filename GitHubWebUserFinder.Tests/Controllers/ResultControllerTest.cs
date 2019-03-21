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
	public class ResultControllerTest
	{
		[TestMethod]
		public void ShowResult_WhenCalled_WillReturn_ASearchResult()
		{
			ResultController controller = new ResultController();

			ViewResult result = (ViewResult)controller.ShowResult("test");

			Assert.IsInstanceOfType(result.Model, typeof(SearchResult));
		}
	}
}
