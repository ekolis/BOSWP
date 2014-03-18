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
			foreach (var comp in Library)
				comp.Hitpoints = comp.MaxHitpoints;
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
				c.WeaponInfo.ReloadRate = WeaponInfo.ReloadRate;
			}
			c.Crew = Crew;
			c.Thrust = Thrust;
			c.Evasion = Evasion;
			c.PointDefense = PointDefense;
			c.Emissive = Emissive;
			c.Shields = Shields;
            c.ShieldRegeneration = ShieldRegeneration;
			c.ScannerRange = ScannerRange;
			c.SensorRange = SensorRange;
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

		/// <summary>
		/// Amount of thrust this component provides.
		/// Ships require thrust equal to their mass for each point of speed.
		/// Ships that don't even have enough thrust for one speed point will be destroyed.
		/// </summary>
		public int Thrust { get; set; }

		/// <summary>
		/// Percent chance to evade enemy direct fire weapons.
		/// If multiple components on a ship grant evasion, the chances will be applied consecutively.
		/// Thus if two components grant 10% evasion, the actual evasion chance will be 19%. (100% to hit - ((100% to hit * 10% to miss) - (90% to hit * 10% to miss))))
		/// </summary>
		public int Evasion { get; set; }

		/// <summary>
		/// Percent chance to shoot down enemy missile weapons.
		/// If multiple components on a ship grant PD, the chances will be applied consecutively.
		/// Thus if two components grant 10% PD, the actual PD chance will be 19%. (100% to hit - ((100% to hit * 10% to miss) - (90% to hit * 10% to miss))))
		/// </summary>
		public int PointDefense { get; set; }

		/// <summary>
		/// Component blocks a random amount of damage striking the ship (after shields are pierced) from zero up to the emissive value.
		/// </summary>
		public int Emissive { get; set; }

		/// <summary>
		/// Component generates shield points for the ship.
		/// </summary>
		public int Shields { get; set; }

        /// <summary>
        /// Component generates shield points for the ship.
        /// </summary>
        public int ShieldRegeneration { get; set; }

		/// <summary>
		/// Range at which this component can perform long range scans of enemy ships.
		/// </summary>
		public int ScannerRange { get; set; }

		/// <summary>
		/// Range at which this component can detect cloaked objects (i.e. shipyards).
		/// </summary>
		public int SensorRange { get; set; }
	}
}
