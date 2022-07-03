using SFML.System;
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
		private Player player;

		public Game() {
			
			world = new World();

			world.GenerateWorld();

			// создаём игрока
			player = new Player(world);
			player.Spawn(new Vector2f(300, 150));
		}

		public void Update() {
			player.Update();
		}

		public void Draw() {
			Program.RenderWindow.Draw(world);
			Program.RenderWindow.Draw(player);
		}
	}
}
