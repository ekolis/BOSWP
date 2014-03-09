using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOSWP
{
	/// <summary>
	/// A 2D square grid, with (0,0) being at the center.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Grid<T> : IReadableGrid<T>
	{
		/// <summary>
		/// Creates a grid with a specified radius (radius 0 = 1 item, radius 1 = 9 items, etc.)
		/// </summary>
		/// <param name="radius"></param>
		public Grid(int radius)
		{
			Radius = radius;
			items = new T[Diameter, Diameter];
		}

		private T[,] items;

		public int Radius {get; private set;}

		public int Diameter {get { return Radius * 2 + 1;}}

		public bool AreCoordsInBounds(int x, int y)
		{
			return x >= -Radius && x <= Radius && y >= -Radius && y <= Radius;
		}

		public T this[int x, int y]
		{
			get
			{
				return items[x + Radius, y + Radius];
			}
			set
			{
				items[x + Radius, y + Radius] = value;
			}
		}

		public IEnumerator<T> GetEnumerator()
		{
			return items.Cast<T>().GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		/// <summary>
		/// Performs a projection using a function.
		/// </summary>
		/// <typeparam name="T2">The target type.</typeparam>
		/// <param name="selector">The projection function.</param>
		/// <returns>The transformed grid.</returns>
		public Grid<T2> Select<T2>(Func<T, T2> selector)
		{
			var g = new Grid<T2>(Radius);
			for (var x = -Radius; x <= Radius; x++)
			{
				for (var y = -Radius; y <= Radius; y++)
					g[x, y] = selector(this[x, y]);
			}
			return g;
		}

		/// <summary>
		/// Finds the x-coordinate of an item, or -1 if it's not found.
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public int GetX(T item)
		{
			for (var x = -Radius; x <= Radius; x++)
			{
				for (var y = -Radius; y <= Radius; y++)
				{
					if (this[x, y].SafeEquals(item))
						return x;
				}
			}
			return -1;
		}

		/// <summary>
		/// Finds the y-coordinate of an item, or -1 if it's not found.
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public int GetY(T item)
		{
			for (var x = -Radius; x <= Radius; x++)
			{
				for (var y = -Radius; y <= Radius; y++)
				{
					if (this[x, y].SafeEquals(item))
						return y;
				}
			}
			return -1;
		}
	}
}
