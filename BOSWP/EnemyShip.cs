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
			Hitpoints = 100;
		}

		public override bool Move()
		{
			// TODO - move enemy ships, give them waypoints and such
			return true;
		}

		public override void Attack()
		{
			// targeting priority: player ship
			if (PlayerShip.Instance.StarSystem == StarSystem)
			{
				if (Utilities.Distance(X, Y, PlayerShip.Instance.X, PlayerShip.Instance.Y) <= 3)
				{
					Log.Add("The Jraenar ship is firing on us! 20 damage to the hull!");
					PlayerShip.Instance.TakeDamage(20);
				}
			}
		}
	}
}
