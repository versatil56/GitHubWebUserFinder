using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.Optimization;

namespace GitHubWebUserFinder
{
	[ExcludeFromCodeCoverage]
	public class BundleConfig
	{
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new StyleBundle("~/Content/css").Include(
					  "~/Content/bootstrap.css",
					  "~/Content/Site.css"));
		}
	}
}
