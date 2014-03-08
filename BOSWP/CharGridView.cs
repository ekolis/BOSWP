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

		public IReadableGrid<IColoredGlyphObject> Grid { get; set; }

		/// <summary>
		/// Glyph for representing null items.
		/// </summary>
		public char NullGlyph { get; set; }

		/// <summary>
		/// Color for representing null items.
		/// </summary>
		public Color NullColor { get; set; }

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);

			if (Grid == null)
				return; // nothing to draw

			var glyphSize = Math.Min(Width, Height) / Grid.Diameter;

			if (glyphSize < 1)
				return; // can't draw it

			var g = pe.Graphics;
			var font = new Font(Font.FontFamily, glyphSize * Math.Min(g.DpiX, g.DpiY) / 96);
			var sf = new StringFormat();
			sf.Alignment = StringAlignment.Center;
			sf.LineAlignment = StringAlignment.Center;

			for (var x = -Grid.Radius; x <= Grid.Radius; x++)
			{
				for (var y = -Grid.Radius; y <= Grid.Radius; y++)
				{
					var item = Grid[x, y];
					if (item == null)
						g.DrawString(NullGlyph.ToString(), font, new SolidBrush(NullColor), (x + Grid.Radius) * glyphSize + glyphSize / 2, (y + Grid.Radius) * glyphSize + glyphSize / 2, sf);
					else
						g.DrawString(item.Glyph.ToString(), font, new SolidBrush(item.Color), (x + Grid.Radius) * glyphSize + glyphSize / 2, (y + Grid.Radius) * glyphSize + glyphSize / 2, sf);
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
