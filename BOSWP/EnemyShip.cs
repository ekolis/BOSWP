using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
		static EnemyShip()
		{
			// load enemy ship designs
			var designsData = JsonConvert.DeserializeObject<IDictionary<string, IEnumerable<string>>>(File.ReadAllText("EnemyShips.json"));
			var library = new List<EnemyShip>();
			foreach (var designData in designsData)
			{
				var design = new EnemyShip();
				design.Name = designData.Key;
				design.Glyph = design.Name[0].ToString().ToUpper()[0];
				foreach (var compName in designData.Value)
				{
					var comp = Component.Get(compName).Clone();
					design.Components.Add(comp);
				}
				library.Add(design);
			}
			Library = library;
		}

		public static IEnumerable<EnemyShip> Library { get; private set; }

		public EnemyShip()
			: base('J', Color.Firebrick)
		{
			NeedsNewWaypoint = true;
		}

		/// <summary>
		/// The name of the ship.
		/// </summary>
		public string Name { get; set; }

		public override bool Move()
		{
			if (PlayerShip.Instance.StarSystem == StarSystem)
			{
				// player ship sighted! CHASE HIM!!!
				// TODO - stay at max weapons range
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
				bool didstuff = false;
				do
				{
					didstuff = false;
					foreach (var comp in Components.Where(c => c.WeaponInfo != null && c.WeaponInfo.Wait <= 0))
					{
						if (Utilities.Distance(X, Y, PlayerShip.Instance.X, PlayerShip.Instance.Y) <= comp.WeaponInfo.Range)
						{
							Log.Add("The " + this + " is firing its " + comp + "!");
							if (PlayerShip.Instance.RollEvasionOrPD(comp.WeaponInfo.IsMissile))
								Log.Add("But it misses!");
							else
								PlayerShip.Instance.TakeDamage(comp.WeaponInfo.Damage);
							comp.WeaponInfo.Wait += comp.WeaponInfo.ReloadRate;
						}
					}
				} while (didstuff);
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
			if (component.Hitpoints <= 0)
				Log.Add(damage + " damage to " + this + "'s hull! Scans indicate a component was destroyed!");
			else
				Log.Add(damage + " damage to " + this + "'s hull!");
		}

		public override void LogEmissiveDamage(int damage, bool soakedAll)
		{
			if (soakedAll)
				Log.Add("Their emissive armor blocked all " + damage + " of the damage.");
			else
				Log.Add("Their emissive armor blocked " + damage + " of the damage.");
		}

		public override void LogShieldDamage(int damage, bool soakedAll)
		{
			if (soakedAll)
				Log.Add("Their shields blocked all " + damage + " of the damage.");
			else
				Log.Add("Their shields blocked " + damage + " of the damage.");
		}

		public EnemyShip Clone()
		{
			var s = new EnemyShip();
			s.Name = Name;
			s.Glyph = Glyph;
			s.Color = Color;
			foreach (var comp in Components)
				s.Components.Add(comp.Clone());
			return s;
		}

		public override string ToString()
		{
			return "Jraenar " + Name;
		}
	}
}
