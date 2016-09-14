using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCreator
{
	/// <summary>
	/// A database.
	/// </summary>
    public class Database
    {
		private string DatabaseN = "";
		Dictionary<string, Dictionary<string, DatabaseObject>> DatabaseObjects = new Dictionary<string, Dictionary<string, DatabaseObject>>();

		/// <summary>
		/// Create a database
		/// </summary>
		/// <param name="DatabaseName">The name of the database to automatically load if not loaded already</param>
		public Database(string DatabaseName)
		{
			DatabaseN = DatabaseName;

			if(File.Exists(Environment.ExpandEnvironmentVariables("%locallow%\\DBC\\" + DatabaseName + ".dbc")))
			{
				string RecentlyMentionedTable = "";

				//Load the database
				foreach(string i in File.ReadAllLines(Environment.ExpandEnvironmentVariables("%locallow%\\DBC\\" + DatabaseName + ".dbc")))
				{
					if (i.StartsWith(":::")) //We've got to load an array.
					{

					} else if (i.StartsWith("::")){
						DatabaseObjects.Add(i.Substring(2), new Dictionary<string,DatabaseObject>());
						RecentlyMentionedTable = i.Substring(2);
					}
					else if(i.StartsWith(":") && RecentlyMentionedTable != "")
					{
						//get some values
						string[] iSplitColon = i.Split(':');

						if (iSplitColon.Length > 3)
						{
							string Identifier = iSplitColon[1];
							string TypeOf = iSplitColon[2];
							string Value = i.Substring(3 + Identifier.Length + TypeOf.Length);

							if (!DatabaseObjects[RecentlyMentionedTable].ContainsKey(Identifier))
								DatabaseObjects[RecentlyMentionedTable].Add(Identifier,
									new DatabaseObject(Convert.ChangeType((object)Value, Type.GetType(TypeOf)))
									);
							else
								DatabaseObjects[RecentlyMentionedTable][Identifier] =
									new DatabaseObject(Convert.ChangeType((object)Value, Type.GetType(TypeOf)));
						}
					}
				}
			}
		}

		/// <summary>
		/// Save the database on the local machine
		/// </summary>
		public void Save()
		{
			List<string> FileLines = new List<string>();
			if(DatabaseObjects.Keys.Count > 0)
			{
				foreach (string i in DatabaseObjects.Keys)
				{
					FileLines.Add("::" + i);

					if (DatabaseObjects[i].Keys.Count > 0)
					{
						foreach (string n in DatabaseObjects[i].Keys)
						{
							FileLines.Add(":" + n + ":" + DatabaseObjects[i][n].ToString());
						}
					}
				}

				File.WriteAllLines(Environment.ExpandEnvironmentVariables("%locallow%\\DBC\\" + DatabaseN + ".dbc"), FileLines.ToArray());
			}
		}

		/// <summary>
		/// Create a table
		/// </summary>
		/// <param name="table">The name of the table</param>
		public void CreateTable(string table)
		{
			if (table.Length == 0)
				throw new ArgumentException("The table's length cannot be zero.");
			if (!DatabaseObjects.ContainsKey(table))
				DatabaseObjects.Add(table, new Dictionary<string, DatabaseObject>());
		}

		/// <summary>
		/// Get an object from a DatabaseObject within the Database
		/// </summary>
		/// <param name="table">The table it's located in</param>
		/// <param name="identifier">The identifier of the object</param>
		/// <returns>The object needed</returns>
		public object Get(string table, string identifier)
		{
			if (DatabaseObjects.ContainsKey(table))
				if (DatabaseObjects[table].ContainsKey(identifier))
					return DatabaseObjects[table][identifier].Get();
				else
					throw new MissingIdentifier();
			else
				throw new MissingTable();
		}

		/// <summary>
		/// Get an object from a DatabaseObject within the Database
		/// </summary>
		/// <param name="table">The table it's located in</param>
		/// <param name="identifier">The identifier of the object</param>
		/// <param name="objectType">The type of the object</param>
		/// <returns>The object needed</returns>
		public object Get(string table, string identifier, Type objectType)
		{
			if (DatabaseObjects.ContainsKey(table))
				if (DatabaseObjects[table].ContainsKey(identifier))
					return DatabaseObjects[table][identifier].Get(objectType);
				else
					throw new MissingIdentifier();
			else
				throw new MissingTable();
		}

		/// <summary>
		/// Get an object from a DatabaseObject within the Database
		/// </summary>
		/// <param name="table">The table it's located in</param>
		/// <param name="identifier">The identifier of the object</param>
		/// <param name="objectType">The string type of the object</param>
		/// <returns>The object needed</returns>
		public object Get(string table, string identifier, string objectType)
		{
			if (DatabaseObjects.ContainsKey(table))
				if (DatabaseObjects[table].ContainsKey(identifier))
					return DatabaseObjects[table][identifier].Get(objectType);
				else
					throw new MissingIdentifier();
			else
				throw new MissingTable();
		}

		/// <summary>
		/// Sets a value within a certain table, and auto-creates the identifier if it doesn't exist already.
		/// </summary>
		/// <param name="table">The table</param>
		/// <param name="identifier">The identifier</param>
		/// <param name="value">The value</param>
		public void Set(string table, string identifier, object value)
		{
			if (DatabaseObjects.ContainsKey(table))
			{
				if (DatabaseObjects[table].ContainsKey(identifier))
					DatabaseObjects[table][identifier].Set(value);
				else
					DatabaseObjects[table].Add(identifier, new DatabaseObject(value));
			}
		}
	}
}
