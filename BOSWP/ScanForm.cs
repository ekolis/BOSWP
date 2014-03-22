using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BOSWP
{
	public partial class ScanForm : Form
	{
		public ScanForm(Ship ship)
		{
			InitializeComponent();
			Ship = ship;
			if (Ship is EnemyShip)
				Text = ((EnemyShip)Ship).ToString();
			else
				Text = "Our Ship";

			lblHitpointsTotal.Text = Ship.Hitpoints + " / " + Ship.MaxHitpoints;
			lblShieldsTotal.Text = Ship.Shields + " / " + Ship.MaxShields;
            lblShieldRegenerationTotal.Text = "+" + ship.Components.Sum(c => c.ShieldRegeneration) + "/turn";
			lblEvasionTotal.Text = Ship.Evasion + "%";
			lblPDTotal.Text = Ship.PointDefense + "%";
            lblArmorRating.Text = ship.Components.Where(c => c.IsArmor).Sum(c => c.Hitpoints) + "hp@" + (Ship.Components.Count(c => c.IsArmor) * 100 / Math.Max(1, ship.Components.Count())) + "%";
            lblEmissiveTotal.Text = Ship.Emissive.ToString();
			lblMassTotal.Text = Ship.Mass + " kT";
			lblCrewTotal.Text = Ship.Crew.ToString();
			lblThrustTotal.Text = Ship.Thrust.ToString();
			lblSpeed.Text = Ship.Speed.ToString();
			lblScannerTotal.Text = Ship.ScannerRange.ToString();
			lblSensorTotal.Text = Ship.SensorRange.ToString();

			// warnings for vulnerability to critical hits on crew quarters and engines
			if (Ship.Components.Any(c => Ship.Crew - c.Crew < Ship.Mass))
				lblCrew.ForeColor = Color.Red;
			else
				lblCrew.ForeColor = SystemColors.ControlText;
			if (Ship.Components.Any(c => Ship.Thrust - c.Thrust < Ship.Mass))
				lblThrust.ForeColor = Color.Red;
			else
				lblThrust.ForeColor = SystemColors.ControlText;

			lstComponents.DataSource = Ship.Components.OrderBy(c => c.Name).ThenByDescending(c => c.Hitpoints).ToList();
		}

		public Ship Ship { get; private set; }

		private void lstComponents_SelectedIndexChanged(object sender, EventArgs e)
		{
			// update component data
			var item = (Component)lstComponents.SelectedItem;
			if (item == null)
			{
				lblDescription.Text = "Component Description";
				lblHitpoints.Text = lblShields.Text = lblEvasion.Text = lblPD.Text = lblEmissive.Text = lblMass.Text = lblCrew.Text = lblThrust.Text = lblScanner.Text = lblSensor.Text = lblDamage.Text = lblRange.Text = lblReload.Text = lblMissile.Text = "?";
			}
			else
			{
				lblDescription.Text = item.Description;
				lblHitpoints.Text = item.Hitpoints + " / " + item.MaxHitpoints;
				lblShields.Text = item.Shields.ToString();
                lblShieldRegeneration.Text = "+" + item.ShieldRegeneration + "/turn";
				lblEvasion.Text = item.Evasion + "%";
				lblPD.Text = item.PointDefense + "%";
				lblEmissive.Text = item.Emissive.ToString();
				lblMass.Text = item.Mass.ToString();
				lblCrew.Text = item.Crew.ToString();
				lblThrust.Text = item.Thrust.ToString();
				lblScanner.Text = item.ScannerRange.ToString();
				lblSensor.Text = item.SensorRange.ToString();
				if (item.WeaponInfo == null)
					lblDamage.Text = lblRange.Text = lblReload.Text = lblMissile.Text = "N/A";
				else
				{
					lblDamage.Text = item.WeaponInfo.Damage.ToString();
					lblRange.Text = item.WeaponInfo.Range.ToString();
					lblReload.Text = item.WeaponInfo.ReloadRate.ToString();
					lblMissile.Text = item.WeaponInfo.IsMissile.ToString();
				}
			}
		}
	}
}
