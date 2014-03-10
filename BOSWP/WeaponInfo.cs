using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOSWP
{
	/// <summary>
	/// Information about a weapon.
	/// </summary>
	public class WeaponInfo
	{
		/// <summary>
		/// Damage inflicted by the weapon.
		/// </summary>
		public int Damage { get; set; }

		/// <summary>
		/// Range of the weapon.
		/// </summary>
		public int Range { get; set; }

		/// <summary>
		/// Missiles use the PD stat for defense; direct fire weapons use the evasion stat.
		/// </summary>
		public bool IsMissile { get; set; }

		/// <summary>
		/// How often this weapon can fire.
		/// </summary>
		public int ReloadRate { get; set; }

		// TODO - damage types?
	}
}
