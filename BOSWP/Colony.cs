using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOSWP
{
	/// <summary>
	/// An allied colony.
	/// You can get repairs or purchase components at a colony, assuming there are no enemies in the system.
	/// </summary>
	public class Colony
	{
		public Colony()
		{
			// pick a random inventory for this colony
			var inventory = new List<Component>();
			for (var i = 0; i < 5; i++)
			{
				var comps = Component.Library.Where(c => !inventory.Contains(c));
				if (comps.Any())
					inventory.Add(comps.PickRandom());
			}
			Inventory = inventory;
		}

		/// <summary>
		/// Components available for sale.
		/// </summary>
		public IEnumerable<Component> Inventory { get; private set; }
	}
}
