using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BOSWP
{
	/// <summary>
	/// Player input via the keyboard.
	/// </summary>
	public static class PlayerInput
	{
		public static IEnumerable<Keys> PressedKeys { get { return pressedKeys; }}

		private static ISet<Keys> pressedKeys = new HashSet<Keys>();

		public static void PressKey(Keys k)
		{
			pressedKeys.Add(k);
		}

		public static void ClearKeys()
		{
			pressedKeys.Clear();
		}
	}
}
