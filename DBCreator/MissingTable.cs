using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCreator
{
	public class MissingTable : Exception
	{
		public MissingTable() : base("The table you're attempting to access is missing.")
		{

		}
	}
}