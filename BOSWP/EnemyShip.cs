using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOSWP
{
	/// <summary>
	/// An enemy ship.
	/// </summary>
	public class EnemyShip : Ship
	{
		public EnemyShip()
			: base('J', Color.Firebrick)
		{
			Hitpoints = 100;
			NeedsNewWaypoint = true;
		}

		public override bool Move()
		{
			if (PlayerShip.Instance.StarSystem == StarSystem)
			{
				// player ship sighted! CHASE HIM!!!
				NeedsNewWaypoint = false;
				WaypointX = PlayerShip.Instance.X;
				WaypointY = PlayerShip.Instance.Y;
			}
			else if (NeedsNewWaypoint)
			{
				// get new waypoint - random warp point to "patrol"
				NeedsNewWaypoint = false;
				var wp = StarSystem.FindSpaceObjects<WarpPoint>().PickRandom();
				WaypointX = wp.X;
				WaypointY = wp.Y;
			}
			else
			{
				// continue toward old waypoint, nothing to do here
			}

			// pursue warp point
			var dir = Utilities.Pathfind(StarSystem, X, Y, WaypointX, WaypointY);
			var x = X + dir.DeltaX;
			var y = Y + dir.DeltaY;
			if (StarSystem.SpaceObjects[x, y] is WarpPoint)
				StarSystem.SpaceObjects[x, y].BeBumped(this);
			else
				Place(StarSystem, x, y);

			// no need to wait for player input
			return true;
		}

		public override void Attack()
		{
			// targeting priority: player ship
			if (PlayerShip.Instance.StarSystem == StarSystem)
			{
				if (Utilities.Distance(X, Y, PlayerShip.Instance.X, PlayerShip.Instance.Y) <= 3)
				{
					Log.Add("The Jraenar ship is firing on us! 20 damage to the hull!");
					PlayerShip.Instance.TakeDamage(20);
				}
			}
		}

		/// <summary>
		/// X-coordinate that the ship is navigating to.
		/// </summary>
		public int WaypointX { get; set; }

		/// <summary>
		/// Y-coordinate that the ship is navigating to.
		/// </summary>
		public int WaypointY { get; set; }

		/// <summary>
		/// Does this ship need a new waypoint?
		/// </summary>
		public bool NeedsNewWaypoint { get; set; }
	}
}
