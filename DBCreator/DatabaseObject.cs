using System;
using System.Collections.Generic;
using System.Text;

namespace DBCreator
{
	/// <summary>
	/// A database object
	/// </summary>
	public class DatabaseObject
	{
		private string Stringify(byte b) { return Convert.ToChar(Convert.ToByte(b)).ToString(); }

		/// <summary>The type of the database object</summary>
		private Type thisType;
		/// <summary>The value of the database object</summary>
		private object value;

		/// <summary>
		/// Create a database object
		/// </summary>
		/// <param name="Value">The value of the DatabaseObject</param>
		public DatabaseObject(object Value)
		{
			thisType = Value.GetType();
			value = Value;
		}

		/// <summary>
		/// Gets the type of the database object within the database object
		/// </summary>
		/// <returns>The type of the database object within the database object</returns>
		public Type GetObjectType() { return thisType; }

		/// <summary>
		/// Gets the value of the database object within the database object
		/// </summary>
		/// <returns>The value of the database object</returns>
		public object GetValue() { return value; }

		/// <summary>
		/// Gets the object the databaseobject is holding.
		/// </summary>
		/// <returns>The object the databaseobject is holding</returns>
		public object Get()
		{
			return value;
		}
		
		/// <summary>
		/// Gets the object the databaseobject is holding, throwing an ArgumentException if the object is not the type needed.
		/// </summary>
		/// <param name="typeOfObject">The type of object the database object should</param>
		/// <returns>The object the databaseobject is holding.</returns>
		public object Get(Type typeOfObject)
		{
			if (thisType != typeOfObject)
				throw new ArgumentException("The database object is not equal to the type specified.");

			return value;
		}

		/// <summary>
		/// Gets the object the databaseobject is holding, throwing an ArgumentException if the object is not the type needed.
		/// </summary>
		/// <param name="typeOfObject">The type of object the database object should</param>
		/// <returns>The object the databaseobject is holding.</returns>
		public object Get(string typeOfObject)
		{
			if (thisType.ToString() != typeOfObject.ToString())
				throw new ArgumentException("The database object is not equal to the type specified.");

			return value;
		}

		/// <summary>
		/// Sets the value of the database object.
		/// </summary>
		/// <param name="Value">The value</param>
		public void Set(object Value)
		{
			thisType = Value.GetType();
			value = Value;
		}

		/// <summary>
		/// Returns in the format "System.String:Hello World! This is a test string!"
		/// </summary>
		/// <returns>What the value would be like stored in the database</returns>
		public override string ToString()
		{
			return thisType.ToString() + Stringify(0x00) + value.ToString();
		}
	}
}
