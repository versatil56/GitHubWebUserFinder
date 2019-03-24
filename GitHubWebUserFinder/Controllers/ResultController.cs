using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GitHubWebUserFinder.Connectors;
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

		[Route("Result/Search/{searchCriteria}")]
		public async Task<ActionResult> GetResult(string searchCriteria)
		{
			var result = await _gitHubSearchService.FindUser(searchCriteria);
			return View(new SearchResult { User = result });
		}

		public async Task<ActionResult> NotFound()
		{
			return View();
		}

		public async Task<ActionResult> NotAvailable()
		{
			return View();
		}


		protected override void OnException(ExceptionContext filterContext)
		{
			filterContext.ExceptionHandled = true;

			if (filterContext.Exception is GitHubUserNotFoundException)
				filterContext.Result = RedirectToAction("NotFound", "Result");

			if (filterContext.Exception is SearchFunctionalityNotAvailableException)
				filterContext.Result = RedirectToAction("NotAvailable", "Result");

		}
	}
}