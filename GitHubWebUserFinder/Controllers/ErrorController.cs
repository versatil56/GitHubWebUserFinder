using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GitHubWebUserFinder.Models;
using GitHubWebUserFinder.Services;

namespace GitHubWebUserFinder.Controllers
{
	public class ErrorController : Controller
	{
		public ActionResult SomethingWentWrong()
		{
			return View();
		}

		public ActionResult NotFound()
		{
			return View();
		}

		public ActionResult NotAvailable()
		{
			return View();
		}
	}
}