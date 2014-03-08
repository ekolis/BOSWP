using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOSWP
{
	/// <summary>
	/// Random number generator.
	/// </summary>
	public static class Dice
	{
		private static Random rng = new Random();

		/// <summary>
		/// Gets a random number within a range (inclusive).
		/// </summary>
		/// <param name="min"></param>
		/// <param name="max"></param>
		/// <returns></returns>
		public static int Range(int min, int max)
		{
			return rng.Next(min, max + 1);
		}

		/// <summary>
		/// Picks a random element from a sequence.
		/// Returns a default value if there are no items to choose from.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="items"></param>
		/// <returns></returns>
		public static T PickRandom<T>(this IEnumerable<T> items)
		{
			if (items == null || !items.Any())
				return default(T);
			return items.ElementAt(Range(0, items.Count() - 1));
		}
	}
}
