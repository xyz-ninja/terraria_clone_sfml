﻿using SFML.System;

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

			DebugRenderer.Enabled = true;
		}

		public void Update() {
			
			player.Update();
		}

		public void Draw() {

			Program.RenderWindow.Draw(world);
			Program.RenderWindow.Draw(player);

			DebugRenderer.Draw(Program.RenderWindow);
		}
	}
}
