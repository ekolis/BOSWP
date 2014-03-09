using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOSWP
{
	/// <summary>
	/// An enemy ship.
	/// </summary>
	public class EnemyShip : Ship
	{
		public EnemyShip()
			: base('J', Color.Firebrick)
		{

		}

		public override bool Move()
		{
			// TODO - move enemy ships, give them waypoints and such
			return true;
		}
	}
}
