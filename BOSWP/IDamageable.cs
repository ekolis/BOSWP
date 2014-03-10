using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOSWP
{
	/// <summary>
	/// Something which can take damage.
	/// </summary>
	public interface IDamageable
	{
		/// <summary>
		/// Inflicts damage.
		/// </summary>
		/// <param name="damage">The amount of damage.</param>
		/// <returns>Leftover damage that should leak to the next avaiable component, if this is a component.</returns>
		int TakeDamage(int damage);

		/// <summary>
		/// Remaining hitpoints.
		/// </summary>
		int Hitpoints { get; }
	}
}
