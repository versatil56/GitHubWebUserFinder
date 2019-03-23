using Newtonsoft.Json.Linq;
using System.Linq;
using FsCheck;
using NUnit.Framework;

namespace GitHubWebUserFinder.Tests.Models
{
	public static class Generators
	{
		private static string _allowedCharacters = "*()&%$£@<>";
		private static string _alphabet = "abcdefghijklmnopqrstuvwxyz";
		private static string _numbers = "0123456789";

		public static Gen<int> RandomNumber(int min, int max)
		{
			return Gen.Choose(min, max);
		}

		public static Arbitrary<char[]> Letters()
		{
			return Gen.ArrayOf(RandomNumber(5, 100).Sample(20, 1).First(), Gen.Elements($"{_alphabet}{_alphabet.ToUpper()}".ToCharArray())).ToArbitrary();
		}

		public static Arbitrary<char[]> Alphanumeric()
		{
			return Gen.ArrayOf(RandomNumber(5, 100).Sample(20, 1).First(), Gen.Elements($"{_alphabet}{_alphabet.ToUpper()}{_numbers}".ToCharArray())).ToArbitrary();
		}

		public static Arbitrary<char[]> NotEmptyStringWithAllowedCharacters()
		{
			return Gen.ArrayOf(RandomNumber(5, 100).Sample(20, 1).First(), Gen.Elements($"{_alphabet}{_alphabet.ToUpper()}{_numbers}{_allowedCharacters}".ToCharArray())).ToArbitrary();
		}

		public static Gen<JObject> GitHubUser = from name in Letters().Generator
			from userLocation in Alphanumeric().Generator
			from alias in Alphanumeric().Generator
			from avatarurl in NotEmptyStringWithAllowedCharacters().Generator
			select JObject.FromObject(new
			{
				name = new string(name),
				location = new string(userLocation),
				login = new string(alias),
				avatar_url = new string(avatarurl)
			});

		public static Gen<JObject> GitHubRepository = from name in Alphanumeric().Generator
			from repoUrl in NotEmptyStringWithAllowedCharacters().Generator
			from stars in RandomNumber(0,100)
			select JObject.FromObject(new
			{								   
				name = new string(name),
				html_url = new string(repoUrl),
				stargazers_count = stars
			});

		public static Gen<JArray> GitHubRepositories = from repos in Gen.ArrayOf(GitHubRepository)
			select new JArray(repos);
	}
}
