using System;

namespace GitHubWebUserFinder.Connectors
{
	public class SearchFunctionalityNotAvailableException : Exception
	{
		public SearchFunctionalityNotAvailableException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}