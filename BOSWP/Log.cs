using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOSWP
{
	/// <summary>
	/// Game log to be displayed to the player.
	/// </summary>
	public static class Log
	{
		static Log()
		{
			entries = new List<string>();
		}

		private static IList<string> entries;

		public static IEnumerable<string> Entries { get { return entries; } }

		/// <summary>
		/// Adds an entry.
		/// </summary>
		/// <param name="entry"></param>
		public static void Add(string entry)
		{
			entries.Add(entry);
		}

		/// <summary>
		/// Clears the log.
		/// </summary>
		public static void Clear()
		{
			entries.Clear();
		}
	}
}
