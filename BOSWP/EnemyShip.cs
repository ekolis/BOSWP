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
			var generic = new Component();
			generic.Name = "generic component";
			generic.Mass = 30;
			generic.MaxHitpoints = 30;
			generic.Hitpoints = generic.MaxHitpoints;
			for (var i = 0; i < 8; i++)
				Components.Add(generic.Clone());
			var duc = new Component();
			duc.Name = "depleted uranium cannon";
			duc.Mass = 30;
			duc.MaxHitpoints = 30;
			duc.Hitpoints = duc.MaxHitpoints;
			duc.WeaponInfo = new WeaponInfo();
			duc.WeaponInfo.Damage = 20;
			duc.WeaponInfo.Range = 3;
			Components.Add(duc);
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
				foreach (var comp in Components.Where(c => c.WeaponInfo != null))
				{
					if (Utilities.Distance(X, Y, PlayerShip.Instance.X, PlayerShip.Instance.Y) <= comp.WeaponInfo.Range)
					{
						Log.Add("The Jraenar ship is firing its " + comp + "!");
						// TODO - evasion/PD
						PlayerShip.Instance.TakeDamage(comp.WeaponInfo.Damage);
					}
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
