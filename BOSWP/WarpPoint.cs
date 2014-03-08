using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOSWP
{
	/// <summary>
	/// Allows ships to travel from one star system to another.
	/// </summary>
	public class WarpPoint : SpaceObject
	{
		public WarpPoint(Direction dir)
			: base('+', Color.CornflowerBlue)
		{
			Direction = dir;
		}

		/// <summary>
		/// The direction of the warp.
		/// </summary>
		public Direction Direction { get; private set; }

		public override bool BeBumped(Ship source)
		{
			// TODO - warping
			return true;
		}
	}
}
