using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GitHubWebUserFinder.Models;
using GitHubWebUserFinder.Services;

namespace GitHubWebUserFinder.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Search(UserSearch userSearch)
		{
			if (ModelState.IsValid)
			{
				return RedirectToAction("GetResult", "Result", new { searchCriteria = userSearch.Criteria });
			}

			return View("Index", userSearch);
		}
	}
}