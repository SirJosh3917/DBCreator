using System;
using System.Collections.Generic;
using System.Text;

namespace DBCreator
{
	public class DatabaseObjectArray
	{
		private string Stringify(byte b) { return Convert.ToChar(Convert.ToByte(b)).ToString(); }
		private List<DatabaseObject> PrivArray = new List<DatabaseObject>();

		/// <summary>
		/// Construct a new DatabaseObjectArray based off of an existing DatabaseObject[]
		/// </summary>
		/// <param name="array">The DatabaseObject[]</param>
		public DatabaseObjectArray(DatabaseObject[] array)
		{
			if (array == null)
				throw new ArgumentNullException("The array given was null");

			if (array.Length < 1)
				throw new ArgumentException("The array given has no elements in it.");

			PrivArray.Clear();

			for (int i = 0; i < array.Length; i++)
				PrivArray.Add(array[i]);
		}

		public DatabaseObjectArray(string array)
		{

		}

		public static explicit operator DatabaseObjectArray(string input)
		{
			return new DatabaseObjectArray(input);
		}

		/// <summary>
		/// The array the DatabaseObjectArray contains
		/// </summary>
		public DatabaseObject[] Array
		{
			get
			{
				return PrivArray.ToArray();
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("The array given was null");

				if (value.Length < 1)
					throw new ArgumentException("The array given has no elements in it.");

				PrivArray.Clear();

				for (int i = 0; i < value.Length; i++)
					PrivArray.Add(value[i]);
			}
		}

		/// <summary>
		/// Get the string version of how the array is saved in the DBC file.
		/// </summary>
		/// <returns>A string version of the array</returns>
		public override string ToString() { return toStr(); }

		private string toStr()
		{
			string Format = "";
			int counter = 0;

			if (PrivArray.Count < 1)
				return Format;

			foreach(DatabaseObject i in PrivArray)
			{
				Format += i.ToString();

				counter++;

				if (!(PrivArray.Count > counter + 1)) return Format;
				else Format += Stringify(0x00);
			}

			return Format;
		}
	}
}
