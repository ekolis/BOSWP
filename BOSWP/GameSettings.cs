using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BOSWP
{
	/// <summary>
	/// Game settings, can be loaded from a data file for customizability.
	/// </summary>
	public class GameSettings
	{
		/// <summary>
		/// Radius of the galaxy map (0 => 1x1, 1 => 3x3, 2 => 5x5, etc.)
		/// </summary>
		public int GalaxyRadius { get; set; }

		/// <summary>
		/// Number of star systems.
		/// </summary>
		public int Systems { get; set; }

		/// <summary>
		/// Radius of each star system. Should be at least 2.
		/// </summary>
		public int SystemRadius { get; set; }

		/// <summary>
		/// Number of planets in each star system.
		/// </summary>
		public int PlanetsPerSystem { get; set; }

		/// <summary>
		/// Chance for each planet to contain an allied colony.
		/// </summary>
        public int ColonyChance { get; set; }
        /// <summary>
        /// Chance for each uninhabited planet to contain minerals.
        /// </summary>
        public int MineralsChance { get; set; }
        /// <summary>
        /// Max about of minerals that can be found per planet.
        /// </summary>
        public int MaxMinerals { get; set; }

		/// <summary>
		/// Number of enemy shipyards in the galaxy.
		/// </summary>
		public int EnemyShipyards { get; set; }

		/// <summary>
		/// Maximum build rate of each enemy shipyard.
		/// </summary>
		public int EnemyShipyardBuildRate { get; set; }
        
		/// <summary>
		/// Maximum build rate of each enemy shipyard.
		/// </summary>
        public int EnemyShipyardScrapRate { get; set; }
		/// <summary>
		/// Maximum build rate of each enemy shipyard.
		/// </summary>
        public int EnemyShipScrapRate { get; set; }
	}
}
