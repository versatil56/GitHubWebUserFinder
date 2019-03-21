using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GitHubWebUserFinder.Models;

namespace GitHubWebUserFinder.Controllers
{
	public class ResultController : Controller
	{
		public ActionResult ShowResult(string searchCriteria)
		{
			return View(new SearchResult { Name = searchCriteria});
		}
	}
}