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
			new Galaxy(4, 30, 7, 6);
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
				bool doUpdate = false;
				doUpdate |= PlayerShip.Instance.Move();
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
