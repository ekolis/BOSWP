using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
		static Component()
		{
			Library = JsonConvert.DeserializeObject<IEnumerable<Component>>(File.ReadAllText("Components.json"));
		}

		public static IEnumerable<Component> Library { get; private set; }

		public static Component Get(string name)
		{
			try
			{
				return Library.Single(c => c.Name == name);
			}
			catch (Exception ex)
			{
				throw new Exception("Either no component named \"" + name + "\" was found in Components.json, or multiple such components were found.", ex);
			}
		}

		/// <summary>
		/// The name of the component.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Description of the component.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// The cost of the component.
		/// </summary>
		public int Cost { get; set; }

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
			c.Description = Description;
			c.Cost = Cost;
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
			c.Crew = Crew;
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

		/// <summary>
		/// Amount of crew this component provides.
		/// Ships require crew greater than or equal to their mass.
		/// Ships with insufficient crew will be destroyed.
		/// </summary>
		public int Crew { get; set; }
	}
}
