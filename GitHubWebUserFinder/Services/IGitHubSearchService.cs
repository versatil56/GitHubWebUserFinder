using System.Threading.Tasks;
using GitHubWebUserFinder.Models;

namespace GitHubWebUserFinder.Services
{
	public interface IGitHubSearchService
	{
		Task<GitHubUser> FindUser(string name);
	}
}