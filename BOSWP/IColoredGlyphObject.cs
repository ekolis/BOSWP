using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOSWP
{
	/// <summary>
	/// Something which can be represented by a colored glyph.
	/// </summary>
	public interface IColoredGlyphObject
	{
		char Glyph { get; }

		Color Color { get; }
	}
}
