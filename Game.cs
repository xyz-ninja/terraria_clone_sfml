using SFML.System;
using System.Collections.Generic;
using TerrariaCloneV2.Entities;

namespace TerrariaCloneV2
{
	class Game
	{
		private World world;
		private Player player;

		private List<NpcSlime> slimes = new List<NpcSlime>();

		public Game() {

			world = new World();

			world.GenerateWorld();

			// создаём игрока
			player = new Player(world);
			player.Spawn(new Vector2f(300, 150));

			// создаём слизней
			for (int i = 0; i < 10; i++) {
				var slime = new NpcSlime(world);

				slime.Direction = Program.Rand.Next(0, 2) == 0 ? 1 : -1;
				slime.Spawn(new Vector2f(Program.Rand.Next(150, 600), 150));

				slimes.Add(slime);
			}

			// создаём быстрого слизня
			var fastSlime = new NpcFastSlime(world);
			fastSlime.Direction = Program.Rand.Next(0, 2) == 0 ? 1 : -1;
			fastSlime.Spawn(new Vector2f(Program.Rand.Next(150, 600), 150));

			slimes.Add(fastSlime);

			DebugRenderer.Enabled = true;
		}

		public void Update() {
			
			player.Update();

			foreach (var slime in slimes) {
				slime.Update();
			}
		}

		public void Draw() {

			Program.RenderWindow.Draw(world);
			Program.RenderWindow.Draw(player);

			foreach (var slime in slimes) {
				Program.RenderWindow.Draw(slime);
			}
			
			DebugRenderer.Draw(Program.RenderWindow);
		}
	}
}
