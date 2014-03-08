using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		public override void Move()
		{
			// Stars can't move, nothing to do here
		}

		public override void Attack()
		{
			// Stars can't attack, nothing to do here
		}

		public override void Bump(SpaceObject target)
		{
			// Stars can't move, nothing to do here
		}

		public override void BeBumped(SpaceObject source)
		{
			// TODO - message for player when he bumps a star?
		}
	}
}
