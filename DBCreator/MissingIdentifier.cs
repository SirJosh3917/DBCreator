using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCreator
{
	public class MissingIdentifier : Exception
	{
		public MissingIdentifier() : base("The identifier in the table you're attempting to access is not there.")
		{

		}
	}
}
