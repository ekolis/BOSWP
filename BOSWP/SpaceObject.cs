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

		/// <summary>
		/// Places this object at a new location (if the location is unoccupied).
		/// </summary>
		/// <param name="s"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns>true if successful, false if blocked</returns>
		public bool Place(StarSystem s, int x, int y)
		{
			// check if occupied
			if (s.SpaceObjects[x, y] != null)
				return false;
			
			// move
			StarSystem.SpaceObjects[X, Y] = null;
			s.SpaceObjects[x, y] = this;

			return true;
		}

		/// <summary>
		/// Finds other space objects that are in range of this space object.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="range"></param>
		/// <returns></returns>
		public IEnumerable<T> FindSpaceObjectsInRange<T>(int range)
		{
			var sys = StarSystem;
			var x = X;
			var y = Y;
			for (var dx = -range; dx <= range; dx++)
			{
				for (var dy = -range; dy <= range; dy++)
				{
					if (sys.SpaceObjects.AreCoordsInBounds(x + dx, y + dy))
					{
						var sobj = sys.SpaceObjects[x + dx, y + dy];
						if (sobj is T && sobj != this)
							yield return (T)(object)sobj;
					}
				}
			}
		}

		/// <summary>
		/// Deletes the space object.
		/// </summary>
		public void Delete()
		{
			if (StarSystem != null)
				StarSystem.SpaceObjects[X, Y] = null;
		}
	}
}
