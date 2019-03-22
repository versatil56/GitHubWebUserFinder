using System.Net.Http;
using System.Threading.Tasks;

namespace GitHubWebUserFinder.Connectors
{
	public interface IAppHttpClient
	{
		Task<HttpResponseMessage> Get(string url);
	}
}