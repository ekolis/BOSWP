﻿using System;
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
		public static Galaxy Current { get; private set; }

		/// <summary>
		/// Creates a galaxy with the specified settings.
		/// </summary>
		/// <param name="settings"></param>
		public Galaxy(GameSettings settings)
            : this(settings.GalaxyRadius, settings.Systems, settings.SystemRadius, settings.PlanetsPerSystem, settings.ColonyChance, settings.MineralsChance, settings.MaxMinerals, settings.EnemyShipyards, settings.EnemyShipyardBuildRate, settings.EnemyShipyardScrapRate, settings.EnemyShipScrapRate)
		{

		}

		/// <summary>
		/// Creates a galaxy with the specified settings (radius 0 = 1x1, radius 1 = 3x3, etc.)
		/// </summary>
		/// <param name="radius"></param>
        public Galaxy(int radius, int numSystems, int systemRadius, int planetsPerSystem, int colonyChance, int mineralsChance, int maxMinerals, int enemyShipyards, int enemyShipyardBuildRate, int enemyShipyardScrapRate, int enemyShipScrapRate)
		{
			Current = this;

            ColonyChance = colonyChance;
            MineralsChance = mineralsChance;
            MaxMinerals= maxMinerals;
            EnemyShipyardScrapRate = enemyShipyardScrapRate;
            EnemyShipScrapRate = enemyShipScrapRate;

			StarSystems = new Grid<StarSystem>(radius);

			// place star systems on grid
			{
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
			}

			// place player ship
			{
				var sys = StarSystems.Where(s => s != null).PickRandom();
				var places = new List<dynamic>();
				for (var x = -sys.SpaceObjects.Radius; x <= sys.SpaceObjects.Radius; x++)
				{
					for (var y = -sys.SpaceObjects.Radius; y <= sys.SpaceObjects.Radius; y++)
					{
						if (sys.SpaceObjects[x, y] == null)
							places.Add(new { X = x, Y = y });
					}
				}
				var coords = places.PickRandom();
				sys.PlaceSpaceObject(PlayerShip.Instance, coords.X, coords.Y, 0);
				
				// perform initial sensor sweep
				for (var x = coords.X - PlayerShip.Instance.SensorRange; x <= coords.X + PlayerShip.Instance.SensorRange; x++)
				{
					for (var y = coords.Y - PlayerShip.Instance.SensorRange; y <= coords.Y + PlayerShip.Instance.SensorRange; y++)
					{
						if (sys.SensorGrid.AreCoordsInBounds(x, y))
							sys.SensorGrid[x, y] = true;
					}
				}
			}

			// place enemy shipyards
			{
				for (int i = 0; i < enemyShipyards; i++)
				{
					var sys = StarSystems.Where(s => s != null).PickRandom();
					var places = new List<dynamic>();
					for (var x = -sys.SpaceObjects.Radius; x <= sys.SpaceObjects.Radius; x++)
					{
						for (var y = -sys.SpaceObjects.Radius; y <= sys.SpaceObjects.Radius; y++)
						{
							if (sys.SpaceObjects[x, y] == null)
								places.Add(new { X = x, Y = y });
						}
					}
					var coords = places.PickRandom();
					sys.PlaceSpaceObject(new EnemyShipyard(enemyShipyardBuildRate), coords.X, coords.Y, 0);
				}
			}

			// delete warp points that lead nowhere
			for (int x = -StarSystems.Radius; x <= StarSystems.Radius; x++)
			{
				for (int y = -StarSystems.Radius; y <= StarSystems.Radius; y++)
				{
					var wpsys = StarSystems[x, y];
					if (wpsys != null)
					{
						for (int sx = -wpsys.SpaceObjects.Radius; sx <= wpsys.SpaceObjects.Radius; sx++)
						{
							for (int sy = -wpsys.SpaceObjects.Radius; sy <= wpsys.SpaceObjects.Radius; sy++)
							{
								if (wpsys.SpaceObjects[sx, sy] is WarpPoint)
								{
									var wp = wpsys.SpaceObjects[sx, sy] as WarpPoint;
									if (wp.TargetSystem == null)
										wpsys.SpaceObjects[sx, sy] = null;
								}
							}
						}
					}
				}
			}

			RefreshEnemyCounts();
		}

		/// <summary>
		/// Percent chance of finding a colony on any given planet.
		/// </summary>
        public int ColonyChance { get; private set; }
        /// <summary>
        /// Percent chance of finding a minerals on any given planet.
        /// </summary>
        public int MineralsChance { get; private set; }
        /// <summary>
        /// Maximum Minerals per planet.
        /// </summary>
        public int MaxMinerals { get; private set; }
        /// <summary>
        /// Maximum Minerals per planet.
        /// </summary>
        public int EnemyShipyardScrapRate { get; private set; }
        /// <summary>
        /// Maximum Minerals per planet.
        /// </summary>
        public int EnemyShipScrapRate { get; private set; }

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

		/// <summary>
		/// Finds all space objects of a type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public IEnumerable<T> FindSpaceObjects<T>()
		{
			return StarSystems.Where(s => s != null).SelectMany(s => s.FindSpaceObjects<T>());
		}

		private int enemyShipyardCount, enemyShipCount;

		public void RefreshEnemyCounts()
		{
			enemyShipyardCount = FindSpaceObjects<EnemyShipyard>().Count();
			enemyShipCount = FindSpaceObjects<EnemyShip>().Count();
		}

		public int EnemyShipyardCount
		{
			get
			{
				return enemyShipyardCount;
			}
		}

		public int EnemyShipCount
		{
			get
			{
				return enemyShipCount;
			}
		}
	}
}
