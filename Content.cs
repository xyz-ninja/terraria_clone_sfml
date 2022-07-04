using SFML.Graphics;

namespace TerrariaCloneV2
{
	class Content
	{
		public const string CONTENT_DIR = "Content\\";

		// textures
		public static Texture texTiles0; // ground
		public static Texture texTiles1; // grass

		public static void Load() {

			texTiles0 = new Texture(CONTENT_DIR + "Textures\\Tiles_0.png");
			texTiles1 = new Texture(CONTENT_DIR + "Textures\\Tiles_2.png");
		}
	}
}
