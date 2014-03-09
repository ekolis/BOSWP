using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOSWP
{
	public class EnemyShipyard : SpaceObject
	{
		public EnemyShipyard(int maxBuildRate)
			: base('#', Color.Red)
		{
			MaxBuildRate = maxBuildRate;
			Savings = maxBuildRate;
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
					Math.Max(1, 
					(
						Galaxy.Current.FindSpaceObjects<EnemyShipyard>().Count() * 10 +
						Galaxy.Current.FindSpaceObjects<EnemyShip>().Count()
					));
			}
		}

		/// <summary>
		/// Increments the shipyard's savings, and attempts to build a ship if possible.
		/// </summary>
		public void Build()
		{
			Savings += BuildRate;

			// TODO - have a "target design" once we have components
			if (Savings >= 1000)
			{
				// find sector to place ship in
				var places = new List<dynamic>();
				for (int x = -1; x <= 1; x++)
				{
					for (int y = -1; y <= 1; y++)
					{
						if (StarSystem.SpaceObjects[X + x, Y + y] == null)
							places.Add(new { X = X + x, Y = Y + y });
					}
				}
				if (!places.Any())
					return; // nowhere to put the ship
				var coords = places.PickRandom();

				// create the ship and place it
				var ship = new EnemyShip();
				Savings -= ship.Cost;
				StarSystem.PlaceSpaceObject(ship, coords.X, coords.Y, 0);
			}
		}
	}
}
