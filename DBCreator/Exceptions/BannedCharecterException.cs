using System;

namespace DBCreator
{
	public class BannedCharecterException : Exception
	{
		public BannedCharecterException(string Keyword)
			: base("You cannot use one of the charecters within the string you put ( " + Keyword + " ), that charecter is banned.")
		{

		}
	}
}
