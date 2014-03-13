using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOSWP
{
	/// <summary>
	/// A star system in the galaxy. Contains a grid of space objects.
	/// </summary>
	public class StarSystem : IColoredGlyphObject
	{
		/// <summary>
		/// Creates a star system with a specified radius (radius 0 = 1x1, radius 1 = 3x3, etc.)
		/// </summary>
		/// <param name="radius"></param>
		public StarSystem(int radius, int numPlanets)
		{
			if (radius < 2)
				throw new ArgumentOutOfRangeException("radius", "Star system radius must be at least 2 to leave room for a star, planets, and warp points.");

			SpaceObjects = new Grid<SpaceObject>(radius);
			SensorGrid = new Grid<bool>(radius);

			// place star in center
			PlaceSpaceObject(new Star(), 0, 0, 1);

			// place warp points around edges
			foreach (var dir in Direction.Orthogonal)
			{
				var x = dir.DeltaX * radius;
				var y = dir.DeltaY * radius;
				PlaceSpaceObject(new WarpPoint(dir), x, y, 1);
			}

			// place planets
			for (int i = 0; i < numPlanets; i++)
			{
				var planet = new Planet();
				var places = new List<dynamic>();
				for (var x = -SpaceObjects.Radius; x <= SpaceObjects.Radius; x++)
				{
					for (var y = -SpaceObjects.Radius; y <= SpaceObjects.Radius; y++)
					{
						if (SpaceObjects[x, y] == null)
							places.Add(new { X = x, Y = y });
					}
				}
				var coords = places.PickRandom();
				if (coords != null)
					PlaceSpaceObject(new Planet(), coords.X, coords.Y, 1);
			}

			// all done, clear the atmospheres so ships can move through those locations
			for (var x = -SpaceObjects.Radius; x <= SpaceObjects.Radius; x++)
			{
				for (var y = -SpaceObjects.Radius; y <= SpaceObjects.Radius; y++)
				{
					if (SpaceObjects[x, y] is Atmosphere)
						SpaceObjects[x, y] = null;
				}
			}
		}

		/// <summary>
		/// The space objects in the star system.
		/// </summary>
		public Grid<SpaceObject> SpaceObjects { get; private set; }

		/// <summary>
		/// Attempts to place a space object at the specified coordinates.
		/// Can also exclude other space objects from a certain radius.
		/// </summary>
		/// <param name="sobj"></param>
		/// <returns>true if successful, false if space is occupied or out of bounds</returns>
		public bool PlaceSpaceObject(SpaceObject sobj, int x, int y, int exclusionRadius)
		{
			// check bounds
			if (x < -SpaceObjects.Radius || x > SpaceObjects.Radius || y < -SpaceObjects.Radius || y > SpaceObjects.Radius)
				return false;

			// check if space is occupied
			if (SpaceObjects[x, y] != null)
				return false;

			// place object
			SpaceObjects[x, y] = sobj;

			// set exclusion radius by placing "atmosphere" space objects
			for (var ex = x - exclusionRadius; ex <= x + exclusionRadius; ex++)
			{
				for (var ey = y - exclusionRadius; ey <= y + exclusionRadius; ey++)
				{
					if (ex != x || ey != y)
						PlaceSpaceObject(new Atmosphere(), ex, ey, 0);
				}
			}

			return true;
		}

		public char Glyph
		{
			get 
			{
				if (SpaceObjects.Contains(PlayerShip.Instance))
					return '@';
				else if (SpaceObjects.OfType<EnemyShipyard>().Any(sy => sy.IsRevealed))
					return '#';
				else if (SpaceObjects.OfType<Planet>().Any(p => p.Colony != null))
					return 'o';
				else
					return '*';
			}
		}

		public Color Color
		{
			get
			{
				if (SpaceObjects.Contains(PlayerShip.Instance))
					return Color.Blue;
				else if (SpaceObjects.OfType<EnemyShipyard>().Any(sy => sy.IsRevealed))
					return Color.Red;
				else if (SpaceObjects.OfType<Planet>().Any(p => p.Colony != null))
					return Color.Blue;
				else if (SpaceObjects.OfType<Planet>().Any(p => !p.IsExplored))
					return Color.White;
				else
					return Color.Gray;
			}
		}

		/// <summary>
		/// Finds all space objects of a type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public IEnumerable<T> FindSpaceObjects<T>()
		{
			return SpaceObjects.OfType<T>();
		}

		/// <summary>
		/// Grid of sectors that the player has swept with his sensors.
		/// </summary>
		public Grid<bool> SensorGrid { get; private set; }
	}
}
