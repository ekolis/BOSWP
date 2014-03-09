using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOSWP
{
	public static class Utilities
	{
		/// <summary>
		/// Null-safe equality testing.
		/// </summary>
		/// <param name="o1"></param>
		/// <param name="o2"></param>
		/// <returns></returns>
		public static bool SafeEquals(this object o1, object o2)
		{
			if (o1 == null && o2 == null)
				return true;
			if (o1 == null || o2 == null)
				return false;
			return o1.Equals(o2);
		}

		/// <summary>
		/// Computes distance along the grid.
		/// </summary>
		/// <param name="x1"></param>
		/// <param name="y1"></param>
		/// <param name="x2"></param>
		/// <param name="y2"></param>
		/// <returns></returns>
		public static int Distance(int x1, int y1, int x2, int y2)
		{
			return Math.Max(Math.Abs(x2 - x1), Math.Abs(y2 - y1));
		}
	}
}
