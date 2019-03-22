using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using GitHubWebUserFinder.Connectors;
using GitHubWebUserFinder.Services;
using Unity;
using Unity.Mvc5;

namespace GitHubWebUserFinder
{
	[ExcludeFromCodeCoverage]
	public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            container.RegisterType<IGitHubSearchService, GitHubSearchService>();
            container.RegisterType<IGitHubConnector, GitHubConnector>();
            container.RegisterType<IAppHttpClient, AppHttpClient>();
        }
	}
}