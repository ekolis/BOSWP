using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
			var settings = JsonConvert.DeserializeObject<GameSettings>(File.ReadAllText("Settings.json"));
			new Galaxy(settings);
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
			{
				systemMap.Grid = sys.SpaceObjects;
				systemMap.BoringGrid = sys.SensorGrid;
				var rectangles = new List<Tuple<Rectangle, Color>>();

				// weapon ranges
				foreach (var ship in sys.FindSpaceObjects<Ship>())
				{
					foreach (var w in ship.Components.Select(c => c.WeaponInfo).Where(w => w != null))
					{
						rectangles.Add(Tuple.Create(new Rectangle(ship.X - w.Range, ship.Y - w.Range, w.Range * 2 + 1, w.Range * 2 + 1), ship.Color));
					}
				}

				// player ship scanner range
				rectangles.Add(Tuple.Create(new Rectangle(PlayerShip.Instance.X - PlayerShip.Instance.ScannerRange, PlayerShip.Instance.Y - PlayerShip.Instance.ScannerRange, PlayerShip.Instance.ScannerRange * 2 + 1, PlayerShip.Instance.ScannerRange * 2 + 1), Color.Silver));
				
				systemMap.Rectangles = rectangles;
			}
			systemMap.Invalidate();
			galaxyMap.Invalidate();

			lblHitpoints.Text = PlayerShip.Instance.Hitpoints + " / " + PlayerShip.Instance.MaxHitpoints;
			lblShields.Text = PlayerShip.Instance.Shields + " / " + PlayerShip.Instance.MaxShields;
			lblMass.Text = PlayerShip.Instance.Mass + " kT";
			lblCrew.Text = PlayerShip.Instance.Crew.ToString();
			lblThrust.Text = PlayerShip.Instance.Thrust.ToString();
			lblSpeed.Text = PlayerShip.Instance.Speed.ToString();
			lblSavings.Text = "$" + PlayerShip.Instance.Savings;

			// warnings for vulnerability to critical hits on crew quarters and engines
			if (PlayerShip.Instance.Components.Any(c => PlayerShip.Instance.Crew - c.Crew < PlayerShip.Instance.Mass))
				lblCrew.ForeColor = Color.Red;
			else
				lblCrew.ForeColor = SystemColors.ControlText;
			if (PlayerShip.Instance.Components.Any(c => PlayerShip.Instance.Thrust - c.Thrust < PlayerShip.Instance.Mass))
				lblThrust.ForeColor = Color.Red;
			else
				lblThrust.ForeColor = SystemColors.ControlText;

			weaponInfoBindingSource.DataSource = PlayerShip.Instance.Components.Where(c => c.WeaponInfo != null).Select(c => c.WeaponInfo);

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
			while (!PlayerShip.Instance.IsDestroyed)
			{
				// refresh the screen?
				bool doUpdate = false;

				// let player move
				var moved = PlayerShip.Instance.Move();
				doUpdate |= moved;

				// other turn stuff (only happens if player moves)
				if (moved)
				{
					// how much time was used?
					var time = 1d / PlayerShip.Instance.Speed;

					// let enemy ships move
					foreach (var ship in Galaxy.Current.FindSpaceObjects<EnemyShip>().ToArray())
					{
						ship.Wait -= time;
						while (ship.Wait <= 0)
						{
							var eMoved = ship.Move();
							doUpdate |= eMoved;
                            ship.Wait += 1d / ship.Speed;
						}
					}

					// let enemy SYs build
					foreach (var sy in Galaxy.Current.FindSpaceObjects<EnemyShipyard>().ToArray())
					{
						sy.Wait -= time;
						while (sy.Wait <= 0)
						{
							var built = sy.Build();
							doUpdate |= built;
							sy.Wait += time;
						}
					}

					// let player attack
					PlayerShip.Instance.Attack();

					// let enemies attack
					foreach (var ship in Galaxy.Current.FindSpaceObjects<EnemyShip>().ToArray())
						ship.Attack();

					// reload weapons
					foreach (var ship in Galaxy.Current.FindSpaceObjects<Ship>().ToArray())
					{
						foreach (var comp in ship.Components.Where(c => c.WeaponInfo != null))
						{
							comp.WeaponInfo.Wait -= time;
							if (comp.WeaponInfo.Wait < 0)
								comp.WeaponInfo.Wait = 0;
						}
					}
                    // recharge shields
                    foreach (var ship in Galaxy.Current.FindSpaceObjects<Ship>().ToArray())
                    {
                        foreach (var comp in ship.Components.Where(c => c.ShieldRegeneration >0))
                        {
                            ship.PartialShields += (double)comp.ShieldRegeneration * time;
                        }
                        ship.Shields = Math.Min(ship.MaxShields, ship.Shields + (int)ship.PartialShields);
                        ship.PartialShields -= (int)ship.PartialShields;
                    }

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
							if (ship is EnemyShip)
							{
								var salvage = ship.Cost / 2;
								PlayerShip.Instance.Savings += salvage;
								Log.Add("We salvage $" + salvage + " worth of minerals from the wreckage.");
							}
						}
						if (ship.Speed <= 0)
						{
							// lack of crew destroys ships
							ship.Delete();
							var name = ship is PlayerShip ? "Our ship " : ("The " + ship);
							Log.Add(name + "'s engines are destroyed and it is dead in space!");
							if (ship is EnemyShip)
							{
								var salvage = ship.Cost / 2;
								PlayerShip.Instance.Savings += salvage;
								Log.Add("We salvage $" + salvage + " worth of minerals from the wreckage.");
							}
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
						var salvage = sy.Savings / 2;
						PlayerShip.Instance.Savings += salvage;
						Log.Add("We salvage $" + salvage + " worth of minerals from the wreckage.");
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
					Invoke(new DoStuffDelegate(() =>
							{
								lstMessages.Items.Clear();
								foreach (var msg in Log.Entries.Reverse())
									lstMessages.Items.Add(msg);
								Log.Clear();
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

		private void btnComponents_Click(object sender, EventArgs e)
		{
			var form = new ScanForm(PlayerShip.Instance);
			form.ShowDialog();
		}

		private void systemMap_MouseDown(object sender, MouseEventArgs e)
		{
			var grid = (Grid<SpaceObject>)systemMap.Grid;
			if (grid == null)
				return;
			var x = e.X / systemMap.GlyphSize - grid.Radius;
			var y = e.Y / systemMap.GlyphSize - grid.Radius;
			var sobj = grid[x, y];
			if (sobj != null && Utilities.Distance(PlayerShip.Instance.X, PlayerShip.Instance.Y, x, y) <= PlayerShip.Instance.ScannerRange)
			{
				if (sobj is EnemyShipyard && !((EnemyShipyard)sobj).IsRevealed)
					return;	// no scanning shipyards we haven't seen yet!

				sobj.Scan();
			}
		}
	}
}
