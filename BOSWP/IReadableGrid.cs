using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOSWP
{
	/// <summary>
	/// A 2D square grid, with (0,0) being at the center.
	/// Only exposes read operations, so it can be covariant.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IReadableGrid<out T> : IEnumerable<T>
	{
		int Radius { get; }
		int Diameter { get; }
		T this[int x, int y] { get; }
	}
}
