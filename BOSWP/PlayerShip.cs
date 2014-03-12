using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace BOSWP
{
	/// <summary>
	/// The ship controlled by the player.
	/// </summary>
	public class PlayerShip : Ship
	{
		public static PlayerShip Instance { get; private set; }

		static PlayerShip()
		{
			Instance = new PlayerShip();
		}

		private PlayerShip()
			: base('@', Color.Blue)
		{
			var compNames = JsonConvert.DeserializeObject<IEnumerable<string>>(File.ReadAllText("PlayerShip.json"));
			foreach (var compName in compNames)
			{
				var comp = Component.Get(compName).Clone();
				Components.Add(comp);
			}
		}

		/// <summary>
		/// Minerals saved for buying components.
		/// </summary>
		public int Savings { get; set; }

		public override bool Move()
		{
			foreach (var k in PlayerInput.PressedKeys.ToArray())
			{
				var dir = Direction.All.SingleOrDefault(d => d.Keys.Contains(k));
				if (dir != null)
				{
					var newx = X + dir.DeltaX;
					var newy = Y + dir.DeltaY;
					if (newx < -StarSystem.SpaceObjects.Radius || newx > StarSystem.SpaceObjects.Radius || newy < -StarSystem.SpaceObjects.Radius || newy > StarSystem.SpaceObjects.Radius)
						return false; // out of bounds
					if (StarSystem.SpaceObjects[newx, newy] != null)
						return StarSystem.SpaceObjects[newx, newy].BeBumped(this);
					else
					{
						// move ship
						var oldx = X;
						var oldy = Y;
						StarSystem.SpaceObjects[newx, newy] = this;
						StarSystem.SpaceObjects[oldx, oldy] = null;

						// do sensor sweep
						var sr = SensorRange;
						for (var x = X - sr; x <= X + sr; x++)
						{
							for (var y = Y - sr; y <= Y + sr; y++)
							{
								if (StarSystem.SpaceObjects[x, y] is EnemyShipyard)
								{
									Log.Add("Enemy shipyard sighted!");
									((EnemyShipyard)StarSystem.SpaceObjects[x, y]).Reveal();
								}
							}
						}

						return true;
					}
				}
			}
			return false;
		}

		public override void Attack()
		{
			var fired = new List<Component>();

			// first targeting priority: shipyards
			// find weapons to fire
			bool didstuff = false;
			do
			{
				didstuff = false;
				foreach (var comp in Components.Where(c => c.WeaponInfo != null && c.WeaponInfo.Wait <= 0))
				{
					var sys = FindSpaceObjectsInRange<EnemyShipyard>(comp.WeaponInfo.Range);
					if (sys.Any())
					{
						// find closest
						EnemyShipyard target = null;
						var dist = int.MaxValue;
						foreach (var sy in sys)
						{
							var nd = Utilities.Distance(X, Y, sy.X, sy.Y);
							if (nd < dist)
							{
								target = sy;
								dist = nd;
							}
						}

						// fire!
						Log.Add("Firing " + comp + " at the Jraenar shipyard!");
						fired.Add(comp);
						target.TakeDamage(comp.WeaponInfo.Damage);
						comp.WeaponInfo.Wait += comp.WeaponInfo.ReloadRate;
						didstuff = true;
					}
				}
			} while (didstuff);

			// second targeting priority: enemy ships
			// find weapons to fire
			do
			{
				didstuff = false;
				foreach (var comp in Components.Where(c => c.WeaponInfo != null && !fired.Contains(c) && c.WeaponInfo.Wait <= 0))
				{
					var ships = FindSpaceObjectsInRange<EnemyShip>(comp.WeaponInfo.Range);
					if (ships.Any())
					{
						// find closest
						EnemyShip target = null;
						var dist = int.MaxValue;
						foreach (var sy in ships)
						{
							var nd = Utilities.Distance(X, Y, sy.X, sy.Y);
							if (nd < dist)
							{
								target = sy;
								dist = nd;
							}
						}

						// fire!
						Log.Add("Firing " + comp + " at the " + target + "!");
						if (target.RollEvasionOrPD(comp.WeaponInfo.IsMissile))
							Log.Add("We missed!");
						else
							target.TakeDamage(comp.WeaponInfo.Damage);
						comp.WeaponInfo.Wait += comp.WeaponInfo.ReloadRate;
						didstuff = true;
					}
				}
			} while (didstuff);
		}

		public override void LogComponentDamage(Component component, int damage)
		{
			if (component.Hitpoints <= 0)
				Log.Add(damage + " damage to our " + component + "! It was destroyed!");
			else
				Log.Add(damage + " damage to our " + component + "!");
		}

		public override void LogEmissiveDamage(int damage, bool soakedAll)
		{
			if (soakedAll)
				Log.Add("Our emissive armor blocked all " +  damage + " of the damage.");
			else
				Log.Add("Our emissive armor blocked " + damage + " of the damage.");
		}

		public override void LogShieldDamage(int damage, bool soakedAll)
		{
			if (soakedAll)
				Log.Add("Our shields blocked all " + damage + " of the damage.");
			else
				Log.Add("Our shields blocked " + damage + " of the damage.");
		}
	}
}
