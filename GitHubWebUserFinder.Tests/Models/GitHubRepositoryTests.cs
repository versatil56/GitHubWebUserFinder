using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FsCheck;
using GitHubWebUserFinder.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace GitHubWebUserFinder.Tests.Models
{
	[TestFixture]
	class GitHubRepositoryTests
	{
		[Test]
		public void AGitJsonPayload_ContainingARepository_WillMapCorrectlyRepositoryName()
		{
			Prop.ForAll(Generators.GitHubRepository.ToArbitrary(), json =>
			{
				dynamic user = json;
				var expectedRepoName = user.name.ToString();
				var result = JsonConvert.DeserializeObject<GitHubRepository>(json.ToString());

				Assert.AreEqual(expectedRepoName, result.Name);
			}).QuickCheck();
		}

		[Test]
		public void AGitJsonPayload_ContainingARepository_WillMapCorrectlyRepositoryUrl()
		{
			Prop.ForAll(Generators.GitHubRepository.ToArbitrary(), json =>
			{
				dynamic user = json;
				var expectedRepoAddress = user.html_url.ToString();
				var result = JsonConvert.DeserializeObject<GitHubRepository>(json.ToString());

				Assert.AreEqual(expectedRepoAddress, result.Url);
			}).QuickCheck();
		}

		[Test]
		public void AGitJsonPayload_ContainingARepository_WillMapCorrectlyRepositoryStarGazerCount()
		{
			Prop.ForAll(Generators.GitHubRepository.ToArbitrary(), json =>
			{
				dynamic user = json;
				var expectedNumberOfStarGazers = Convert.ToInt16(user.stargazers_count);
				var result = JsonConvert.DeserializeObject<GitHubRepository>(json.ToString());
				Assert.AreEqual(expectedNumberOfStarGazers, result.NumberOfStarGazers);
			}).QuickCheck();
		}

		[Test]
		public void AGitJsonPayload_ContainingAnArrayOfRepositories_WillMapCorrectlyToAListOfRepositories()
		{
			Prop.ForAll(Generators.GitHubRepositories.ToArbitrary(), json =>
			{
				var result = JsonConvert.DeserializeObject<List<GitHubRepository>>(json.ToString());
				Assert.AreEqual(result.Count, json.Count);
			}).QuickCheck();
		}
	}
}
