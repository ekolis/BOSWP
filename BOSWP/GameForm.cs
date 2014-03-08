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
			Galaxy.Current = new Galaxy(4, 30, 7, 6);
			var home = Galaxy.Current.StarSystems.Where(s => s != null && s.SpaceObjects.OfType<PlayerShip>().Any()).Single();
			galaxyMap.Grid = Galaxy.Current.StarSystems;
			systemMap.Grid = home.SpaceObjects;

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
				PlayerShip.Instance.Move();
				PlayerInput.ClearKeys();
				systemMap.Invalidate();
				Application.DoEvents();
			}
		}

		private void GameForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			runner.Abort();
		}
	}
}
