using Newtonsoft.Json.Linq;
using System.Linq;
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
		public void AGitJsonPayload_ContainingAName_WillMapCorrectlyToFullName()
		{
			Prop.ForAll(Generators.GitHubUserGenerator.ToArbitrary(), json =>
			{
				dynamic user = json;
				var expectedFullName = user.name.ToString();
				var result = JsonConvert.DeserializeObject<GitHubUser>(json.ToString());

				Assert.AreEqual(expectedFullName, result.FullName);
			}).QuickCheck();
		}

		[Test]
		public void AGitJsonPayload_ContainingALocation_WillMapCorrectlyToCurrentLocation()
		{
			Prop.ForAll(Generators.GitHubUserGenerator.ToArbitrary(), json =>
			{
				dynamic user = json;
				var expectedLocation = user.location.ToString();
				var result = JsonConvert.DeserializeObject<GitHubUser>(json.ToString());

				Assert.AreEqual(expectedLocation, result.CurrentLocation);
			}).QuickCheck();
		}

		[Test]
		public void AGitJsonPayload_ContainingALogin_WillMapCorrectlyToAlias()
		{
			Prop.ForAll(Generators.GitHubUserGenerator.ToArbitrary(), json =>
			{
				dynamic user = json;
				var expectedAlias = user.login.ToString();
				var result = JsonConvert.DeserializeObject<GitHubUser>(json.ToString());

				Assert.AreEqual(expectedAlias, result.Alias);
			}).QuickCheck();
		}

		[Test]
		public void AGitJsonPayload_ContainingAvatarUrl_WillMapCorrectlyToAvatarUrl()
		{
			Prop.ForAll(Generators.GitHubUserGenerator.ToArbitrary(), json =>
			{
				dynamic user = json;
				var expectedAvatarUrl = user.avatar_url.ToString();
				var result = JsonConvert.DeserializeObject<GitHubUser>(json.ToString());

				Assert.AreEqual(expectedAvatarUrl, result.AvatarUrl);
			}).QuickCheck();
		}
	}

	public static class Generators
	{
		private static string _alphabet = "abcdefghijklmnopqrstuvwxyz";
		private static string _numbers = "0123456789";

		private static Gen<NonEmptyString> NotEmptyString()
		{
			return Arb.Default.NonEmptyString().Generator;
		}

		public static Gen<int> RandomNumber(int min, int max)
		{
			return Gen.Choose(min, max);
		}

		public static Arbitrary<char[]> LettersGenerator()
		{
			return Gen.ArrayOf(RandomNumber(5, 100).Sample(20, 1).First(), Gen.Elements($"{_alphabet}{_alphabet.ToUpper()}".ToCharArray())).ToArbitrary();
		}

		public static Arbitrary<char[]> AlphanumericGenerator()
		{
			return Gen.ArrayOf(RandomNumber(5, 100).Sample(20, 1).First(), Gen.Elements($"{_alphabet}{_alphabet.ToUpper()}{_numbers}".ToCharArray())).ToArbitrary();
		}

		public static Gen<JObject> GitHubUserGenerator = from name in LettersGenerator().Generator
			from userLocation in AlphanumericGenerator().Generator
			from alias in AlphanumericGenerator().Generator
			from avatarurl in AlphanumericGenerator().Generator
			from car in AlphanumericGenerator().Generator
			select JObject.FromObject(new
			{
				name = new string(name),
				location = new string(userLocation),
				login = new string(alias),
				avatar_url = new string(avatarurl)
			});
	}
}
