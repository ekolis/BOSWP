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
			NeedsNewWaypoint = true;
			var template = new Component();
			template.Name = "generic component";
			template.Mass = 30;
			template.MaxHitpoints = 30;
			template.Hitpoints = template.MaxHitpoints;
			for (var i = 0; i < 4; i++)
				Components.Add(template.Clone());
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
					Log.Add("The Jraenar ship is firing on us!");
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

		public override void LogComponentDamage(Component component, int damage)
		{
			// TODO - if player has long range scanners, he should see component damage on enemies
			Log.Add(damage + " damage to the Jraenar ship's hull!");
		}
	}
}
