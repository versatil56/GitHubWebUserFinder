using GitHubWebUserFinder.Models;
using Newtonsoft.Json;
using FsCheck;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace GitHubWebUserFinder.Tests.Models
{
	[TestFixture]
	public class GitHubUserTests
	{
		[Test]
		public void AGitJsonPayload_ContainingAGitHubUser_WillMapCorrectlyToFullName()
		{
			Prop.ForAll(Generators.JsonGitHubUser.ToArbitrary(), json =>
			{
				dynamic user = json;
				var expectedFullName = user.name.ToString();
				var result = JsonConvert.DeserializeObject<GitHubUser>(json.ToString());

				Assert.AreEqual(expectedFullName, result.FullName);
			}).QuickCheck();
		}

		[Test]
		public void AGitJsonPayload_ContainingAGitHubUser_WillMapCorrectlyToCurrentLocation()
		{
			Prop.ForAll(Generators.JsonGitHubUser.ToArbitrary(), json =>
			{
				dynamic user = json;
				var expectedLocation = user.location.ToString();
				var result = JsonConvert.DeserializeObject<GitHubUser>(json.ToString());

				Assert.AreEqual(expectedLocation, result.CurrentLocation);
			}).QuickCheck();
		}

		[Test]
		public void AGitJsonPayload_ContainingAGitHubUser_WillMapCorrectlyToAlias()
		{
			Prop.ForAll(Generators.JsonGitHubUser.ToArbitrary(), json =>
			{
				dynamic user = json;
				var expectedAlias = user.login.ToString();
				var result = JsonConvert.DeserializeObject<GitHubUser>(json.ToString());

				Assert.AreEqual(expectedAlias, result.Alias);
			}).QuickCheck();
		}

		[Test]
		public void AGitJsonPayload_ContainingAGitHubUser_WillMapCorrectlyToAvatarUrl()
		{
			Prop.ForAll(Generators.JsonGitHubUser.ToArbitrary(), json =>
			{
				dynamic user = json;
				var expectedAvatarUrl = user.avatar_url.ToString();
				var result = JsonConvert.DeserializeObject<GitHubUser>(json.ToString());

				Assert.AreEqual(expectedAvatarUrl, result.AvatarUrl);
			}).QuickCheck();
		}
	}
}
