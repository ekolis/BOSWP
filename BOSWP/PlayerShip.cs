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
			var template = new Component();
			template.Name = "generic component";
			template.Mass = 30;
			template.MaxHitpoints = 30;
			template.Hitpoints = template.MaxHitpoints;
			for (var i = 0; i < 10; i++)
				Components.Add(template.Clone());
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
			// first targeting priority: shipyards
			var sys = FindSpaceObjectsInRange<EnemyShipyard>(3);
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

				// TODO - weapons
				Log.Add("Firing on the Jraenar shipyard!");
				target.TakeDamage(20);
				return;
			}

			// second targeting priority: enemy ships
			var ships = FindSpaceObjectsInRange<EnemyShip>(3);
			if (ships.Any())
			{
				// find closest
				EnemyShip target = null;
				var dist = int.MaxValue;
				foreach (var ship in ships)
				{
					var nd = Utilities.Distance(X, Y, ship.X, ship.Y);
					if (nd < dist)
					{
						target = ship;
						dist = nd;
					}
				}

				// TODO - weapons
				Log.Add("Firing on the Jraenar ship!");
				target.TakeDamage(20);
				return;
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
