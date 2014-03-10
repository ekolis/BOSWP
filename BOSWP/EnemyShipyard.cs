using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOSWP
{
	public class EnemyShipyard : SpaceObject, IDamageable
	{
		public EnemyShipyard(int maxBuildRate)
			: base('#', Color.Red)
		{
			MaxBuildRate = maxBuildRate;
			Savings = maxBuildRate;
			Hitpoints = 400;
		}

		public override bool BeBumped(Ship source)
		{
			// TODO - notify player if he bumps a shipyard
			return false;
		}

		/// <summary>
		/// Saved minerals for building.
		/// </summary>
		public int Savings { get; set; }

		public int MaxBuildRate { get; set; }

		/// <summary>
		/// The rate at which the shipyard builds new ships.
		/// Due to maintenance costs, more shipyards and more ships slow down build rate.
		/// </summary>
		public int BuildRate
		{
			get
			{
				return
					MaxBuildRate /
					Math.Max(1, Galaxy.Current.EnemyShipyardCount * 10 + Galaxy.Current.EnemyShipCount);
			}
		}

		/// <summary>
		/// Increments the shipyard's savings, and attempts to build a ship if possible.
		/// </summary>
		/// <returns>true if something was built, otherwise false</returns>
		public bool Build()
		{
			Savings += BuildRate;

			// TODO - have a "target design" once we have components
			if (Savings >= 1000)
			{
				// find sector to place ship in
				var places = new List<dynamic>();
				var sys = StarSystem;
				var x = X;
				var y = Y;
				for (int dx = -1; dx <= 1; dx++)
				{
					for (int dy = -1; dy <= 1; dy++)
					{
						if (sys.SpaceObjects.AreCoordsInBounds(x + dx, y + dy) && sys.SpaceObjects[x + dx, y + dy] == null)
							places.Add(new { X = x + dx, Y = y + dy });
					}
				}
				if (!places.Any())
					return false; // nowhere to put the ship
				var coords = places.PickRandom();

				// create the ship and place it
				var ship = new EnemyShip();
				Savings -= ship.Cost;
				StarSystem.PlaceSpaceObject(ship, coords.X, coords.Y, 0);
				Galaxy.Current.RefreshEnemyCounts();
				return true;
			}
			else
				return false;
		}

		public int TakeDamage(int damage)
		{
			Hitpoints -= damage;
			if (Hitpoints < 0)
			{
				var leftovers = -Hitpoints;
				Hitpoints = 0;
				return leftovers;
			}
			else
				return 0;
		}

		public int Hitpoints
		{
			get;
			private set;
		}
	}
}
