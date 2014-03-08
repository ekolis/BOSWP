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
	public abstract class SpaceObject
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
		/// What happens when it's this space object's turn to move?
		/// </summary>
		public abstract void Move();

		/// <summary>
		/// What happens when it's this space object's turn to attack?
		/// </summary>
		public abstract void Attack();

		/// <summary>
		/// What happens when this space object bumps another space object?
		/// </summary>
		/// <param name="target"></param>
		public abstract void Bump(SpaceObject target);

		/// <summary>
		/// What happens when this space object is bumped by another space object?
		/// </summary>
		/// <param name="source"></param>
		public abstract void BeBumped(SpaceObject source);
	}
}
