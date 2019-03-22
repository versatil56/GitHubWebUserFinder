using Newtonsoft.Json.Linq;
using System.Linq;
using FsCheck;

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

		public static Arbitrary<char[]> LettersGenerator()
		{
			return Gen.ArrayOf(RandomNumber(5, 100).Sample(20, 1).First(), Gen.Elements($"{_alphabet}{_alphabet.ToUpper()}".ToCharArray())).ToArbitrary();
		}

		public static Arbitrary<char[]> AlphanumericGenerator()
		{
			return Gen.ArrayOf(RandomNumber(5, 100).Sample(20, 1).First(), Gen.Elements($"{_alphabet}{_alphabet.ToUpper()}{_numbers}".ToCharArray())).ToArbitrary();
		}

		public static Arbitrary<char[]> NotEmptyStringWithAllowedCharactersGenerator()
		{
			return Gen.ArrayOf(RandomNumber(5, 100).Sample(20, 1).First(), Gen.Elements($"{_alphabet}{_alphabet.ToUpper()}{_numbers}{_allowedCharacters}".ToCharArray())).ToArbitrary();
		}

		public static Gen<JObject> GitHubUserGenerator = from name in LettersGenerator().Generator
			from userLocation in AlphanumericGenerator().Generator
			from alias in AlphanumericGenerator().Generator
			from avatarurl in NotEmptyStringWithAllowedCharactersGenerator().Generator
			select JObject.FromObject(new
			{
				name = new string(name),
				location = new string(userLocation),
				login = new string(alias),
				avatar_url = new string(avatarurl)
			});
	}
}
