using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOSWP
{
	/// <summary>
	/// Dummy space object to block off sectors adjacent to planets and stars so it's always possible to find a path between them.
	/// </summary>
	public class Atmosphere : SpaceObject
	{
		public Atmosphere()
			: base('.', Color.White)
		{

		}

		public override bool BeBumped(Ship source)
		{
			return false;
		}

		public override void Scan()
		{
			
		}
	}
}
