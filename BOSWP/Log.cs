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
			newEntries = new List<string>();
			entries = new List<string>();
		}

		private static IList<string> newEntries;

		private static IList<string> entries;

		private static readonly int MaxLength = 100;

		public static IEnumerable<string> Entries { get { return entries; } }

		/// <summary>
		/// Adds an entry as new.
		/// </summary>
		/// <param name="entry"></param>
		public static void Add(string entry)
		{
			newEntries.Add(entry);
		}

		/// <summary>
		/// Pushes all new entries to the main entries list and clears the new entries list..
		/// Also deletes entries that are too old (up to MaxLength entries can be kept).
		/// </summary>
		public static void PushNewEntries()
		{
			foreach (var entry in newEntries)
				entries.Add(entry);
			newEntries.Clear();
			while (entries.Count > MaxLength)
				entries.RemoveAt(0);
		}
	}
}
