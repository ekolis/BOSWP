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
	/// <summary>
	/// For buying stuff from colonies.
	/// </summary>
	public partial class ShopForm : Form
	{
		public ShopForm(Colony colony)
		{
			InitializeComponent();
			Colony = colony;
			lstComponents.DataSource = Colony.Inventory.ToList();
			Text = "Shop - $" + PlayerShip.Instance.Savings;
		}

		public Colony Colony { get; private set; }

		private void btnBuy_Click(object sender, EventArgs e)
		{
			var comp = (Component)lstComponents.SelectedItem;
			if (comp == null)
			{
				MessageBox.Show("Choose a component, will ya?");
				return;
			}

			// make sure we can afford the component
			if (comp.Cost > PlayerShip.Instance.Savings)
			{
				MessageBox.Show("Window shopping are you? We can't afford one of those!");
				return;
			}

			// make sure component is valid thrustwise and crewwise
			var mass = PlayerShip.Instance.Mass + comp.Mass;
			var crew = PlayerShip.Instance.Crew + comp.Crew;
			var thrust = PlayerShip.Instance.Thrust + comp.Thrust;
			if (mass > crew)
			{
				MessageBox.Show("We can't install this component - it would stretch our crew too thin! Buy more crew quarters first.");
				return;
			}
			if (mass > thrust)
			{
				MessageBox.Show("We can't install this component - me bonnie engines canna take the strain! Buy more engines first.");
				return;
			}

			// buy the component
			var copycomp = comp.Clone();
			copycomp.Hitpoints = copycomp.MaxHitpoints;
			PlayerShip.Instance.Components.Add(copycomp);
			PlayerShip.Instance.Savings -= comp.Cost;
			Text = "Shop - $" + PlayerShip.Instance.Savings;
			MessageBox.Show("Thank you; come again!");
		}

		private void btnOurShip_Click(object sender, EventArgs e)
		{
			new ScanForm(PlayerShip.Instance).ShowDialog();
		}

		private void lstComponents_SelectedIndexChanged(object sender, EventArgs e)
		{
			// update component data
			var item = (Component)lstComponents.SelectedItem;
			if (item == null)
			{
				lblDescription.Text = "Component Description";
				lblHitpoints.Text = lblShields.Text = lblMass.Text = lblCrew.Text = lblThrust.Text = lblDamage.Text = lblRange.Text = lblReload.Text = lblMissile.Text = "?";
				lblCost.Text = "Choose a component to view its price.";
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
				lblCost.Text = "This component will run you $" + item.Cost + ".";
			}
		}
	}
}
