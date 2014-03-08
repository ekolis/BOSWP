using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOSWP
{
	/// <summary>
	/// A planet. Can be uninhabited, or an allied colony.
	/// </summary>
	public class Planet : SpaceObject
	{
		public Planet()
			: base('o', Color.White)
		{

		}

		public override bool BeBumped(Ship source)
		{
			// TODO - explore planets, and buy stuff at allied colonies
			return false;
		}
	}
}
