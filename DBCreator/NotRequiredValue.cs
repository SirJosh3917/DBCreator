using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCreator
{
	public class NotRequiredValue : Exception
	{
		public NotRequiredValue() : base("The type of object you're trying to access is not the type of object the DatabaseObject has.")
		{

		}
	}
}
