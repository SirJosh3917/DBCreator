using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DBCreator
{
	/// <summary>
	/// A database.
	/// </summary>
    public class Database
    {
		private string Stringify(byte b) { return Convert.ToChar(Convert.ToByte(b)).ToString(); }

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

				string[] Db = File.ReadAllLines(Environment.ExpandEnvironmentVariables("%locallow%\\DBC\\" + DatabaseName + ".dbc"));

				//Load the database
				foreach(string i in Db)
				{
					if (i.StartsWith(Stringify(0x03))) //We've got to load an array.
					{

					}
					else if (i.StartsWith(Stringify(0x02)))
					{
						DatabaseObjects.Add(i.Substring(1), new Dictionary<string,DatabaseObject>());
						RecentlyMentionedTable = i.Substring(1);
					}
					else if (i.StartsWith(Stringify(0x01)) && RecentlyMentionedTable != "")
					{
						//get some values
						string[] iSplitColon = i.Split(Convert.ToChar(0x00));

						if (iSplitColon.Length > 2)
						{
							string Identifier = iSplitColon[0].Substring(1);
							string TypeOf = iSplitColon[1];
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
					FileLines.Add(Stringify(0x02) + i);

					if (DatabaseObjects[i].Keys.Count > 0)
					{
						foreach (string n in DatabaseObjects[i].Keys)
						{
							Console.WriteLine(DatabaseObjects[i][n].GetObjectType().ToString());
							if (DatabaseObjects[i][n].GetObjectType().ToString() == "DBOA")
							{

							}
							else
							{
								FileLines.Add(Stringify(0x01) + n + Stringify(0x00) + DatabaseObjects[i][n].ToString());
							}
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
			if (table.Contains(Stringify(0x00)))
				throw new BannedCharecterException("table, " + table);

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
					throw new MissingIdentifierException();
			else
				throw new MissingTableException();
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
			if (table.Contains(Stringify(0x00)))
				throw new BannedCharecterException("table, " + table);

			if (identifier.Contains(Stringify(0x00)))
				throw new BannedCharecterException("identifier, " + identifier);

			if (objectType.ToString().Contains(Stringify(0x00)))
				throw new BannedCharecterException("objectType, " + objectType.ToString());

			if (DatabaseObjects.ContainsKey(table))
				if (DatabaseObjects[table].ContainsKey(identifier))
					return DatabaseObjects[table][identifier].Get(objectType);
				else
					throw new MissingIdentifierException();
			else
				throw new MissingTableException();
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
			if (table.Contains(Stringify(0x00)))
				throw new BannedCharecterException("table, " + table);

			if (identifier.Contains(Stringify(0x00)))
				throw new BannedCharecterException("identifier, " + identifier);

			if (objectType.ToString().Contains(Stringify(0x00)))
				throw new BannedCharecterException("objectType, " + objectType);

			if (DatabaseObjects.ContainsKey(table))
				if (DatabaseObjects[table].ContainsKey(identifier))
					return DatabaseObjects[table][identifier].Get(objectType);
				else
					throw new MissingIdentifierException();
			else
				throw new MissingTableException();
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
			else
			{
				throw new MissingTableException();
			}
		}
		/*
		/// <summary>
		/// Sets a value within a certain table, and auto-creates the identifier if it doesn't exist already.
		/// </summary>
		/// <param name="table">The table</param>
		/// <param name="identifier">The identifier</param>
		/// <param name="value">The value</param>
		/// <param name="valueAssembly">The assembly the object was made in. This allows for custom object re-creation.</param>
		public void Set(string table, string identifier, object value, string valueAssembly)
		{
			if (valueAssembly.Contains(Stringify(0x00)))
				throw new BannedCharecterException("valueAssembly, " + valueAssembly);

			if (DatabaseObjects.ContainsKey(table))
			{
				if (DatabaseObjects[table].ContainsKey(identifier))
					DatabaseObjects[table][identifier].Set(value, valueAssembly);
				else
					DatabaseObjects[table].Add(identifier, new DatabaseObject(value));
			}
			else
			{
				throw new MissingTableException();
			}
		}*/

		/// <summary>
		/// Sets a value to an array, and auto-creates the identifier if it doesn't exist already.
		/// </summary>
		/// <param name="table">The table</param>
		/// <param name="identifier">The identifier</param>
		/// <param name="array">The array</param>
		public void SetArray(string table, string identifier, DatabaseObjectArray array)
		{
			if (DatabaseObjects.ContainsKey(table))
			{
				if (DatabaseObjects[table].ContainsKey(identifier))
					DatabaseObjects[table][identifier].SetArray(array);
				else
					DatabaseObjects[table].Add(identifier, new DatabaseObject(array));
			}
			else
			{
				throw new MissingTableException();
			}
		}
	}
}
