using System;

namespace DBCreator
{
	public class NotRequiredValueException : Exception
	{
		public NotRequiredValueException() : base("The type of object you're trying to access is not the type of object the DatabaseObject has.")
		{

		}
	}
}
