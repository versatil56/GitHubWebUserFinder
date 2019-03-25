using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using GitHubWebUserFinder.Models;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace GitHubWebUserFinder.Tests.Models
{
	[TestFixture]
	class UserSearchTests
	{
		[Test]
		public void UserSearch_WithNoCriteria_IsInvalid()
		{
			UserSearch testSearch = new UserSearch();

			IList<ValidationResult> results = ValidateModel(testSearch);

			Assert.AreEqual(1, results.Count);
			Assert.AreEqual("The Criteria field is required.", results.First().ErrorMessage);
		}

		[Test]
		public void UserSearch_WithUserNameWithCharactersNotAcceptedByGitHub_IsInvalid()
		{
			UserSearch testSearch = new UserSearch
			{
				Criteria = "john.doe"
			};

			IList<ValidationResult> results = ValidateModel(testSearch);

			Assert.AreEqual(1, results.Count);
			Assert.AreEqual("Search may only contain alphanumeric characters or single hyphens, and cannot begin or end with a hyphen", results.First().ErrorMessage);
		}

		[Test]
		public void UserSearch_Valid_WillNotReturnErrors()
		{
			UserSearch testSearch = new UserSearch
			{
				Criteria = "versatil56"
			};

			IList<ValidationResult> results = ValidateModel(testSearch);

			Assert.AreEqual(0,results.Count);
		}

		private static IList<ValidationResult> ValidateModel(object modelToValidate)
		{
			List<ValidationResult> validationResult = new List<ValidationResult>();
			ValidationContext validationContext = new ValidationContext(modelToValidate, null, null);
			Validator.TryValidateObject(modelToValidate, validationContext, validationResult, true);

			return validationResult;
		}
	}
}
