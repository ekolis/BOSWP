using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOSWP
{
	/// <summary>
	/// An object which can occupy a sector.
	/// </summary>
	public abstract class SpaceObject : IColoredGlyphObject
	{
		protected SpaceObject(char glyph, Color color)
		{
			Glyph = glyph;
			Color = color;
		}

		/// <summary>
		/// The symbol used to represent this space object on the map.
		/// </summary>
		public char Glyph { get; set; }

		/// <summary>
		/// The color of this space object.
		/// </summary>
		public Color Color { get; set; }

		/// <summary>
		/// Is this space object invisible to ships lacking tachyon sensors?
		/// </summary>
		public bool IsCloaked { get; set; }

		/// <summary>
		/// What happens when this space object is bumped by a ship?
		/// </summary>
		/// <param name="source"></param>
		/// <returns>Should the bump consume a move?</returns>
		public abstract bool BeBumped(Ship source);

		/// <summary>
		/// The star system containing this space object.
		/// </summary>
		public StarSystem StarSystem
		{
			get
			{
				return Galaxy.Current.StarSystems.SingleOrDefault(s => s != null && s.SpaceObjects.Contains(this));
			}
		}

		/// <summary>
		/// The x-coordinate of this space object.
		/// </summary>
		public int X
		{
			get
			{
				return StarSystem.SpaceObjects.GetX(this);
			}
		}

		/// <summary>
		/// The y-coordinate of this space object.
		/// </summary>
		public int Y
		{
			get
			{
				return StarSystem.SpaceObjects.GetY(this);
			}
		}
	}
}
