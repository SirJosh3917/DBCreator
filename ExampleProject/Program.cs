using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBCreator;

namespace ExampleProject
{
	class Program
	{
		static void Main(string[] args)
		{
			//This should on first run, create a database, say "Table's missing", and then exit.
			//On the second run, it should say all of the values it was storing.

			//Create a new database
			Database d = new Database("IMTHEBEST");

			//Attempt to get a value
			try
			{
				Console.WriteLine(d.Get("example", "derp1"));
				Console.WriteLine(d.Get("example", "derp2"));
				Console.WriteLine(d.Get("example", "derp3"));
				Console.WriteLine(d.Get("example", "derp4"));
				Console.WriteLine(d.Get("example", ""));
			}
			catch (MissingTableException e)
			{
				Console.WriteLine("Table's missing");
				d.CreateTable("example");
			} catch (MissingIdentifierException e)
			{
				Console.WriteLine("Identifier's missing");
			}
			//Set some values
			d.Set("example", "derp1", true);
			d.Set("example", "derp2", 1);
			d.Set("example", "derp3", "false");
			d.Set("example", "derp4", float.MaxValue);
			d.SetArray("example", "", new DatabaseObjectArray(new DatabaseObject[]{
			new DatabaseObject(false),
			new DatabaseObject(4),
			new DatabaseObject('a'),
			new DatabaseObject(byte.MinValue)
			}));

			//Save this to a file
			d.Save();

			//Wait for user input
			Console.ReadLine();
		}
	}
}
