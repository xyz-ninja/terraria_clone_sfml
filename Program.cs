using SFML.Graphics;
using System;

namespace TerrariaCloneV2
{
	class Program 
	{

		private static RenderWindow win;

		public static RenderWindow RenderWindow { get { return win; } }
		public static Game Game { private set; get; }
		public static Random Rand { private set; get; }

		static void Main(string[] args) {

			win = new RenderWindow(new SFML.Window.VideoMode(800, 600), "TerrariaClone");
			win.SetVerticalSyncEnabled(true);

			win.Closed += Win_Closed;
			win.Resized += Win_Resized;

			Content.Load();

			Rand = new Random();
			Game = new Game();

			while (win.IsOpen) {

				win.DispatchEvents(); // анализирует события с окна (закрытие, перемещение мыши и тд.)

				Game.Update();

				win.Clear(Color.Black);

				Game.Draw();

				win.Display();
			}
		}

		private static void Win_Closed(object sender, EventArgs e) {
			win.Close();
		}

		private static void Win_Resized(object sender, SFML.Window.SizeEventArgs e) {
			win.SetView(new View(new FloatRect(0, 0, e.Width, e.Height)));
		}
	}
}