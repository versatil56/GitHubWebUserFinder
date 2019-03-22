using GitHubWebUserFinder.Models;
using System.Threading.Tasks;

namespace GitHubWebUserFinder.Connectors
{
	public interface IGitHubConnector
	{
		Task<GitHubUser> FindUser(string criteria);
	}
}