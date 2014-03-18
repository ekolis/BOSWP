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
			// deal with shields
			if (damage < Shields)
			{
				Shields -= damage;
				LogShieldDamage(damage, true);
				damage = 0;
			}
			else if (Shields > 0)
			{
				damage -= Shields;
				LogShieldDamage(Shields, false);
				Shields = 0;
			}

			// deal with emissive armor
			var emissive = Math.Min(damage, RollEmissive());
			if (emissive > 0)
			{
				LogEmissiveDamage(emissive, damage <= emissive);
				damage -= emissive;
			}

			// regular damage
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

		public abstract void LogEmissiveDamage(int damage, bool soakedAll);

		public abstract void LogShieldDamage(int damage, bool soakedAll);

		public int Hitpoints
		{
			get { return Components.Sum(c => c.Hitpoints); }
		}

		public int MaxHitpoints
		{
			get { return Components.Sum(c => c.MaxHitpoints); }
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
		public int Speed
		{
			get
			{
				if (Mass == 0)
					return -1;
				return Thrust / Mass;
			}
		}

		/// <summary>
		/// How long until this ship can move again?
		/// </summary>
		public double Wait { get; set; }

		/// <summary>
		/// A ship is destroyed when all its components are destroyed, its speed reaches zero, or its crew drops below its mass.
		/// </summary>
		public bool IsDestroyed
		{
			get
			{
				return Hitpoints <= 0 || Speed <= 0 || Crew < Mass;
			}
		}

		/// <summary>
		/// Percent chance to evade enemy direct fire weapons.
		/// If multiple components on a ship grant evasion, the chances will be applied consecutively.
		/// Thus if two components grant 10% evasion, the actual evasion chance will be 19%. (100% to hit - ((100% to hit * 10% to miss) - (90% to hit * 10% to miss))))
		/// Max evasion value allowed: 99%.
		/// </summary>
		public int Evasion
		{
			get
			{
				var tohit = 100;
				foreach (var comp in Components)
					tohit -= tohit * comp.Evasion / 100;
				return Math.Min(100 - tohit, 99);
			}
		}

		/// <summary>
		/// Percent chance to shoot down enemy missile weapons.
		/// If multiple components on a ship grant PD, the chances will be applied consecutively.
		/// Thus if two components grant 10% PD, the actual PD chance will be 19%. (100% to hit - ((100% to hit * 10% to miss) - (90% to hit * 10% to miss))))
		/// Max PD value allowed: 99%.
		/// </summary>
		public int PointDefense
		{
			get
			{
				var tohit = 100;
				foreach (var comp in Components)
					tohit -= tohit * comp.PointDefense / 100;
				return Math.Min(100 - tohit, 99);
			}
		}

		/// <summary>
		/// Emissive defense rating of this ship. Shots piercing the shields are reduced by a random amount of damage up to this value.
		/// </summary>
		public int Emissive
		{
			get { return Components.Sum(c => c.Emissive); }
		}

		/// <summary>
		/// Maximum shield points of this ship.
		/// </summary>
		public int MaxShields
		{
			get
			{
				return Components.Sum(c => c.Shields);
			}
		}

		/// <summary>
		/// Current shield points of this ship.
		/// </summary>
        public int Shields { get; set; }
        /// <summary>
        /// Carryover from partial shield regeneration due to high speed
        /// </summary>
        public double PartialShields { get; set; }

		/// <summary>
		/// Rolls evasion (for direct fire weapons) or PD (for missile weapons) against this ship.
		/// </summary>
		/// <param name="isMissile"></param>
		/// <returns>true if evaded/shot down, false if it's a hit</returns>
		public bool RollEvasionOrPD(bool isMissile)
		{
			var chance = isMissile ? PointDefense : Evasion;
			return Dice.Range(0, 99) < chance;
		}

		/// <summary>
		/// Rolls emissive defense for this ship.
		/// </summary>
		/// <returns>Damage blocked.</returns>
		public int RollEmissive()
		{
			return Dice.Range(0, Emissive);
		}

		/// <summary>
		/// Range at which this ship can scan enemy ships in detail.
		/// </summary>
		public int ScannerRange
		{
			get
			{
				return Components.Sum(c => c.ScannerRange);
			}
		}

		/// <summary>
		/// Range at which this ship can detect cloaked objects (i.e. shipyards).
		/// All ships get one point of sensor range for free.
		/// </summary>
		public int SensorRange
		{
			get
			{
				return 1 + Components.Sum(c => c.SensorRange);
			}
		}

		public bool CanScan(Ship ship)
		{
			return StarSystem == ship.StarSystem && Utilities.Distance(X, Y, ship.Y, ship.Y) <= ScannerRange;
		}

		public bool CanSee(EnemyShipyard sy)
		{
			return sy.IsRevealed || StarSystem == sy.StarSystem && Utilities.Distance(X, Y, sy.Y, sy.Y) <= SensorRange;
		}

		public override void Scan()
		{
			new ScanForm(this).ShowDialog();
		}
	}
}
