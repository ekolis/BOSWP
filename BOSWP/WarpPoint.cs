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
			// velocity in equals velocity out!
			var dx = X - source.X;
			var dy = Y - source.Y;
			var ts = TargetSystem;
			var tgt = Target;
			var nx = tgt.X + dx;
			var ny = tgt.Y + dy;
			var realBumpee = ts.SpaceObjects[nx, ny];
			if (realBumpee == null)
			{
				// move
				source.Place(ts, nx, ny);
				if (source is EnemyShip)
					((EnemyShip)source).NeedsNewWaypoint = true;
				return true;
			}
			else
			{
				// TODO - notify player if he bumps an occupied sector
				return true;
			}
		}

		/// <summary>
		/// The star system to warp to.
		/// </summary>
		public StarSystem TargetSystem
		{
			get
			{
				var sys = StarSystem;
				var x = Galaxy.Current.StarSystems.GetX(sys);
				var y = Galaxy.Current.StarSystems.GetY(sys);
				var tx = x + Direction.DeltaX;
				var ty = y + Direction.DeltaY;
				if (tx < -Galaxy.Current.StarSystems.Radius || tx > Galaxy.Current.StarSystems.Radius || ty < -Galaxy.Current.StarSystems.Radius || ty > Galaxy.Current.StarSystems.Radius)
					return null;
				return Galaxy.Current.StarSystems[tx, ty];
			}
		}

		/// <summary>
		/// The warp point to warp to.
		/// </summary>
		public WarpPoint Target
		{
			get
			{
				var sys = TargetSystem;
				return sys.SpaceObjects.OfType<WarpPoint>().Single(wp => wp.Direction == Direction.Opposite);
			}
		}

		public override void Scan()
		{
			MessageBox.Show("It's a warp point. You can use it to travel to the next system.");
		}
	}
}
