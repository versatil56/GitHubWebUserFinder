using Newtonsoft.Json.Linq;
using System.Linq;
using FsCheck;
using NUnit.Framework;
using GitHubWebUserFinder.Models;
using System.Collections.Generic;

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

		public static Gen<JObject> JsonGitHubUser = from name in Letters().Generator
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

		public static Gen<GitHubUser> GitHubUser = from name in Letters().Generator
			from userLocation in Alphanumeric().Generator
			from alias in Alphanumeric().Generator
			from avatarurl in NotEmptyStringWithAllowedCharacters().Generator
			select new GitHubUser
			{
				FullName = new string(name),
				CurrentLocation = new string(userLocation),
				Alias = new string(alias),
				AvatarUrl = new string(avatarurl)
			};

		public static Gen<JObject> JsonGitHubRepository = from name in Alphanumeric().Generator
			from repoUrl in NotEmptyStringWithAllowedCharacters().Generator
			from stars in RandomNumber(0,100)
			select JObject.FromObject(new
			{								   
				name = new string(name),
				html_url = new string(repoUrl),
				stargazers_count = stars
			});

		public static Gen<GitHubRepository> GitHubRepository = from name in Alphanumeric().Generator
			from repoUrl in NotEmptyStringWithAllowedCharacters().Generator
			from stars in RandomNumber(0, 100)
			select new GitHubRepository
			{
				Name = new string(name),
				Url = new string(repoUrl),
				NumberOfStarGazers = stars
			};

		public static Gen<JArray> JsonGitHubRepositories = from repos in Gen.ArrayOf(JsonGitHubRepository)
			select new JArray(repos);

		public static Gen<List<GitHubRepository>> GitHubRepositories = from repos in Gen.ListOf(GitHubRepository)
			select repos.ToList();
	}
}
