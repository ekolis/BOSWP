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

		/// <summary>
		/// Dijkstra-style pathfinding algorithm.
		/// based on http://www.roguebasin.com/index.php?title=Pathfinding
		/// </summary>
		/// <param name="sys">The star system to navigate.</param>
		/// <param name="x">Starting X coordinate.</param>
		/// <param name="y">Starting Y coordinate.</param>
		/// <param name="tx">Target X coordinate.</param>
		/// <param name="ty">Target Y coordinate.</param>
		/// <returns>Direction to travel.</returns>
		public static Direction Pathfind(StarSystem sys, int x, int y, int tx, int ty)
		{
			// create "check-it" queue and add start node to it
			var pQueue = new List<PathfindingNode>();
			pQueue.Add(new PathfindingNode(x, y, null, 0));

			// create list of previously visitied nodes
			var visited = new List<PathfindingNode>();

			// search for paths
			while (pQueue.Any())
			{
				// take out the lowest cost node and work on it
				var lowestCost = pQueue.Min(n => n.Cost);
				var node = pQueue.Where(n => n.Cost == lowestCost).First();
				pQueue.Remove(node);

				// did we reach the goal?
				if (node.X == tx && node.Y == ty)
				{
					var first = node.SecondAncestor;
					return Direction.Get(first.X - x, first.Y - y);
				}

				// find successor nodes
				foreach (var dir in Direction.All)
				{
					var next = new PathfindingNode(node.X + dir.DeltaX, node.Y + dir.DeltaY, node, node.Cost + 1);
					if (sys.SpaceObjects.AreCoordsInBounds(next.X, next.Y))
					{
						var sameNodes = visited.Where(n => n.X == next.X && n.Y == next.Y).ToArray();
						var queuedNodes = pQueue.Where(n => n.X == next.X && n.Y == next.Y).ToArray();
						if (!sameNodes.Any() ||
							queuedNodes.Any(n => n.Cost > next.Cost) ||
							sameNodes.Any(n => n.Cost > next.Cost))
						{
							// if node already exists, get rid of old node with higher cost
							foreach (var n in sameNodes)
								visited.Remove(n);
							foreach (var n in queuedNodes)
								pQueue.Remove(n);

							// add to visited list and priority queue
							pQueue.Add(next);
							visited.Add(next);
						}
					}
				}
			}

			// get as close as possible
			var closest = visited.OrderBy(n => Distance(tx, ty, n.X, n.Y)).FirstOrDefault();

			if (closest == null)
				return Direction.None; // no path leads closer than where we are now

			// find first node in path
			var resultFirst = closest.SecondAncestor;
			return Direction.Get(resultFirst.X - x, resultFirst.Y - y);
		}

		private class PathfindingNode
		{
			public PathfindingNode(int x, int y, PathfindingNode parent, int cost)
			{
				X = x;
				Y = y;
				Parent = parent;
				Cost = cost;
			}

			public int X { get; set; }
			public int Y { get; set; }

			/// <summary>
			/// The node that was traversed just prior to reaching this node.
			/// </summary>
			public PathfindingNode Parent { get; set; }

			/// <summary>
			/// The number of steps required to reach this node.
			/// </summary>
			public int Cost { get; set; }

			/// <summary>
			/// The second ancestor of this node (first after the starting node)
			/// </summary>
			public PathfindingNode SecondAncestor
			{
				get
				{
					var first = this;
					var ancestor = Parent;
					var ancestor2 = Parent == null ? null : Parent.Parent;
					while (ancestor2 != null)
					{
						first = ancestor;
						ancestor = ancestor.Parent;
						ancestor2 = ancestor2.Parent;
					}
					return first;
				}
			}
		}
	}
}
