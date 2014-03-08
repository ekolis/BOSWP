using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using K = System.Windows.Forms.Keys;

namespace BOSWP
{
	/// <summary>
	/// A direction on the map.
	/// </summary>
	public class Direction
	{
		private Direction(int dx, int dy, params K[] keys)
		{
			DeltaX = dx;
			DeltaY = dy;
			Keys = keys;
		}

		public int DeltaX { get; private set; }

		public int DeltaY { get; private set; }

		public IEnumerable<K> Keys { get; private set; }

		public Direction Opposite
		{
			get
			{
				return All.Single(d => d.DeltaX == -DeltaX && d.DeltaY == -DeltaY);
			}
		}

		public static Direction None = new Direction(0, 0, K.NumPad5, K.Clear, K.OemPeriod, K.Decimal, K.D5, K.Space);
		public static Direction North = new Direction(0, -1, K.Up, K.NumPad8, K.D8);
		public static Direction Northeast = new Direction(1, -1, K.NumPad9, K.D9, K.PageUp);
		public static Direction Northwest = new Direction(-1, -1, K.NumPad7, K.D7, K.Home);
		public static Direction East = new Direction(1, 0, K.Right, K.NumPad6, K.D6);
		public static Direction West = new Direction(-1, 0, K.Left, K.NumPad4, K.D4);
		public static Direction South = new Direction(0, 1, K.Down, K.NumPad2, K.D2);
		public static Direction Southeast = new Direction(1, 1, K.NumPad3, K.D3, K.PageDown);
		public static Direction Southwest = new Direction(-1, 1, K.NumPad1, K.D1, K.End);

		public static IEnumerable<Direction> All
		{
			get
			{
				yield return None;
				yield return North;
				yield return Northeast;
				yield return Northwest;
				yield return East;
				yield return West;
				yield return South;
				yield return Southeast;
				yield return Southwest;
			}
		}

		public static IEnumerable<Direction> Orthogonal
		{
			get
			{
				yield return North;
				yield return South;
				yield return East;
				yield return West;
			}
		}
	}
}
