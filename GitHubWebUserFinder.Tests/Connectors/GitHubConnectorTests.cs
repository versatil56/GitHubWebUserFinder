using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GitHubWebUserFinder.Connectors;
using GitHubWebUserFinder.Models;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace GitHubWebUserFinder.Tests.Services
{
	[TestFixture]
	public class GitHubConnectorTests
	{
		[Test]
		public async Task AGitHubConnector_WillReturnAGitHubUser_WhenSearching()
		{
			var expectedResponseFromGitHub = new HttpResponseMessage
			{
				Content = new StringContent(@"{login:'test'}")
			};

			Mock<IAppHttpClient> appHttpClient = new Mock<IAppHttpClient>();
			appHttpClient.Setup(c => c.Get(It.IsAny<string>())).ReturnsAsync(expectedResponseFromGitHub);

			GitHubConnector connector = new GitHubConnector(appHttpClient.Object);
			GitHubUser result = await connector.FindUser("test");

			Assert.AreEqual(result.GetType(), typeof(GitHubUser));
		}

		[Test]
		public async Task AGitHubConnector_WillReturnAGitHubUser_WhenSearchingSuccessfully()
		{
			var expectedUser = @"{  
					login:'robconery',
					avatar_url:'https://avatars0.githubusercontent.com/u/78586?v=4',
					gravatar_id:'',
					name:'Rob Conery',
					company:'BigMachine',
					location:'Honolulu, HI'
				}";

			var expectedResult = JsonConvert.DeserializeObject<GitHubUser>(expectedUser);

			var expectedResponseFromGitHub = new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.OK,
				Content = new StringContent(expectedUser)
			};

			Mock<IAppHttpClient> appHttpClient = new Mock<IAppHttpClient>();
			appHttpClient.Setup(c => c.Get(It.IsAny<string>())).ReturnsAsync(expectedResponseFromGitHub);

			GitHubConnector connector = new GitHubConnector(appHttpClient.Object);
			GitHubUser result = await connector.FindUser("test");

			Assert.AreEqual(result.FullName, expectedResult.FullName);
		}
	}
}
