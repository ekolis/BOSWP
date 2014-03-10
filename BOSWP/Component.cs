using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOSWP
{
	/// <summary>
	/// A ship component.
	/// </summary>
	public class Component : IDamageable
	{
		/// <summary>
		/// The name of the component.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The mass of the component, in kT.
		/// Heavier components require more engines to move them through space.
		/// </summary>
		public int Mass { get; set; }

		/// <summary>
		/// The maximum hitpoints of the component.
		/// </summary>
		public int MaxHitpoints { get; set; }

		/// <summary>
		/// The current hitpoints of the component.
		/// </summary>
		public int Hitpoints { get; set; }

		/// <summary>
		/// Inflicts damage upon the component.
		/// </summary>
		/// <param name="damage">The amount of damage.</param>
		public int TakeDamage(int damage)
		{
			Hitpoints -= damage;
			if (Hitpoints < 0)
			{
				damage = -Hitpoints;
				Hitpoints = 0;
				return damage;
			}
			else
				return 0;
		}

		public Component Clone()
		{
			var c = new Component();
			c.Name = Name;
			c.Mass = Mass;
			c.MaxHitpoints = MaxHitpoints;
			c.Hitpoints = Hitpoints;
			if (WeaponInfo != null)
			{
				c.WeaponInfo = new WeaponInfo();
				c.WeaponInfo.Damage = WeaponInfo.Damage;
				c.WeaponInfo.IsMissile = WeaponInfo.IsMissile;
				c.WeaponInfo.Range = WeaponInfo.Range;
			}
			return c;
		}

		public override string ToString()
		{
			return Name;
		}

		/// <summary>
		/// If this is a weapon, information about it will be stored here.
		/// </summary>
		public WeaponInfo WeaponInfo { get; set; }
	}
}
