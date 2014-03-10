using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BOSWP
{
	public partial class GameForm : Form
	{
		public GameForm()
		{
			InitializeComponent();
		}

		private void GameForm_Load(object sender, EventArgs e)
		{
			new Galaxy(4, 30, 7, 6, 10, 2000);
			galaxyMap.Grid = Galaxy.Current.StarSystems;
			DoUpdate();

			runner = new Thread(new ThreadStart(RunGame));
			runner.IsBackground = true;
			runner.Start();
		}

		private void DoUpdate()
		{
			var sys = FindPlayerSystem();
			if (sys != null)
				systemMap.Grid = FindPlayerSystem().SpaceObjects;
			systemMap.Invalidate();
			galaxyMap.Invalidate();

			lblHitpoints.Text = PlayerShip.Instance.Hitpoints.ToString();
			lblShields.Text = "0"; // TODO - shields
			lblMass.Text = PlayerShip.Instance.Mass + " kT";
			lblCrew.Text = PlayerShip.Instance.Crew.ToString();
			lblThrust.Text = PlayerShip.Instance.Thrust.ToString();
			lblSpeed.Text = PlayerShip.Instance.Speed.ToString();
		}

		private void GameForm_SizeChanged(object sender, EventArgs e)
		{
			systemMap.Invalidate();
		}

		private void GameForm_KeyDown(object sender, KeyEventArgs e)
		{
			PlayerInput.PressKey(e.KeyCode);
		}

		private Thread runner;

		private void RunGame()
		{
			// TODO - AI ships, etc.
			while (true)
			{
				// refresh the screen?
				bool doUpdate = false;

				// let player move
				var moved = PlayerShip.Instance.Move();
				doUpdate |= moved;

				// other turn stuff (only happens if player moves)
				if (moved)
				{
					// let enemy ships move
					foreach (var ship in Galaxy.Current.FindSpaceObjects<EnemyShip>().ToArray())
					{
						ship.Wait -= 1d / PlayerShip.Instance.Speed;
						if (ship.Wait <= 0)
						{
							var eMoved = ship.Move();
							doUpdate |= eMoved;
							ship.Wait = 1d / ship.Speed;
						}
					}

					// let enemy SYs build
					foreach (var sy in Galaxy.Current.FindSpaceObjects<EnemyShipyard>().ToArray())
					{
						var built = sy.Build();
						doUpdate |= built;
					}

					// let player attack
					PlayerShip.Instance.Attack();

					// let enemies attack
					foreach (var ship in Galaxy.Current.FindSpaceObjects<EnemyShip>().ToArray())
						ship.Attack();

					// scan for destroyed stuff
					foreach (var ship in Galaxy.Current.FindSpaceObjects<Ship>().ToArray())
					{
						foreach (var comp in ship.Components.ToArray())
						{
							if (comp.Hitpoints <= 0)
								ship.Components.Remove(comp);
						}
						if (ship.Crew < ship.Mass)
						{
							// lack of crew destroys ships
							ship.Delete();
							var name = ship is PlayerShip ? "Our ship " : ("The " + ship);
							Log.Add(name + " has insufficient crew and drifts off into space...");
						}
					}
					if (PlayerShip.Instance.Hitpoints <= 0 || PlayerShip.Instance.StarSystem == null)
					{
						PlayerShip.Instance.Delete();
						MessageBox.Show("Your ship is destroyed! Game over!");
					}
					foreach (var sy in Galaxy.Current.FindSpaceObjects<EnemyShipyard>().ToArray().Where(sy => sy.Hitpoints <= 0))
					{
						Log.Add("The Jraenar shipyard explodes!");
						sy.Delete();
						Galaxy.Current.RefreshEnemyCounts();
					}
					foreach (var ship in Galaxy.Current.FindSpaceObjects<EnemyShip>().ToArray().Where(s => s.Hitpoints <= 0))
					{
						Log.Add("The " + ship + " explodes!");
						ship.Delete();
						Galaxy.Current.RefreshEnemyCounts();
					}
					if (Galaxy.Current.FindSpaceObjects<EnemyShipyard>().Count() == 0)
					{
						MessageBox.Show("You have successfully destroyed all Jraenar shipyards! The remaining Jraenar forces make a hasty retreat. You are promoted to Admiral - Congratulations!");
					}

					// update log
					Log.PushNewEntries();
					Invoke(new DoStuffDelegate(() =>
							{
								lstMessages.Items.Clear();
								foreach (var msg in Log.Entries.Reverse())
									lstMessages.Items.Add(msg);
							}));
				}

				PlayerInput.ClearKeys();
				if (doUpdate)
					Invoke(new DoStuffDelegate(() => DoUpdate()));
				Application.DoEvents();
			}
		}

		delegate void DoStuffDelegate();

		private void GameForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			runner.Abort();
		}

		private void systemMap_KeyDown(object sender, KeyEventArgs e)
		{
			PlayerInput.PressKey(e.KeyCode);
		}

		private void systemMap_Leave(object sender, EventArgs e)
		{
			systemMap.Focus();
		}

		private StarSystem FindPlayerSystem()
		{
			return Galaxy.Current.StarSystems.Where(s => s != null && s.SpaceObjects.OfType<PlayerShip>().Any()).SingleOrDefault();
		}
	}
}
