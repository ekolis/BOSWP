using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOSWP
{
	/// <summary>
	/// The overarching game map. Contains a grid of star systems, though not all spaces in the grid are necessarily occupied.
	/// </summary>
	public class Galaxy
	{
		/// <summary>
		/// The galaxy currently in play.
		/// </summary>
		public static Galaxy Current { get; set; }

		/// <summary>
		/// Creates a galaxy with a specified radius (radius 0 = 1x1, radius 1 = 3x3, etc.)
		/// </summary>
		/// <param name="radius"></param>
		public Galaxy(int radius, int numSystems, int systemRadius, int planetsPerSystem)
		{
			StarSystems = new Grid<StarSystem>(radius);

			// place star systems on grid
			for (int i = 0; i < numSystems && i < (radius + 1) * (radius + 1); i++)
			{
				if (i == 0)
				{
					// place randomly
					var x = Dice.Range(-StarSystems.Radius, StarSystems.Radius);
					var y = Dice.Range(-StarSystems.Radius, StarSystems.Radius);
					StarSystems[x, y] = new StarSystem(systemRadius, planetsPerSystem);
				}
				else
				{
					// place adjacent to some other system in an empty space
					var places = new List<dynamic>();
					for (var x = -StarSystems.Radius; x <= StarSystems.Radius; x++)
					{
						for (var y = -StarSystems.Radius; y <= StarSystems.Radius; y++)
						{
							if (!IsFilled(x, y) && HasFilledNeighbor(x, y))
								places.Add(new { X = x, Y = y });
						}
					}
					var coords = places.PickRandom();
					StarSystems[coords.X, coords.Y] = new StarSystem(systemRadius, planetsPerSystem);
				}
			}

			// place player ship
			var sys = StarSystems.Where(s => s != null).PickRandom();
			var pplaces = new List<dynamic>();
			for (var x = -sys.SpaceObjects.Radius; x <= sys.SpaceObjects.Radius; x++)
			{
				for (var y = -sys.SpaceObjects.Radius; y <= sys.SpaceObjects.Radius; y++)
				{
					if (sys.SpaceObjects[x, y] == null)
						pplaces.Add(new { X = x, Y = y });
				}
			}
			var pcoords = pplaces.PickRandom();
			sys.PlaceSpaceObject(PlayerShip.Instance, pcoords.X, pcoords.Y, 0);
			
			// TODO - place enemy shipyards

			// TODO - delete warp points that lead nowhere
		}

		/// <summary>
		/// Does a space have an orthogonally neighboring space that has a system in it?
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		private bool HasFilledNeighbor(int x, int y)
		{
			return IsFilled(x - 1, y) || IsFilled(x + 1, y) || IsFilled(x, y - 1) || IsFilled(x, y + 1);
		}

		/// <summary>
		/// Does a space have a system in it?
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		private bool IsFilled(int x, int y)
		{
			return
				x >= -StarSystems.Radius &&
				x <= StarSystems.Radius &&
				y >= -StarSystems.Radius &&
				y <= StarSystems.Radius &&
				StarSystems[x, y] != null;

		}

		/// <summary>
		/// The star systems in the galaxy.
		/// </summary>
		public Grid<StarSystem> StarSystems { get; private set; }
	}
}
