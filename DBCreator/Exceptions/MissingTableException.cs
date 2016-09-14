using System;

namespace DBCreator
{
	public class MissingTableException : Exception
	{
		public MissingTableException() : base("The table you're attempting to access is missing.")
		{

		}
	}
}