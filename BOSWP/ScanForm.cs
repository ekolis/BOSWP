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
				Text = ((EnemyShip)Ship).Name;
			else
				Text = "Our Ship";

			lblHitpointsTotal.Text = Ship.Hitpoints + " / " + Ship.MaxHitpoints;
			lblShieldsTotal.Text = "0"; // TODO - shields
			lblMassTotal.Text = Ship.Mass + " kT";
			lblCrewTotal.Text = Ship.Crew.ToString();
			lblThrustTotal.Text = Ship.Thrust.ToString();
			lblSpeed.Text = Ship.Speed.ToString();

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
				lblHitpoints.Text = lblShields.Text = lblMass.Text = lblCrew.Text = lblThrust.Text = lblDamage.Text = lblRange.Text = lblReload.Text = lblMissile.Text = "?";
			}
			else
			{
				lblDescription.Text = item.Description;
				lblHitpoints.Text = item.Hitpoints + " / " + item.MaxHitpoints;
				lblShields.Text = "0"; // TODO - shields
				lblMass.Text = item.Mass.ToString();
				lblCrew.Text = item.Crew.ToString();
				lblThrust.Text = item.Thrust.ToString();
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
