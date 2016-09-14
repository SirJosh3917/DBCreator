using System;

namespace DBCreator
{
	public class MissingIdentifierException : Exception
	{
		public MissingIdentifierException() : base("The identifier in the table you're attempting to access is not there.")
		{

		}
	}
}
