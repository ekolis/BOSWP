using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
			for (var i = 0; i < 2; i++)
				Components.Add(duc.Clone());
		}

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
			foreach (var comp in Components.Where(c => c.WeaponInfo != null))
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
					// TODO - evasion/PD
					target.TakeDamage(comp.WeaponInfo.Damage);
				}
			}

			// second targeting priority: enemy ships
			// find weapons to fire
			foreach (var comp in Components.Where(c => c.WeaponInfo != null && !fired.Contains(c)))
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
					Log.Add("Firing " + comp + " at the Jraenar ship!");
					// TODO - evasion/PD
					target.TakeDamage(comp.WeaponInfo.Damage);
				}
			}
		}

		public override void LogComponentDamage(Component component, int damage)
		{
			Log.Add(damage + " damage to our " + component + "!");
			if (component.Hitpoints <= 0)
				Log.Add("Our " + component + " has been destroyed!");
		}
	}
}
