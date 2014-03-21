using System;
using System.Collections.Generic;
using System.Drawing;
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
			entries = new List<Entry>();
		}

		private static IList<Entry> entries;

		public static IEnumerable<Entry> Entries { get { return entries; } }

		/// <summary>
		/// Adds an entry.
		/// </summary>
		/// <param name="entry"></param>
		public static void Add(string text, Color color)
		{
			entries.Add(new Entry(text, color));
		}

		/// <summary>
		/// Adds an entry with the default color of white.
		/// </summary>
		/// <param name="entry"></param>
		public static void Add(string text)
		{
			entries.Add(new Entry(text, Color.White));
		}

		/// <summary>
		/// Clears the log.
		/// </summary>
		public static void Clear()
		{
			entries.Clear();
		}

		public class Entry
		{
			public Entry(string text, Color color)
			{
				Text = text;
				Color = color;
			}

			public string Text { get; set; }
			public Color Color { get; set; }
		}

		public static void Trim(int num)
		{
			entries = entries.Reverse().Take(num).Reverse().ToList();
		}
	}
}
