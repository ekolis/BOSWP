using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        /// Adds an entry that optionally does not end the line.
        /// </summary>
        /// <param name="entry"></param>
        public static void Add(string text, Color color, bool endOfLine)
        {
            entries.Add(new Entry(text, color, endOfLine, (entries.Count > 0 && entries.Last().EndOfLine)));
        }
		/// <summary>
		/// Adds an entry.
		/// </summary>
		/// <param name="entry"></param>
		public static void Add(string text, Color color)
		{
            Add(text, color, true);
		}

		/// <summary>
		/// Adds an entry with the default color of white.
		/// </summary>
		/// <param name="entry"></param>
		public static void Add(string text)
		{
            Add(text, Color.White);
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
			public Entry(string text, Color color, bool endOfLine, bool startOfLine)
			{
				Text = text;
				Color = color;
                EndOfLine = endOfLine;
                StartOfLine = startOfLine;
			}

			public string Text { get; set; }
			public Color Color { get; set; }
            public bool EndOfLine { get; set; }
            public bool StartOfLine { get; set; }
		}

		public static void Trim(int num)
		{
			int excess = Math.Max(0,entries.Where(e => e.EndOfLine).Count() - num);
            while (excess > 0
                ||
                (entries.Count > 0 && !entries[0].EndOfLine) // Keep trimming until we get to a newline
                )
            {
                excess--;
                entries.RemoveAt(0);
            }
		}
        
        public static string toRTF()
        {
            RichTextBox temp = new RichTextBox();
            foreach (var msg in Entries)
			{
                temp.AppendLine(msg.Text, msg.Color, msg.EndOfLine);// add to output
                msg.Color = fadeColor(msg.Color);//Color.Gray;// mark gray for read
            }
            return temp.Rtf;
        }
        public static Color fadeColor(Color original)
        {
            int r = original.R;
            int g = original.G;
            int b = original.B;
            int a = original.A;
            return Color.FromArgb((int)((double)a*0.9), r/2+a/2, g/2+a/2, b/2+a/2);
        }
	}
}
