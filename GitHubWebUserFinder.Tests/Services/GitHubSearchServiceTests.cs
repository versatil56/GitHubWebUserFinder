using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitHubWebUserFinder.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GitHubWebUserFinder.Tests.Services
{
	[TestClass]
	public class GitHubSearchServiceTests
	{
		[TestMethod]
		public async Task AGitHubSearchService_WillReturnAGitHubUser_WhenSearching()
		{
			var service = new GitHubSearchService();

			var result = await service.FindUser("test");

			Assert.AreEqual(result.Name, "test");
		}
	}
}
