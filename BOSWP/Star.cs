using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BOSWP
{
	/// <summary>
	/// Twinkle, twinkle, little star...
	/// </summary>
	public class Star : SpaceObject
	{
		public Star()
			: base('*', Color.Yellow)
		{

		}

		public override bool BeBumped(Ship source)
		{
			// TODO - message for player when he bumps a star?
			return false;
		}

		public override void Scan()
		{
			MessageBox.Show("Twinkle, twinkle, little star...");
		}
	}
}
