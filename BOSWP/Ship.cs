using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOSWP
{
	/// <summary>
	/// A ship - either the player's ship or an enemy ship.
	/// </summary>
	public abstract class Ship : SpaceObject, IDamageable
	{
		protected Ship(char glyph, Color color)
			: base(glyph, color)
		{
			Components = new HashSet<Component>();
		}

		/// <summary>
		/// What happens when it's this space object's turn to move?
		/// </summary>
		/// <returns>true if the space object is done moving, false if waiting for player input</returns>
		public abstract bool Move();
		
		/// <summary>
		/// Attacks the nearest enemy ship (or the player ship, if this is an enemy ship).
		/// </summary>
		public abstract void Attack();

		/// <summary>
		/// Nothing special happens when a ship is bumped.
		/// </summary>
		/// <param name="source"></param>
		public override bool BeBumped(Ship source)
		{
			if (source == this)
				return true; // ship is deciding to sit still
			return false;
		}

		/// <summary>
		/// The cost to build this ship.
		/// </summary>
		public int Cost
		{
			get
			{
				return Components.Sum(c => c.Cost);
			}
		}

		/// <summary>
		/// The components which make up this ship.
		/// </summary>
		public ISet<Component> Components { get; private set; }

		public int TakeDamage(int damage)
		{
			while (damage > 0 && Hitpoints > 0)
			{
				var comp = Components.PickRandom();
				var leftovers = comp.TakeDamage(damage);
				if (damage - leftovers > 0)
					LogComponentDamage(comp, damage - leftovers);
				damage = leftovers;
			}
			return damage;
		}

		public abstract void LogComponentDamage(Component component, int damage);

		public int Hitpoints
		{
			get { return Components.Sum(c => c.Hitpoints); }
		}

		public int MaxHitpoints
		{
			get	{ return Components.Sum(c => c.MaxHitpoints); }
		}

		/// <summary>
		/// Total mass of ship components.
		/// </summary>
		public int Mass { get { return Components.Sum(c => c.Mass); } }

		/// <summary>
		/// Total crew on the ship.
		/// If this is less than the ship's mass, the ship will be derelict.
		/// </summary>
		public int Crew { get { return Components.Sum(c => c.Crew); } }

		/// <summary>
		/// Total thrust of the ship's engines.
		/// </summary>
		public int Thrust { get { return Components.Sum(c => c.Thrust); } }

		/// <summary>
		/// Thrust divided by mass.
		/// If this is zero (i.e. thrust is less than mass), the ship will be derelict.
		/// </summary>
		public int Speed { get { return Thrust / Mass; } }

		/// <summary>
		/// How long until this ship can move again?
		/// </summary>
		public double Wait { get; set; }
	}
}
