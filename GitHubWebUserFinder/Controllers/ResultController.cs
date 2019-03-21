using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GitHubWebUserFinder.Models;
using GitHubWebUserFinder.Services;

namespace GitHubWebUserFinder.Controllers
{
	public class ResultController : Controller
	{
		private readonly IGitHubSearchService _gitHubSearchService;
		public ResultController(IGitHubSearchService searchService)
		{
			_gitHubSearchService = searchService;
		}

		public async Task<ActionResult> GetResult(string searchCriteria)
		{
			var result = await _gitHubSearchService.FindUser(searchCriteria);
			return View(new SearchResult { User = result });
		}
	}
}