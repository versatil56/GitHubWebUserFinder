using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using GitHubWebUserFinder.Connectors;
using GitHubWebUserFinder.Models;
using GitHubWebUserFinder.Services;
using Microsoft.Ajax.Utilities;

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
			GitHubUser result = await _gitHubSearchService.FindUser(searchCriteria);

			var viewModel = InitialiseViewModel(result);

			return View(viewModel);
		}

		private static SearchResult InitialiseViewModel(GitHubUser result)
		{
			if (result.CurrentLocation.IsNullOrWhiteSpace())
				result.CurrentLocation = "N/A";

			if (result.Repositories.Count() == 0)
				result.NoRepositoriesMessage = "It looks like this user has no repositories";

			SearchResult viewModel = new SearchResult
			{
				User = result
			};
			return viewModel;
		}


		protected override void OnException(ExceptionContext filterContext)
		{
			filterContext.ExceptionHandled = true;

			if (filterContext.Exception is GitHubUserNotFoundException)
				filterContext.Result = RedirectToAction("NotFound", "Error");

			if (filterContext.Exception is SearchFunctionalityNotAvailableException)
				filterContext.Result = RedirectToAction("NotAvailable", "Error");

		}
	}
}