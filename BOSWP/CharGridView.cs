using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BOSWP
{
	public partial class CharGridView : Control
	{
		public CharGridView()
		{
			InitializeComponent();
			DoubleBuffered = true;
		}

		/// <summary>
		/// The grid of characters.
		/// </summary>
		public IReadableGrid<IColoredGlyphObject> Grid { get; set; }

		/// <summary>
		/// Rectangles to draw on the grid.
		/// </summary>
		public ICollection<Tuple<Rectangle, Color>> Rectangles { get; set; }

		/// <summary>
		/// Glyph for representing null items.
		/// </summary>
		public char NullGlyph { get; set; }

		/// <summary>
		/// Color for representing null items.
		/// </summary>
		public Color NullColor { get; set; }

		/// <summary>
		/// The size to draw each glyph.
		/// </summary>
		public int GlyphSize
		{
			get
			{
				if (Grid == null)
					return 0;
				return Math.Min(Width, Height) / Grid.Diameter;
			}
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);

			var glyphSize = GlyphSize;
			if (glyphSize < 1)
				return; // can't draw it

			var g = pe.Graphics;
			var font = new Font(Font.FontFamily, glyphSize * Math.Min(g.DpiX, g.DpiY) / 96);
			var sf = new StringFormat();
			sf.Alignment = StringAlignment.Center;
			sf.LineAlignment = StringAlignment.Center;

			if (Rectangles != null)
			{
				foreach (var rectangle in Rectangles)
				{
					var rect = rectangle.Item1;
					var rect2 = new Rectangle(
						(rect.Left + Grid.Radius) * glyphSize,
						(rect.Top + Grid.Radius) * glyphSize,
						rect.Width * glyphSize,
						rect.Height * glyphSize);

					g.FillRectangle(new SolidBrush(Color.FromArgb(16, rectangle.Item2)), rect2);
					g.DrawRectangle(new Pen(rectangle.Item2), rect2);
				}
			}

			for (var x = -Grid.Radius; x <= Grid.Radius; x++)
			{
				for (var y = -Grid.Radius; y <= Grid.Radius; y++)
				{
					var l = (x + Grid.Radius) * glyphSize;
					var r = (x + Grid.Radius + 1) * glyphSize;
					var t = (y + Grid.Radius) * glyphSize;
					var b = (y + Grid.Radius + 1) * glyphSize;
					var cx = l + glyphSize / 2;
					var cy = t + glyphSize / 2;

					if (Grid != null)
					{
						var item = Grid[x, y];
						if (item == null)
							g.DrawString(NullGlyph.ToString(), font, new SolidBrush(NullColor), cx, cy, sf);
						else
							g.DrawString(item.Glyph.ToString(), font, new SolidBrush(item.Color), cx, cy, sf);
					}
				}
			}
		}

		/// <summary>
		/// Don't let winforms eat the arrow keys
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="keyData"></param>
		/// <returns></returns>
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (Direction.All.Any(d => d.Keys.Contains(keyData)))
			{
				OnKeyDown(new KeyEventArgs(keyData));
				return true;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}
	}
}
