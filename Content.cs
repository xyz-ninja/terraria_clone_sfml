using SFML.Graphics;

namespace TerrariaCloneV2
{
	class Content
	{
		public const string CONTENT_DIR = "Content\\";

		// ебанный sfml почему-то не может дотянуться до вложенных папок в Textures
		// приходится сделать так что бы всё изображения лежали в одной папке

		// environment
		public static Texture texTiles0; // ground
		public static Texture texTiles1; // grass

		// npc
		public static Texture texNpcSlime;

		// player
		public static Texture texPlayerHair;
		public static Texture texPlayerHands;
		public static Texture texPlayerHead;
		public static Texture texPlayerLegs;
		public static Texture texPlayerShirt;
		public static Texture texPlayerUndershirt;
		public static Texture texPlayerShoes;

		public static void Load() {

			// environment
			texTiles0 = new Texture(CONTENT_DIR + "Textures\\Tiles_0.png");
			texTiles1 = new Texture(CONTENT_DIR + "Textures\\Tiles_2.png");

			// npc
			texNpcSlime = new Texture(CONTENT_DIR + "Textures\\NPC_16.png");

			// player
			texPlayerHair = new Texture(CONTENT_DIR + "Textures\\Player_Hair_29.png");
			texPlayerHands = new Texture(CONTENT_DIR + "Textures\\Player_Hands.png");
			texPlayerHead = new Texture(CONTENT_DIR + "Textures\\Player_Head.png");
			texPlayerLegs = new Texture(CONTENT_DIR + "Textures\\Player_Pants.png");
			texPlayerShirt = new Texture(CONTENT_DIR + "Textures\\Player_Shirt.png");
			texPlayerUndershirt = new Texture(CONTENT_DIR + "Textures\\Player_Undershirt.png");
			texPlayerShoes = new Texture(CONTENT_DIR + "Textures\\Player_Shoes.png");
		}
	}
}
