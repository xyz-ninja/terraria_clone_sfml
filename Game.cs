using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrariaCloneV2
{
	class Game
	{

		private World world;

		public Game() {
			world = new World();
		}

		public void Update() {

		}

		public void Draw() {
			Program.RenderWindow.Draw(world);
		}
	}
}
