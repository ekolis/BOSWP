using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOSWP
{
	/// <summary>
	/// A planet. Can be uninhabited, or an allied colony.
	/// </summary>
	public class Planet : SpaceObject
	{
		public Planet()
			: base('o', Color.White)
		{

		}

		/// <summary>
		/// Has this planet been explored yet?
		/// </summary>
		public bool IsExplored { get; private set; }

		/// <summary>
		/// The colony on this planet, if any.
		/// </summary>
		public Colony Colony { get; private set; }

		public override bool BeBumped(Ship source)
		{
			if (IsExplored)
			{
				if (Colony == null)
					Log.Add("This planet is uninhabited; there's nothing of interest here.");
				else if (StarSystem.FindSpaceObjects<EnemyShip>().Any() || StarSystem.FindSpaceObjects<EnemyShipyard>().Any())
					Log.Add("The colony hails you: \"Take care of the Jraenar invaders in the system first!\"");
				else
				{
					Log.Add("The colony repairs your ship and opens up its spacedock.");
					foreach (var comp in PlayerShip.Instance.Components)
						comp.Hitpoints = comp.MaxHitpoints;
					new ShopForm(Colony).ShowDialog();
				}
			}
			else
			{
				// explore the planet
				IsExplored = true;
				var hasColony = Dice.Range(0, 9) == 0;
				if (hasColony)
				{
					Log.Add("This planet contains an allied colony! Bump it again to dock.");
					Color = Color.Blue;
					Colony = new Colony();
				}
				else
				{
					Log.Add("This planet appears to be uninhabited. There's nothing of interest here.");
					Color = Color.Gray;
				}
			}
			return true;
		}
	}
}
