using System;

namespace GitHubWebUserFinder.Connectors
{
	public class GitHubUserNotFoundException : Exception
	{
		public GitHubUserNotFoundException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}