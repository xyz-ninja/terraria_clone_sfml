using SFML.Graphics;

namespace TerrariaCloneV2
{
	class Content
	{
		public const string CONTENT_DIR = "Content\\";

		// environment
		public static Texture texTiles0; // ground
		public static Texture texTiles1; // grass

		// npc
		public static Texture texNpcSlime;

		public static void Load() {

			// environment
			texTiles0 = new Texture(CONTENT_DIR + "Textures\\Tiles_0.png");
			texTiles1 = new Texture(CONTENT_DIR + "Textures\\Tiles_2.png");

			// npc
			texNpcSlime = new Texture(CONTENT_DIR + "Textures\\NPC_16.png");
		}
	}
}
