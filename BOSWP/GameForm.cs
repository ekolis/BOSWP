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
			new Galaxy(4, 30, 7, 6, 10, 1000);
			galaxyMap.Grid = Galaxy.Current.StarSystems;
			systemMap.Grid = FindPlayerSystem().SpaceObjects;
			systemMap.Focus();

			runner = new Thread(new ThreadStart(RunGame));
			runner.Start();
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
						var eMoved = ship.Move();
						doUpdate |= eMoved;
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
					if (PlayerShip.Instance.Hitpoints <= 0)
					{
						PlayerShip.Instance.Delete();
						MessageBox.Show("Your ship is destroyed! Game over!");
						Application.Exit();
					}
					foreach (var sy in Galaxy.Current.FindSpaceObjects<EnemyShipyard>().ToArray().Where(sy => sy.Hitpoints <= 0))
					{
						sy.Delete();
						Galaxy.Current.RefreshEnemyCounts();
					}
					foreach (var ship in Galaxy.Current.FindSpaceObjects<EnemyShip>().ToArray().Where(s => s.Hitpoints <= 0))
					{
						ship.Delete();
						Galaxy.Current.RefreshEnemyCounts();
					}
					if (Galaxy.Current.FindSpaceObjects<EnemyShipyard>().Count() == 0)
					{
						MessageBox.Show("You have successfully destroyed all Jraenar shipyards! The remaining Jraenar forces make a hasty retreat. You are promoted to Admiral - Congratulatiions!");
						Application.Exit();
					}
				}

				PlayerInput.ClearKeys();
				if (doUpdate)
				{
					systemMap.Grid = FindPlayerSystem().SpaceObjects;
					systemMap.Invalidate();
					galaxyMap.Invalidate();
				}
				Application.DoEvents();
			}
		}

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
