using SFML.System;
using TerrariaCloneV2.Entities;

namespace TerrariaCloneV2
{
	class Game
	{
		private World world;
		private Player player;
		private NpcSlime npcSlime;

		public Game() {
			
			world = new World();

			world.GenerateWorld();

			// создаём игрока
			player = new Player(world);
			player.Spawn(new Vector2f(300, 150));

			// создаём слизня
			npcSlime = new NpcSlime(world);
			npcSlime.Spawn(new Vector2f(500, 150));

			DebugRenderer.Enabled = true;
		}

		public void Update() {
			
			player.Update();
			npcSlime.Update();
		}

		public void Draw() {

			Program.RenderWindow.Draw(world);
			Program.RenderWindow.Draw(player);
			Program.RenderWindow.Draw(npcSlime);

			DebugRenderer.Draw(Program.RenderWindow);
		}
	}
}
